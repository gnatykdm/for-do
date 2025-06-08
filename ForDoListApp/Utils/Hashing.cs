using System;
using System.Security.Cryptography;
using System.Text;

namespace Utills
{
    public interface IHashPassword {
        string? HashPasswordSHA256(string password);
    }

    public class HashPassword : IHashPassword
    {
        public string? HashPasswordSHA256(string password)
        {
            if (string.IsNullOrEmpty || string.IsNullOrWhiteSpace))
            {
                return null;
            }

            using var sha256 = SHA256.Create();
            var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashed);
        }
    }
}