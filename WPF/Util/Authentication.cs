using System;
using System.Linq;
using System.Security.Cryptography;
using Database.Model;

namespace WPF.Util;

public class Authentication
{
    //TODO:Make unique
    public static readonly string Salt = "APP";
    public static bool CheckAdmin<T>(T user) where T :IAuthenticatable
    {
        return user.IsAdmin;
    }
    public static IAuthenticatable? GetUserWithCredentials(string password, string username,StudentBeleidContext ctx)
    {
        try
        {
            return (IAuthenticatable) ctx.Teachers.First(s => s.Username.Equals(username)) ??
                   (IAuthenticatable) ctx.StudentBegeleiders.First(s => s.Username.Equals(username)) ?? null;
        }
        catch (InvalidOperationException e)
        {
            return null;
        }
    }

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

    public static bool CheckPassword(byte[] passwordInput, byte[] passwordRemote, byte[] salt)
    {
        byte[] hashedPassword = HashPasswordWithSalt(passwordInput, salt);
        return hashedPassword == passwordRemote;
    }
    
    public static bool CheckPassword(byte[] passwordInput, string passwordRemote, byte[] salt)
    {
        byte[] hashedPassword = HashPasswordWithSalt(passwordInput, salt);
        return System.Text.Encoding.Default.GetString(hashedPassword) == passwordRemote;
    }
    
    public static bool CheckPassword(string PasswordHashed, string passwordRemote)
    {
        return PasswordHashed == passwordRemote;
    }
}