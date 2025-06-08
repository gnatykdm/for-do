using System;
using System.Security.Cryptography;
using System.Text;

namespace ForDoListApp.Utils
{
    public interface IHashPassword {
        string HashPasswordSHA256(string password);
    }

    public class HashPassword : IHashPassword
    {
        public string HashPasswordSHA256(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or whitespace.", nameof(password));

            var hashed = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashed);
        }
    }
}
