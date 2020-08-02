using InternetConnectionTest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetConnectionTest.Interfaces
{
    public interface ILogController
    {
        void Warn(string content);
        void Error(string content);
        void Info(string content);
        void UnsuccessPing(PingResultDTO pingResult);
        void UnsuccessPing(IList<PingResultDTO> pingResults);
        void Ping(IList<PingResultDTO> pingResults);
    }
}
