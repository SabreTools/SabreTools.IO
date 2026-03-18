using System;

namespace SabreTools.Logging
{
    /// <summary>
    /// Stopwatch class for keeping track of duration in the code
    /// </summary>
    public class InternalStopwatch
    {
        private string _subject;
        private DateTime _startTime;
        private readonly Logger _logger;

        /// <summary>
        /// Constructor that initalizes the stopwatch
        /// </summary>
        public InternalStopwatch()
        {
            _subject = string.Empty;
            _logger = new Logger(this);
        }

        /// <summary>
        /// Constructor that initalizes the stopwatch with a subject and starts immediately
        /// </summary>
        /// <param name="subject">Subject of the stopwatch</param>
        public InternalStopwatch(string subject)
        {
            _subject = subject;
            _logger = new Logger(this);
            Start();
        }

        /// <summary>
        /// Start the stopwatch and display subject text
        /// </summary>
        public void Start()
        {
            _startTime = DateTime.Now;
            _logger.User($"{_subject}...");
        }

        /// <summary>
        /// Start the stopwatch and display subject text
        /// </summary>
        /// <param name="subject">Text to show on stopwatch start</param>
        public void Start(string subject)
        {
            _subject = subject;
            Start();
        }

        /// <summary>
        /// End the stopwatch and display subject text
        /// </summary>
        public void Stop()
        {
            _logger.User($"{_subject} completed in {DateTime.Now.Subtract(_startTime):G}");
        }
    }
}
