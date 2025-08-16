using System;
using System.Security.Cryptography;
using System.Text;

namespace MvcCV.Helpers
{
    public static class PasswordHelper
    {
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password Cannot be empty!", nameof(password));
            }

            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = HashPasswordWithSalt(password, salt);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
            {
                return false;
            }

            try
            {
                string[] parts = hashedPassword.Split(':');
                if (parts.Length != 2)
                {
                    return false;
                }

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] storedHash = Convert.FromBase64String(parts[1]);

                byte[] computedHash = HashPasswordWithSalt(password, salt);

                return SlowEquals(storedHash, computedHash);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsValidHash(string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
            {
                return false;
            }

            try
            {
                string[] parts = hashedPassword.Split(':');
                if (parts.Length != 2)
                {
                    return false;
                }

                Convert.FromBase64String(parts[0]);
                Convert.FromBase64String(parts[1]);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            uint diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            
            return diff == 0;
        }
    }
}