using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Interfaces
{
    public interface IPingReply
    {
        public IPAddress Address { get; set; }
        public byte[] Buffer { get; set; }
        public PingOptions Options { get; set; }
        public long RoundtripTime { get; set; }
        public IPStatus Status { get; set; }
    }
}
