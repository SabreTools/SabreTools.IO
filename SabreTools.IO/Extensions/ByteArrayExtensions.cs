using System;

namespace SabreTools.IO.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Convert a byte array to a hex string
        /// </summary>
        public static string? ByteArrayToString(byte[]? bytes)
        {
            // If we get null in, we send null out
            if (bytes == null)
                return null;

            try
            {
                string hex = BitConverter.ToString(bytes);
                return hex.Replace("-", string.Empty).ToLowerInvariant();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert a hex string to a byte array
        /// </summary>
        public static byte[]? StringToByteArray(string? hex)
        {
            // If we get null in, we send null out
            if (string.IsNullOrEmpty(hex))
                return null;

            try
            {
                int NumberChars = hex!.Length;
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }

                return bytes;
            }
            catch
            {
                return null;
            }
        }
    }
}