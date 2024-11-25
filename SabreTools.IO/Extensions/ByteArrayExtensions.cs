using System;

namespace SabreTools.IO.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Indicates whether the specified array is null or has a length of zero
        /// </summary>
        public static bool IsNullOrEmpty(this Array? array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Convert a byte array to a hex string
        /// </summary>
        public static string? ToHexString(this byte[]? bytes)
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
        public static byte[]? FromHexString(this string? hex)
        {
            // If we get null in, we send null out
            if (string.IsNullOrEmpty(hex))
                return null;

            try
            {
                int chars = hex!.Length;
                byte[] bytes = new byte[chars / 2];
                for (int i = 0; i < chars; i += 2)
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