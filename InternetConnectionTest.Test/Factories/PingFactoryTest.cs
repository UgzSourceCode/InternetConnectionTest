using AutoMapper;
using AutoMapper.Configuration;
using InternetConnectionTest.Factories;
using InternetConnectionTest.Interfaces;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Test.Factories
{
    class PingFactoryTest
    {
        private IMapper _mapper;

        public PingFactoryTest()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<PingReply, IPingReply>();
            var mapperCfg = new MapperConfiguration(cfg);
            this._mapper = new Mapper(mapperCfg);
        }

        [Test]
        public void CreateNewPingResultIsNotNull()
        {
            IPingFactory pingFactory = new PingFactory(this._mapper);

            IPing ping = pingFactory.CreateNewPing();

            Assert.IsNotNull(ping);
        }

        [Test]
        public void CreateNewPingResultIsCorrectType()
        {
            IPingFactory pingFactory = new PingFactory(this._mapper);

            IPing ping = pingFactory.CreateNewPing();

            Assert.IsInstanceOf(typeof(IPing), ping);
        }

        [Test]
        public void CreateNewPingOptionsResultIsNotNull()
        {
            IPingFactory pingFactory = new PingFactory(this._mapper);

            PingOptions pingOptions = pingFactory.CreateNewPingOptions();

            Assert.IsNotNull(pingOptions);
        }

        [Test]
        public void CreateNewPingOptionsResultIsCorrectType()
        {
            IPingFactory pingFactory = new PingFactory(this._mapper);

            PingOptions pingOptions = pingFactory.CreateNewPingOptions();

            Assert.IsInstanceOf(typeof(PingOptions), pingOptions);
        }
    }
}
