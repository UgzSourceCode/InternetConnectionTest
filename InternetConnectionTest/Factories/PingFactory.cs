using AutoMapper;
using InternetConnectionTest.Facedes;
using InternetConnectionTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Factories
{
    public class PingFactory : IPingFactory
    {
        #region Fields
        private IMapper _mapper;
        #endregion

        #region Constructors
        public PingFactory(IMapper mapper)
        {
            this._mapper = mapper;
        }
        #endregion

        public IPing CreateNewPing()
        {
            return new PingFacade(this._mapper, new Ping());
        }

        public PingOptions CreateNewPingOptions()
        {
            return new PingOptions();
        }
    }
}