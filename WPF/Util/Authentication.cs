#region

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Database.Model;

#endregion

namespace WPF.Util;

public class Authentication
{
    public static readonly string Salt = "APP";


    public static bool CheckAdmin<T>(T user) where T :IAuthenticatable
    {
        return user.IsAdmin;
    }

    /**
     * <summary>get the user with the password and username</summary>
     */
    public static IAuthenticatable? GetUserWithCredentials(string password, string username,DatabaseContext ctx)
    {
        try
        {
            var teacher = ctx.Teacher.Where(s => s.Username.Equals(username));
            var studentSuperVisor = ctx.StudentSupervisor.Where(s => s.Username.Equals(username));
            if (teacher.Count() != 0)
            {
                return teacher.First() as IAuthenticatable;
            }
            else if (studentSuperVisor.Count() != 0)
            {
                return studentSuperVisor.First() as IAuthenticatable;
            }

            return null;

        }
        catch (InvalidOperationException e)
        {
            return null;
        }
    }

    /**
     * <summary>Hash the password for storage</summary>
     */
    public static byte[] HashPasswordWithSalt(byte[] password,byte[] salt)
    {
        HashAlgorithm algorithm = new SHA256Managed();

        byte[] plainwithSalt = new byte[password.Length + salt.Length];

        for (int i = 0; i < password.Length; i++)
        {
            plainwithSalt[i] = password[i];
        }

        for (int i = 0; i < salt.Length; i++)
        {
            plainwithSalt[password.Length + i] = salt[i];
        }

        return algorithm.ComputeHash(plainwithSalt);
    }

    /**
     * <summary>check if the passwords match</summary>
     */
    public static bool CheckPassword(byte[] passwordInput, string passwordRemote, byte[] salt)
    {
        byte[] hashedPassword = HashPasswordWithSalt(passwordInput, salt);
        return Encoding.Default.GetString(hashedPassword) == passwordRemote;
    }
}