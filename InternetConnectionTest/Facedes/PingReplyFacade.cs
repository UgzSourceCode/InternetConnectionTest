using AutoMapper;
using InternetConnectionTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;

namespace InternetConnectionTest.Facedes
{
    public class PingReplyFacade : IPingReply
    {
        #region Properties
        public IPAddress Address { get; set; }
        public byte[] Buffer { get; set; }
        public PingOptions Options { get; set; }
        public long RoundtripTime { get; set; }
        public IPStatus Status { get; set; }
        #endregion
    }
}
