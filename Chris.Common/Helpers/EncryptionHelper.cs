using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Chris.Common.Helpers
{
    public static class EncryptionHelper
    {
        public static string GeneratePassword()
        {
            return new PasswordGenerator().GeneratePassword();
        }

        public static string EncryptPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public static bool PasswordIsVerified(string hashedPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }
    }

    internal class PasswordGenerator
    {
        public string GeneratePassword()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#£$%&/{([)]=}?\\<>|-_+*'^~";

            var chars = new char[10];
            
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                chars[i] = allowedChars[random.Next(allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
