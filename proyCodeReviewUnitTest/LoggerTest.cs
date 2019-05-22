using Microsoft.VisualStudio.TestTools.UnitTesting;
using proyCodeReview.Logic;

namespace proyCodeReviewUnitTest
{
    [TestClass]
    public class LoggerTest
    {
        readonly JobLoggerLogic _oJobLoggerLogic = new JobLoggerLogic();

        [TestMethod]
        public void LoggerTestMessage()
        {
            const string logMessage = "LoggerTestMessage";
            var logIdMessage = _oJobLoggerLogic.LogMessage(logMessage, JobLoggerLogic.LogType.Message);
            Assert.AreNotEqual("-1", logIdMessage);
        }

        [TestMethod]
        public void LoggerTestWarning()
        {
            const string logWarning = "LoggerTestWarning";
            var logIdWarning = _oJobLoggerLogic.LogMessage(logWarning, JobLoggerLogic.LogType.Warning);
            Assert.AreNotEqual("-1", logIdWarning);
        }

        [TestMethod]
        public void LoggerTestError()
        {
            const string logError = "LoggerTestError";
            var logIdError = _oJobLoggerLogic.LogMessage(logError, JobLoggerLogic.LogType.Error);
            Assert.AreNotEqual("-1", logIdError);
        }
    }
}
