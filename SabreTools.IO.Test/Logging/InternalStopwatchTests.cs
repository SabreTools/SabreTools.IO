using SabreTools.IO.Logging;
using Xunit;

namespace SabreTools.IO.Test.Logging
{
    public class InternalStopwatchTests
    {
        [Fact]
        public void Stopwatch_NoSubject_StartNoSubject()
        {
            var stopwatch = new InternalStopwatch();
            stopwatch.Start();
            stopwatch.Stop();
        }

        [Fact]
        public void Stopwatch_NoSubject_StartSubject()
        {
            var stopwatch = new InternalStopwatch();
            stopwatch.Start("start");
            stopwatch.Stop();
        }

        [Fact]
        public void Stopwatch_Subject_StartNoSubject()
        {
            var stopwatch = new InternalStopwatch("init");
            stopwatch.Start();
            stopwatch.Stop();
        }

        [Fact]
        public void Stopwatch_Subject_StartSubject()
        {
            var stopwatch = new InternalStopwatch("init");
            stopwatch.Start("start");
            stopwatch.Stop();
        }
    }
}