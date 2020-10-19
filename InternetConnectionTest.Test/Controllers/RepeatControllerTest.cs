using InternetConnectionTest.Controllers;
using InternetConnectionTest.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InternetConnectionTest.Test.Controllers
{
    class RepeatControllerTest
    {
        [Test]
        public void RepeatControllerIsNotNull()
        {
            Mock<IConfigController> mockConfigController = new Mock<IConfigController>();
            mockConfigController.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                RepeatConfig = new DTO.RepeatConfigDTO()
                {
                    Delay = 1,
                    NumberOfRepetitions = 1
                }
            });

            IRepeatController repeatController = new RepeatController(mockConfigController.Object);

            Assert.IsNotNull(repeatController);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(15)]
        public void QuantityRepeat(int quantity)
        {
            Mock<IConfigController> mockConfigController = new Mock<IConfigController>();
            mockConfigController.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                RepeatConfig = new DTO.RepeatConfigDTO()
                {
                    Delay = 1,
                    NumberOfRepetitions = quantity
                }
            });
            
            IRepeatController repeatController = new RepeatController(mockConfigController.Object);
            int i = 0;
            repeatController.Start(() => {
                i++;
            });

            Assert.AreEqual(quantity, i);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void TimeOfRelay(int milisec)
        {
            Mock<IConfigController> mockConfigController = new Mock<IConfigController>();
            mockConfigController.SetupGet(cfg => cfg.Config).Returns(new DTO.ConfigDTO()
            {
                RepeatConfig = new DTO.RepeatConfigDTO()
                {
                    Delay = milisec,
                    NumberOfRepetitions = 1
                }
            });

            IRepeatController repeatController = new RepeatController(mockConfigController.Object);
            Stopwatch stopwatch = new Stopwatch();
            repeatController.Start(() => {
                stopwatch.Start();
            });
            stopwatch.Stop();

            int toleration = 50;
            Assert.IsTrue(milisec <= stopwatch.ElapsedMilliseconds && milisec + toleration >= stopwatch.ElapsedMilliseconds);
        }
    }
}
