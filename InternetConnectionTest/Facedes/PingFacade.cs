using AutoMapper;
using InternetConnectionTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Facedes
{
    public class PingFacade : IPing
    {
        #region Fields
        private Ping _ping;
        private IMapper _mapper;
        #endregion

        #region Constuctors
        public PingFacade(IMapper mapper, Ping ping)
        {
            this._ping = ping;
            this._mapper = mapper;
        }
        #endregion

        #region Public methods
        public IPingReply Send(string host, int timeout, byte[] buffer, PingOptions options)
        {
            PingReply pingReply = this._ping.Send(host, timeout, buffer, options);

            return this._mapper.Map<PingReply, IPingReply>(pingReply);
        } 
        #endregion
    }
}
