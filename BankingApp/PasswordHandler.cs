using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BankingApp
{
    class PasswordHandler
    {
        public static (string password, string salt) HashPassword(string password)
        {
            string salt = GenerateRandomSalt();
            return (HashPassword(password, salt), salt);
        }

        public static string HashPassword(string password, string salt)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] input = Convert.FromBase64String(password + salt);
                return Convert.ToBase64String(sha.ComputeHash(input));
            }
        }

        public static string GenerateRandomSalt(int length = 512)
        {
            using (RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider())
            {
                byte[] chars = new byte[length];
                rand.GetBytes(chars);
                return Convert.ToBase64String(chars);
            }
        }
    }
}
