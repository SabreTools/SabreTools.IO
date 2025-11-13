using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using SabreTools.IO.Extensions;

namespace SabreTools.IO.Encryption
{
    public static class AESCTR
    {
        /// <summary>
        /// Create AES decryption cipher and intialize
        /// </summary>
        /// <param name="key">Byte array representation of 128-bit encryption key</param>
        /// <param name="iv">AES initial value for counter</param>
        /// <returns>Initialized AES cipher</returns>
        public static IBufferedCipher CreateDecryptionCipher(byte[] key, byte[] iv)
        {
            if (key.Length != 16)
                throw new ArgumentOutOfRangeException(nameof(key));

            var keyParam = new KeyParameter(key);
            var cipher = CipherUtilities.GetCipher("AES/CTR");
            cipher.Init(forEncryption: false, new ParametersWithIV(keyParam, iv));
            return cipher;
        }

        /// <summary>
        /// Create AES encryption cipher and intialize
        /// </summary>
        /// <param name="key">Byte array representation of 128-bit encryption key</param>
        /// <param name="iv">AES initial value for counter</param>
        /// <returns>Initialized AES cipher</returns>
        public static IBufferedCipher CreateEncryptionCipher(byte[] key, byte[] iv)
        {
            if (key.Length != 16)
                throw new ArgumentOutOfRangeException(nameof(key));

            var keyParam = new KeyParameter(key);
            var cipher = CipherUtilities.GetCipher("AES/CTR");
            cipher.Init(forEncryption: true, new ParametersWithIV(keyParam, iv));
            return cipher;
        }

        /// <summary>
        /// Perform an AES operation using an existing cipher
        /// </summary>
        public static void PerformOperation(uint size,
            IBufferedCipher cipher,
            Stream input,
            Stream output,
            Action<string>? progress = null)
        {
            // Get MiB-aligned block count and extra byte count
            int blockCount = (int)((long)size / (1024 * 1024));
            int extraBytes = (int)((long)size % (1024 * 1024));

            // Process MiB-aligned data
            if (blockCount > 0)
            {
                for (int i = 0; i < blockCount; i++)
                {
                    byte[] readBytes = input.ReadBytes(1024 * 1024);
                    byte[] processedBytes = cipher.ProcessBytes(readBytes);
                    output.Write(processedBytes);
                    output.Flush();
                    progress?.Invoke($"{i} / {blockCount + 1} MB");
                }
            }

            // Process additional data
            if (extraBytes > 0)
            {
                byte[] readBytes = input.ReadBytes(extraBytes);
                byte[] finalBytes = cipher.DoFinal(readBytes);
                output.Write(finalBytes);
                output.Flush();
            }

            progress?.Invoke($"{blockCount + 1} / {blockCount + 1} MB... Done!\r\n");
        }

        /// <summary>
        /// Perform an AES operation using two existing ciphers
        /// </summary>
        public static void PerformOperation(uint size,
            IBufferedCipher firstCipher,
            IBufferedCipher secondCipher,
            Stream input,
            Stream output,
            Action<string>? progress = null)
        {
            // Get MiB-aligned block count and extra byte count
            int blockCount = (int)((long)size / (1024 * 1024));
            int extraBytes = (int)((long)size % (1024 * 1024));

            // Process MiB-aligned data
            if (blockCount > 0)
            {
                for (int i = 0; i < blockCount; i++)
                {
                    byte[] readBytes = input.ReadBytes(1024 * 1024);
                    byte[] firstProcessedBytes = firstCipher.ProcessBytes(readBytes);
                    byte[] secondProcessedBytes = secondCipher.ProcessBytes(firstProcessedBytes);
                    output.Write(secondProcessedBytes);
                    output.Flush();
                    progress?.Invoke($"{i} / {blockCount + 1} MB");
                }
            }

            // Process additional data
            if (extraBytes > 0)
            {
                byte[] readBytes = input.ReadBytes(extraBytes);
                byte[] firstFinalBytes = firstCipher.DoFinal(readBytes);
                byte[] secondFinalBytes = secondCipher.DoFinal(firstFinalBytes);
                output.Write(secondFinalBytes);
                output.Flush();
            }

            progress?.Invoke($"{blockCount + 1} / {blockCount + 1} MB... Done!\r\n");
        }
    }
}
