using InternetConnectionTest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetConnectionTest.Interfaces
{
    public interface IPingController
    {
        public IList<string> Hosts { get; }
        public IList<PingResultDTO> DoForAll();
    }
}
