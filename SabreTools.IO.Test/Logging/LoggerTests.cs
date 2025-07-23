using System;
using SabreTools.IO.Logging;
using Xunit;

namespace SabreTools.IO.Test.Logging
{
    public class LoggerTests
    {
        [Fact]
        public void EndToEnd()
        {
            Assert.Null(LoggerImpl.Filename);
            Assert.False(LoggerImpl.LogToFile);
            Assert.Null(LoggerImpl.LogDirectory);
            Assert.True(LoggerImpl.AppendPrefix);
            Assert.False(LoggerImpl.ThrowOnError);

            LoggerImpl.Start();

            var logger = new Logger();

            logger.Verbose("verbose");
            logger.Verbose(new Exception());
            logger.Verbose(new Exception(), "verbose");
            logger.Verbose(1, 1, "verbose");

            logger.User("user");
            logger.User(new Exception());
            logger.User(new Exception(), "user");
            logger.User(1, 1, "user");

            logger.Warning("warning");
            logger.Warning(new Exception());
            logger.Warning(new Exception(), "warning");
            logger.Warning(1, 1, "warning");

            logger.Error("error");
            logger.Error(new Exception());
            logger.Error(new Exception(), "error");
            logger.Error(1, 1, "error");

            LoggerImpl.ThrowOnError = true;
            Assert.Throws<Exception>(() => logger.Error(new Exception()));

            Assert.True(LoggerImpl.StartTime < DateTime.Now);
            Assert.True(LoggerImpl.LoggedWarnings);
            Assert.True(LoggerImpl.LoggedErrors);

            LoggerImpl.SetFilename("logfile.txt", addDate: true);

            LoggerImpl.Close();
        }
    }
}
