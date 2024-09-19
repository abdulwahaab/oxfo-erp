
using System.Security.Cryptography;

namespace OxfoHome.DAL.Services
{
    public class HashService : IHashService
    {
        private const int SaltSize = 16; // Size of the salt in bytes
        private const int HashSize = 32; // Size of the hash in bytes
        private const int Iterations = 10000; // Number of iterations

        public string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            // Generate the hash
            byte[] hash = GenerateHash(password, salt);

            // Combine salt and hash
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Convert the stored hash to bytes
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Generate the hash for the provided password
            byte[] hash = GenerateHash(password, salt);

            // Compare the generated hash with the stored hash
            return CryptographicOperations.FixedTimeEquals(hash, hashBytes.AsSpan(SaltSize));
        }

        private byte[] GenerateHash(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(HashSize);
        }
    }
}