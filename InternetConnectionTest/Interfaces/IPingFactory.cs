using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Interfaces
{
    public interface IPingFactory
    {
        public IPing CreateNewPing();
        public PingOptions CreateNewPingOptions();
    }
}
