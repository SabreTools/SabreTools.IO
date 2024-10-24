using System;
using System.IO;
using System.Text;
using SabreTools.IO.Extensions;

namespace SabreTools.IO.Logging
{
    /// <summary>
    /// Internal logging implementation
    /// </summary>
    public static class LoggerImpl
    {
        #region Fields

        /// <summary>
        /// Optional output filename for logs
        /// </summary>
        public static string? Filename { get; private set; } = null;

        /// <summary>
        /// Determines if we're logging to file or not
        /// </summary>
        public static bool LogToFile { get { return !string.IsNullOrEmpty(Filename); } }

        /// <summary>
        /// Optional output log directory
        /// </summary>
        public static string? LogDirectory { get; private set; } = null;

        /// <summary>
        /// Determines the lowest log level to output
        /// </summary>
        public static LogLevel LowestLogLevel { get; set; } = LogLevel.VERBOSE;

        /// <summary>
        /// Determines whether to prefix log lines with level and datetime
        /// </summary>
        public static bool AppendPrefix { get; set; } = true;

        /// <summary>
        /// Determines whether to throw if an exception is logged
        /// </summary>
        public static bool ThrowOnError { get; set; } = false;

        /// <summary>
        /// Logging start time for metrics
        /// </summary>
        public static DateTime StartTime { get; private set; }

        /// <summary>
        /// Determines if there were errors logged
        /// </summary>
        public static bool LoggedErrors { get; private set; } = false;

        /// <summary>
        /// Determines if there were warnings logged
        /// </summary>
        public static bool LoggedWarnings { get; private set; } = false;

        #endregion

        #region Private variables

        /// <summary>
        /// StreamWriter representing the output log file
        /// </summary>
        private static StreamWriter? _log;

        /// <summary>
        /// Object lock for multithreaded logging
        /// </summary>
        private static readonly object _lock = new();

        #endregion

        #region Control

        /// <summary>
        /// Generate and set the log filename
        /// </summary>
        /// <param name="filename">Base filename to use</param>
        /// <param name="addDate">True to append a date to the filename, false otherwise</param>
        public static void SetFilename(string filename, bool addDate = true)
        {
            // Get the full log path
            string fullPath = Path.GetFullPath(filename);

            // Set the log directory
            LogDirectory = Path.GetDirectoryName(fullPath);

            // Set the 
            if (addDate)
                Filename = $"{Path.GetFileNameWithoutExtension(fullPath)} ({DateTime.Now:yyyy-MM-dd HH-mm-ss}).{fullPath.GetNormalizedExtension()}";
            else
                Filename = Path.GetFileName(fullPath);
        }

