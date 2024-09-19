using System;
using System.Security.Cryptography;
using System.Text;

namespace OxfoHome.DAL.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public EncryptionService(byte[] key, byte[] iv)
        {
            if (key == null || key.Length != 32)
                throw new ArgumentException("Invalid encryption key length. Expected 256-bit key (32 bytes).", nameof(key));

            if (iv == null || iv.Length != 12)
                throw new ArgumentException("Invalid IV length. Expected 96-bit IV (12 bytes).", nameof(iv));

            this.key = key;
            this.iv = iv;
        }

        public string Encrypt(string plainText)
        {
            using (var aesGcm = new AesGcm(key))
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] cipherText = new byte[plainTextBytes.Length];
                byte[] tag = new byte[16]; // 128-bit tag

                aesGcm.Encrypt(iv, plainTextBytes, cipherText, tag);

                // Combine IV, ciphertext, and tag into one byte array
                byte[] encryptedData = new byte[iv.Length + cipherText.Length + tag.Length];
                Buffer.BlockCopy(iv, 0, encryptedData, 0, iv.Length);
                Buffer.BlockCopy(cipherText, 0, encryptedData, iv.Length, cipherText.Length);
                Buffer.BlockCopy(tag, 0, encryptedData, iv.Length + cipherText.Length, tag.Length);

                // Encode the byte array using Base32 (URL-safe by design)
                return Base32Encode(encryptedData);
            }
        }

        public string Decrypt(string cipherText)
        {
            byte[] encryptedData = Base32Decode(cipherText);

            byte[] iv = new byte[12];
            byte[] tag = new byte[16];
            byte[] cipherTextBytes = new byte[encryptedData.Length - iv.Length - tag.Length];

            Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(encryptedData, iv.Length, cipherTextBytes, 0, cipherTextBytes.Length);
            Buffer.BlockCopy(encryptedData, iv.Length + cipherTextBytes.Length, tag, 0, tag.Length);

            using (var aesGcm = new AesGcm(key))
            {
                byte[] decryptedText = new byte[cipherTextBytes.Length];
                aesGcm.Decrypt(iv, cipherTextBytes, tag, decryptedText);
                return Encoding.UTF8.GetString(decryptedText);
            }
        }

        private static string Base32Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder((data.Length + 7) * 8 / 5);

            for (int i = 0; i < data.Length; i += 5)
            {
                int byteCount = Math.Min(data.Length - i, 5);
                ulong buffer = 0;

                for (int j = 0; j < byteCount; j++)
                {
                    buffer |= (ulong)data[i + j] << (8 * (4 - j));
                }

                for (int j = 0; j < 8; j++)
                {
                    if (j * 5 >= byteCount * 8) break;

                    int index = (int)((buffer >> (35 - j * 5)) & 0x1F);
                    result.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"[index]);
                }
            }

            return result.ToString();
        }

        private static byte[] Base32Decode(string encoded)
        {
            // This is a simplified example and assumes valid Base32 input.
            int byteCount = encoded.Length * 5 / 8;
            byte[] data = new byte[byteCount];

            ulong buffer = 0;
            int bitsLeft = 0;
            int dataIndex = 0;

            foreach (char c in encoded)
            {
                buffer <<= 5;
                buffer |= (ulong)(c < 'A' ? c - '2' + 26 : c - 'A');
                bitsLeft += 5;

                if (bitsLeft >= 8)
                {
                    data[dataIndex++] = (byte)(buffer >> (bitsLeft - 8));
                    bitsLeft -= 8;
                }
            }

            return data;
        }
    }
}
