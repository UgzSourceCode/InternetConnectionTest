using InternetConnectionTest.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;
using InternetConnectionTest.Interfaces;

namespace InternetConnectionTest.Controllers
{
    public class ConfigController : IConfigController
    {
        #region Fields
        private IConfiguration _configuration;
        #endregion

        #region Properties
        private ConfigDTO _config;
        public ConfigDTO Config
        {
            get { return _config; }
            private set { _config = value; }
        }
        #endregion

        #region Constructors
        public ConfigController()
        {
            this._configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            this.Config = new ConfigDTO();
            this._configuration.Bind("AppConfig", this.Config);
        }
        #endregion
    }
}
