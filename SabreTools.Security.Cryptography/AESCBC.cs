using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace SabreTools.Security.Cryptography
{
    public static class AESCBC
    {
        /// <summary>
        /// Create AES/CBC with no padding decryption cipher and intialize
        /// </summary>
        /// <param name="key">Byte array representation of 128-bit encryption key</param>
        /// <param name="iv">AES initial value for counter</param>
        /// <returns>Initialized AES cipher</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="key"/> does not have a length of 16.
        /// </exception>
        public static IBufferedCipher CreateDecryptionCipher(byte[] key, byte[] iv)
        {
            if (key.Length != 16)
                throw new ArgumentOutOfRangeException(nameof(key));

            var keyParam = new KeyParameter(key);
            var cipher = CipherUtilities.GetCipher("AES/CBC/NoPadding");
            cipher.Init(forEncryption: false, new ParametersWithIV(keyParam, iv));
            return cipher;
        }

        /// <summary>
        /// Create AES/CBC with no padding encryption cipher and intialize
        /// </summary>
        /// <param name="key">Byte array representation of 128-bit encryption key</param>
        /// <param name="iv">AES initial value for counter</param>
        /// <returns>Initialized AES cipher</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="key"/> does not have a length of 16.
        /// </exception>
        public static IBufferedCipher CreateEncryptionCipher(byte[] key, byte[] iv)
        {
            if (key.Length != 16)
                throw new ArgumentOutOfRangeException(nameof(key));

            var keyParam = new KeyParameter(key);
            var cipher = CipherUtilities.GetCipher("AES/CBC/NoPadding");
            cipher.Init(forEncryption: true, new ParametersWithIV(keyParam, iv));
            return cipher;
        }

        /// <summary>
        /// Decrypts <paramref name="data"/> with AES-128-CBC (no padding).
        /// Returns null if any argument is invalid or decryption fails.
        /// </summary>
        /// <param name="data">Ciphertext to decrypt.</param>
        /// <param name="key">16-byte AES key.</param>
        /// <param name="iv">16-byte initialisation vector.</param>
        public static byte[]? Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            // Validate the key and IV
            if (key.Length != 16)
                return null;
            if (iv.Length != 16)
                return null;

            var cipher = CreateDecryptionCipher(key, iv);
            return cipher.DoFinal(data);
        }

        /// <summary>
        /// Encrypts <paramref name="data"/> with AES-128-CBC (no padding).
        /// Returns null if any argument is invalid or encryption fails.
        /// </summary>
        /// <param name="data">Plaintext to encrypt.</param>
        /// <param name="key">16-byte AES key.</param>
        /// <param name="iv">16-byte initialisation vector.</param>
        public static byte[]? Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            // Validate the key and IV
            if (key.Length != 16)
                return null;
            if (iv.Length != 16)
                return null;

            var cipher = CreateEncryptionCipher(key, iv);
            return cipher.DoFinal(data);
        }
    }
}
