using InternetConnectionTest.Controllers;
using InternetConnectionTest.DTO;
using InternetConnectionTest.Facedes;
using InternetConnectionTest.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Test.Controllers
{
    class PingControllerTest
    {
        [Test]
        public void PingControllerIsNotNull()
        {
            Mock<IConfigController> mockConfigControler = new Mock<IConfigController>();
            mockConfigControler.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                Hosts = new List<string>()
            });
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();

            IPingController pingController = new PingController(mockConfigControler.Object, mockPingFactory.Object);

            Assert.IsNotNull(pingController);
        }

        [Test]
        public void HostsIsNotNull()
        {
            Mock<IConfigController> mockConfigControler = new Mock<IConfigController>();
            mockConfigControler.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                Hosts = new List<string>()
            });
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();

            IPingController pingController = new PingController(mockConfigControler.Object, mockPingFactory.Object);

            Assert.IsNotNull(pingController.Hosts);
        }

        [Test]
        public void HostsIsCorrectList()
        {
            string[] hosts = new String[] { "google", "facebook" };
            Mock<IConfigController> mockConfigControler = new Mock<IConfigController>();
            mockConfigControler.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                Hosts = hosts.ToList()
            });
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();

            IPingController pingController = new PingController(mockConfigControler.Object, mockPingFactory.Object);

            Assert.AreEqual(pingController.Hosts, hosts.ToList());
        }

        [Test]
        public void PingSuccess()
        {
            string[] hosts = new String[] { "google" };
            Mock<IConfigController> mockConfigControler = new Mock<IConfigController>();
            mockConfigControler.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                Hosts = hosts.ToList()
            });
            Mock<IPing> mockPing = new Mock<IPing>();
            mockPing.Setup(p => p.Send(It.IsNotNull<string>(), It.IsAny<int>(), It.IsAny<byte[]>(), It.IsAny<PingOptions>())).Returns(new PingReplyFacade() { Status = IPStatus.Success });
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();
            mockPingFactory.Setup(pf => pf.CreateNewPing()).Returns(mockPing.Object);

            IPingController pingController = new PingController(mockConfigControler.Object, mockPingFactory.Object);
            IList<PingResultDTO> result = pingController.DoForAll();

            Assert.IsNotNull(result.First());
            Assert.IsTrue(result.First().Success);
        }

        [Test]
        public void PingFailed()
        {
            string[] hosts = new String[] { "google" };
            Mock<IConfigController> mockConfigControler = new Mock<IConfigController>();
            mockConfigControler.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                Hosts = hosts.ToList()
            });
            Mock<IPing> mockPing = new Mock<IPing>();
            mockPing.Setup(p => p.Send(It.IsNotNull<string>(), It.IsAny<int>(), It.IsAny<byte[]>(), It.IsAny<PingOptions>())).Returns(new PingReplyFacade() { Status = IPStatus.TimedOut });
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();
            mockPingFactory.Setup(pf => pf.CreateNewPing()).Returns(mockPing.Object);

            IPingController pingController = new PingController(mockConfigControler.Object, mockPingFactory.Object);
            IList<PingResultDTO> result = pingController.DoForAll();

            Assert.IsNotNull(result.First());
            Assert.IsFalse(result.First().Success);
        }

        [Test]
        public void NumberOfExecutionsDoItAll()
        {
            string[] hosts = new String[] { "google", "ugz", "polishdev.com" };
            Mock<IConfigController> mockConfigControler = new Mock<IConfigController>();
            mockConfigControler.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                Hosts = hosts.ToList()
            });
            Mock<IPing> mockPing = new Mock<IPing>();
            int numberOfPingExecutions = 0;
            mockPing.Setup(p => p.Send(It.IsNotNull<string>(), It.IsAny<int>(), It.IsAny<byte[]>(), It.IsAny<PingOptions>())).Returns(new PingReplyFacade() { Status = IPStatus.Success }).Callback(() => { numberOfPingExecutions++; });
            Mock<IPingFactory> mockPingFactory = new Mock<IPingFactory>();
            mockPingFactory.Setup(pf => pf.CreateNewPing()).Returns(mockPing.Object);

            IPingController pingController = new PingController(mockConfigControler.Object, mockPingFactory.Object);
            IList<PingResultDTO> result = pingController.DoForAll();

            Assert.AreEqual(hosts.Count(), numberOfPingExecutions);
        }
    }
}
