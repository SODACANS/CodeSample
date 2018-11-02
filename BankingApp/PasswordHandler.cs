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
                byte[] input = System.Text.Encoding.UTF8.GetBytes(password + salt);
                return System.Text.Encoding.UTF8.GetString(sha.ComputeHash(input));
            }
        }

        public static string GenerateRandomSalt(int length = 512)
        {
            using (RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[length];
                rand.GetBytes(bytes);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
