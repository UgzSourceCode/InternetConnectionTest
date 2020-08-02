using InternetConnectionTest.Controllers;
using InternetConnectionTest.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetConnectionTest.Test.Controllers
{
    class ConfigControllerTest
    {
        [Test]
        public void ConfigControllerIsNotNull()
        {
            IConfigController configController = new ConfigController();
            
            Assert.IsNotNull(configController);
        }

        [Test]
		public void IsConfigExist()
        {
            IConfigController configController = new ConfigController();

            Assert.IsNotNull(configController.Config);
        }
	}
}
