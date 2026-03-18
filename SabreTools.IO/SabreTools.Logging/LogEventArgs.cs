using System;

namespace SabreTools.Logging
{
    /// <summary>
    /// Generic delegate type for log events
    /// </summary>
    public delegate void LogEventHandler(object? sender, LogEventArgs args);

    /// <summary>
    /// Logging specific event arguments
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// LogLevel for the event
        /// </summary>
        public readonly LogLevel LogLevel;

        /// <summary>
        /// Log statement to be printed
        /// </summary>
        public readonly string? Statement = null;

        /// <summary>
        /// Exception to be passed along to the event handler
        /// </summary>
        public readonly Exception? Exception = null;

        /// <summary>
        /// Total count for progress log events
        /// </summary>
        public readonly long? TotalCount = null;

        /// <summary>
        /// Current count for progress log events
        /// </summary>
        public readonly long? CurrentCount = null;

        /// <summary>
        /// Statement constructor
        /// </summary>
        public LogEventArgs(LogLevel logLevel, string statement)
        {
            LogLevel = logLevel;
            Statement = statement;
        }

        /// <summary>
        /// Statement constructor
        /// </summary>
        public LogEventArgs(LogLevel logLevel, Exception exception)
        {
            LogLevel = logLevel;
            Exception = exception;
        }

        /// <summary>
        /// Statement and exception constructor
        /// </summary>
        public LogEventArgs(LogLevel logLevel, string statement, Exception exception)
        {
            LogLevel = logLevel;
            Statement = statement;
            Exception = exception;
        }

        /// <summary>
        /// Progress constructor
        /// </summary>
        public LogEventArgs(long total, long current, LogLevel logLevel, string? statement = null)
        {
            LogLevel = logLevel;
            Statement = statement;
            TotalCount = total;
            CurrentCount = current;
        }
    }
}
