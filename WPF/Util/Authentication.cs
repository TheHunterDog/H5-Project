using System;
using System.Linq;
using System.Security.Cryptography;
using Database.Model;

namespace WPF.Util;

public class Authentication
{
    public static bool CheckAdmin<T>(T user) where T :IAuthenticatable
    {
        return user.IsAdmin;
    }
    public static IAuthenticatable GetUserWithCredentials(string password, string username,StudentBeleidContext ctx)
    {
        return (IAuthenticatable) ctx.Teachers.First(s => s.Username.Equals(username)) ??
               (IAuthenticatable) ctx.StudentBegeleiders.First(s=>s.Username.Equals(username));
    }

    public static Byte[] HashPasswordWithSalt(byte[] password,byte[] salt)
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

    public static bool CheckPassword(byte[] passwordInput, byte[] passwordRemote, byte[] salt)
    {
        byte[] hashedPassword = HashPasswordWithSalt(passwordInput, salt);
        return hashedPassword == passwordRemote;
    }
    
}