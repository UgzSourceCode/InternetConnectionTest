using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Interfaces
{
    public interface IPing
    {
        public IPingReply Send(string host, int timeout, byte[] buffer, PingOptions options);
    }
}
