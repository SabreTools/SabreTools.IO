namespace SabreTools.IO.Logging
{
    public static class Converters
    {
        #region String to Enum

        /// <summary>
        /// Get the LogLevel value for an input string, if possible
        /// </summary>
        /// <param name="value">String value to parse/param>
        /// <returns></returns>
        public static LogLevel AsLogLevel(this string? value)
        {
            return value?.ToLowerInvariant() switch
            {
                "verbose" => LogLevel.VERBOSE,
                "user" => LogLevel.USER,
                "warning" => LogLevel.WARNING,
                "error" => LogLevel.ERROR,
                _ => LogLevel.VERBOSE,
            };
        }

        #endregion

        #region Enum to String

        /// <summary>
        /// Get string value from input LogLevel
        /// </summary>
        /// <param name="value">LogLevel to get value from</param>
        /// <returns>String corresponding to the LogLevel</returns>
        public static string? FromLogLevel(this LogLevel value)
        {
            return value switch
            {
                LogLevel.VERBOSE => "VERBOSE",
                LogLevel.USER => "USER",
                LogLevel.WARNING => "WARNING",
                LogLevel.ERROR => "ERROR",
                _ => null,
            };
        }

        #endregion
    }
}
