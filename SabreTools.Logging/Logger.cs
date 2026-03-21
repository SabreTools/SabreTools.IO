using System;

namespace SabreTools.Logging
{
    /// <summary>
    /// Per-class logging
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Instance associated with this logger
        /// </summary>
        /// TODO: Derive class name for this object, if possible
        private readonly object? _instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public Logger(object? instance = null)
        {
            _instance = instance;
        }

        #region Log Event Triggers

        #region Verbose

        /// <summary>
        /// Write the given string as a verbose message to the log output
        /// </summary>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Verbose(string output)
            => LoggerImpl.Verbose(_instance, output);

        /// <summary>
        /// Write the given exception as a verbose message to the log output
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Verbose(Exception ex)
            => LoggerImpl.Verbose(_instance, ex);

        /// <summary>
        /// Write the given exception and string as a verbose message to the log output
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Verbose(Exception ex, string output)
            => LoggerImpl.Verbose(_instance, ex, output);

        /// <summary>
        /// Write the given verbose progress message to the log output
        /// </summary>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public void Verbose(long total, long current, string? output = null)
            => LoggerImpl.Verbose(_instance, total, current, output);

        #endregion

        #region User

        /// <summary>
        /// Write the given string as a user message to the log output
        /// </summary>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void User(string output)
            => LoggerImpl.User(_instance, output);

        /// <summary>
        /// Write the given exception as a user message to the log output
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void User(Exception ex)
            => LoggerImpl.User(_instance, ex);

        /// <summary>
        /// Write the given exception and string as a user message to the log output
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void User(Exception ex, string output)
            => LoggerImpl.User(_instance, ex, output);

        /// <summary>
        /// Write the given user progress message to the log output
        /// </summary>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public void User(long total, long current, string? output = null)
            => LoggerImpl.User(_instance, total, current, output);

        #endregion

        #region Warning

        /// <summary>
        /// Write the given string as a warning to the log output
        /// </summary>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Warning(string output)
            => LoggerImpl.Warning(_instance, output);

        /// <summary>
        /// Write the given exception as a warning to the log output
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Warning(Exception ex)
            => LoggerImpl.Warning(_instance, ex);

        /// <summary>
        /// Write the given exception and string as a warning to the log output
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Warning(Exception ex, string output)
            => LoggerImpl.Warning(_instance, ex, output);

        /// <summary>
        /// Write the given warning progress message to the log output
        /// </summary>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public void Warning(long total, long current, string? output = null)
            => LoggerImpl.Warning(_instance, total, current, output);

        #endregion

        #region Error

        /// <summary>
        /// Writes the given string as an error in the log
        /// </summary>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Error(string output)
            => LoggerImpl.Error(_instance, output);

        /// <summary>
        /// Writes the given exception as an error in the log
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Error(Exception ex)
            => LoggerImpl.Error(_instance, ex);

        /// <summary>
        /// Writes the given exception and string as an error in the log
        /// </summary>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public void Error(Exception ex, string output)
            => LoggerImpl.Error(_instance, ex, output);

        /// <summary>
        /// Write the given error progress message to the log output
        /// </summary>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public void Error(long total, long current, string? output = null)
            => LoggerImpl.Error(_instance, total, current, output);

        #endregion

        #endregion
    }
}