        /// <summary>
        /// Start logging by opening output file (if necessary)
        /// </summary>
        /// <returns>True if the logging was started correctly, false otherwise</returns>
        public static bool Start()
        {
            // Setup the logging handler to always use the internal log
            LogEventHandler += HandleLogEvent;

            // Start the logging
            StartTime = DateTime.Now;
            if (!LogToFile)
                return true;

            // Setup file output and perform initial log
            try
            {
                if (!string.IsNullOrEmpty(LogDirectory) && !Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                FileStream logfile = File.Create(Path.Combine(LogDirectory ?? string.Empty, Filename ?? string.Empty));
#if NET20 || NET35 || NET40
                _log = new StreamWriter(logfile, Encoding.UTF8, 4096)
#else
                _log = new StreamWriter(logfile, Encoding.UTF8, 4096, true)
#endif
                {
                    AutoFlush = true
                };

                _log.WriteLine($"Logging started {StartTime:yyyy-MM-dd HH:mm:ss}");
                _log.WriteLine($"Command run: {string.Join(" ", Environment.GetCommandLineArgs())}");
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// End logging by closing output file (if necessary)
        /// </summary>
        /// <param name="suppress">True if all ending output is to be suppressed, false otherwise (default)</param>
        /// <returns>True if the logging was ended correctly, false otherwise</returns>
        public static bool Close(bool suppress = false)
        {
            if (!suppress)
            {
                if (LoggedWarnings)
                    Console.WriteLine("There were warnings in the last run! Check the log for more details");

                if (LoggedErrors)
                    Console.WriteLine("There were errors in the last run! Check the log for more details");

                TimeSpan span = DateTime.Now.Subtract(StartTime);

#if NET20 || NET35
                string total = span.ToString();
#else
                // Special case for multi-day runs
                string total;
                if (span >= TimeSpan.FromDays(1))
                    total = span.ToString(@"d\:hh\:mm\:ss");
                else
                    total = span.ToString(@"hh\:mm\:ss");
#endif

                if (!LogToFile)
                {
                    Console.WriteLine($"Total runtime: {total}");
                    return true;
                }

                try
                {
                    _log?.WriteLine($"Logging ended {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    _log?.WriteLine($"Total runtime: {total}");
                    Console.WriteLine($"Total runtime: {total}");
                    _log?.Close();
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    _log?.Close();
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Handler for log events
        /// </summary>
        public static event LogEventHandler LogEventHandler = delegate { };

        /// <summary>
        /// Default log event handling
        /// </summary>
        public static void HandleLogEvent(object? sender, LogEventArgs args)
        {
            // Null args means we can't handle it
            if (args == null)
                return;

            // If we have an exception and we're throwing on that
            if (ThrowOnError && args.Exception != null)
                throw args.Exception;

            // If we have a warning or error, set the flags accordingly
            if (args.LogLevel == LogLevel.WARNING)
                LoggedWarnings = true;
            if (args.LogLevel == LogLevel.ERROR)
                LoggedErrors = true;

            // Setup the statement based on the inputs
            string logLine;
            if (args.Exception != null)
            {
                logLine = $"{(args.Statement != null ? args.Statement + ": " : string.Empty)}{args.Exception}";
            }
            else if (args.TotalCount != null && args.CurrentCount != null)
            {
                double percentage = ((double)args.CurrentCount.Value / args.TotalCount.Value) * 100;
                logLine = $"{percentage:N2}%{(args.Statement != null ? ": " + args.Statement : string.Empty)}";
            }
            else
            {
                logLine = args.Statement ?? string.Empty;
            }

            // Then write to the log
            Log(logLine, args.LogLevel);
        }

        /// <summary>
        /// Write the given string to the log output
        /// </summary>
        /// <param name="output">String to be written log</param>
        /// <param name="loglevel">Severity of the information being logged</param>
        private static void Log(string output, LogLevel loglevel)
        {
            // If the log level is less than the filter level, we skip it but claim we didn't
            if (loglevel < LowestLogLevel)
                return;

            // USER and ERROR writes to console
            if (loglevel == LogLevel.USER || loglevel == LogLevel.ERROR)
                Console.WriteLine((loglevel == LogLevel.ERROR && AppendPrefix ? loglevel.ToString() + " " : string.Empty) + output);

            // If we're writing to file, use the existing stream
            if (LogToFile)
            {
                try
                {
                    lock (_lock)
                    {
                        _log?.WriteLine((AppendPrefix ? $"{loglevel} - {DateTime.Now} - " : string.Empty) + output);
                    }
                }
                catch (Exception ex) when (ThrowOnError)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Could not write to log file!");
                    return;
                }
            }

            return;
        }

        #endregion

        #region Log Event Triggers

        #region Verbose

        /// <summary>
        /// Write the given string as a verbose message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Verbose(object? instance, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.VERBOSE, output));

        /// <summary>
        /// Write the given exception as a verbose message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Verbose(object? instance, Exception ex)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.VERBOSE, ex));

        /// <summary>
        /// Write the given exception and string as a verbose message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Verbose(object? instance, Exception ex, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.VERBOSE, output, ex));

        /// <summary>
        /// Write the given verbose progress message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public static void Verbose(object? instance, long total, long current, string? output = null)
            => LogEventHandler(instance, new LogEventArgs(total, current, LogLevel.VERBOSE, output));

        #endregion

        #region User

        /// <summary>
        /// Write the given string as a user message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void User(object? instance, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.USER, output));

        /// <summary>
        /// Write the given exception as a user message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void User(object? instance, Exception ex)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.USER, ex));

        /// <summary>
        /// Write the given exception and string as a user message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void User(object? instance, Exception ex, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.USER, output, ex));

        /// <summary>
        /// Write the given user progress message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public static void User(object? instance, long total, long current, string? output = null)
            => LogEventHandler(instance, new LogEventArgs(total, current, LogLevel.USER, output));

        #endregion

        #region Warning

        /// <summary>
        /// Write the given string as a warning to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Warning(object? instance, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.WARNING, output));

        /// <summary>
        /// Write the given exception as a warning to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Warning(object? instance, Exception ex)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.WARNING, ex));

        //// <summary>
        /// Write the given exception and string as a warning to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Warning(object? instance, Exception ex, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.WARNING, output, ex));

        /// <summary>
        /// Write the given warning progress message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public static void Warning(object? instance, long total, long current, string? output = null)
            => LogEventHandler(instance, new LogEventArgs(total, current, LogLevel.WARNING, output));

        #endregion

        #region Error

        /// <summary>
        /// Writes the given string as an error in the log
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Error(object? instance, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.ERROR, output));

        /// <summary>
        /// Writes the given exception as an error in the log
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Error(object? instance, Exception ex)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.ERROR, ex));

        /// <summary>
        /// Writes the given exception and string as an error in the log
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="ex">Exception to be written log</param>
        /// <param name="output">String to be written log</param>
        /// <returns>True if the output could be written, false otherwise</returns>
        public static void Error(object? instance, Exception ex, string output)
            => LogEventHandler(instance, new LogEventArgs(LogLevel.ERROR, output, ex));

        /// <summary>
        /// Write the given error progress message to the log output
        /// </summary>
        /// <param name="instance">Instance object that's the source of logging</param>
        /// <param name="total">Total count for progress</param>
        /// <param name="current">Current count for progres</param>
        /// <param name="output">String to be written log</param>
        public static void Error(object? instance, long total, long current, string? output = null)
            => LogEventHandler(instance, new LogEventArgs(total, current, LogLevel.ERROR, output));

        #endregion

        #endregion
    }
}
