using InternetConnectionTest.Controllers;
using InternetConnectionTest.DTO;
using InternetConnectionTest.Interfaces;
using Moq;
using NLog;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetConnectionTest.Test.Controllers
{
    class LogControllerTest
    {
        [Test]
        public void LogControllerIsNotNull()
        {
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);

            Assert.IsNotNull(logController);
        }

        [TestCase("")]
        [TestCase("Error")]
        [TestCase(null)]
        public void ErrorWasWritten(string input)
        {
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);
            string output = string.Empty;
            mockLogger.Setup(e => e.Error(It.IsAny<string>())).Callback<string>(msg => { output = msg; });
            logController.Error(input);
            
            Assert.AreEqual(input, output);
        }

        [TestCase("")]
        [TestCase("Info")]
        [TestCase(null)]
        public void InfoWasWritten(string input)
        {
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);
            string output = string.Empty;
            mockLogger.Setup(e => e.Info(It.IsAny<string>())).Callback<string>(msg => { output = msg; });
            logController.Info(input);

            Assert.AreEqual(input, output);
        }

        [TestCase("")]
        [TestCase("Warn")]
        [TestCase(null)]
        public void WarnWasWritten(string input)
        {
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);
            string output = string.Empty;
            mockLogger.Setup(e => e.Warn(It.IsAny<string>())).Callback<string>(msg => { output = msg; });
            logController.Warn(input);

            Assert.AreEqual(input, output);
        }

        [Test]
        public void SaveUnsuccessPingWithCorrectResult()
        {
            PingResultDTO input = new PingResultDTO()
            {
                Host = "test.host",
                Success = true
            };
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);
            string output = string.Empty;
            mockLogger.Setup(e => e.Warn(It.IsNotNull<string>())).Callback<string>(msg => { output = "WARN|" + msg; });
            mockLogger.Setup(e => e.Info(It.IsNotNull<string>())).Callback<string>(msg => { output = "INFO|" + msg; });
            logController.UnsuccessPing(input);

            Assert.AreEqual(string.Empty, output);
        }

        [Test]
        public void SaveUnsuccessPingWithUncorrectResult()
        {
            PingResultDTO input = new PingResultDTO()
            {
                Host = "test.host",
                Success = false
            };
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);
            string output = string.Empty;
            mockLogger.Setup(e => e.Warn(It.IsNotNull<string>())).Callback<string>(msg => { output = "WARN|" + msg; });
            mockLogger.Setup(e => e.Info(It.IsNotNull<string>())).Callback<string>(msg => { output = "INFO|" + msg; });
            logController.UnsuccessPing(input);

            Assert.AreEqual("WARN|FAIL|test.host", output);
        }

        [Test]
        public void SaveUnsuccessPingWithCollectionOfResult()
        {
            IList<PingResultDTO> input = new List<PingResultDTO>()
            {
                new PingResultDTO()
                {
                    Host = "test.host",
                    Success = false
                },
                new PingResultDTO()
                {
                    Host = "test.host",
                    Success = true
                },
            };
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);
            string output = string.Empty;
            mockLogger.Setup(e => e.Warn(It.IsNotNull<string>())).Callback<string>(msg => { output += "WARN|" + msg; });
            mockLogger.Setup(e => e.Info(It.IsNotNull<string>())).Callback<string>(msg => { output += "INFO|" + msg; });
            logController.UnsuccessPing(input);

            Assert.AreEqual("WARN|FAIL|test.host", output);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SavePingResult(bool success)
        {
            IList<PingResultDTO> input = new List<PingResultDTO>(){ 
                new PingResultDTO()
                {
                    Host = "test.host",
                    Success = success
                } 
            };
            var mockLogger = new Mock<ILogger>();

            ILogController logController = new LogController(mockLogger.Object);
            string output = string.Empty;
            mockLogger.Setup(e => e.Warn(It.IsNotNull<string>())).Callback<string>(msg => { output = "WARN|" + msg; });
            mockLogger.Setup(e => e.Info(It.IsNotNull<string>())).Callback<string>(msg => { output = "INFO|" + msg; });
            logController.Ping(input);

            string expected = (success ? "INFO|SUCCESS|" : "WARN|FAIL|") + input[0].Host;
            Assert.AreEqual(expected, output);
        }
    }
}
