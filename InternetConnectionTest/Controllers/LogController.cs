using InternetConnectionTest.DTO;
using InternetConnectionTest.Interfaces;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetConnectionTest.Controllers
{
    public class LogController : ILogController
    {
        #region Fields
        private ILogger _logger;
        #endregion

        #region Constructors
        public LogController(ILogger logger)
        {
            this._logger = logger;
        }

        ~LogController()
        {
            LogManager.Shutdown();
        }
        #endregion

        #region Public methods
        public void Error(string content)
        {
            this._logger.Error(content);
        }

        public void Info(string content)
        {
            this._logger.Info(content);
        }

        public void Warn(string content)
        {
            this._logger.Warn(content);
        }

        public void UnsuccessPing(PingResultDTO pingResult)
        {
            if (pingResult != null)
                PingStatus(pingResult, false);
        }

        public void UnsuccessPing(IList<PingResultDTO> pingResults)
        {
            if (pingResults != null)
            {
                foreach(var item in pingResults)
                    UnsuccessPing(item);
            }
        }

        public void Ping(IList<PingResultDTO> pingResults)
        {
            if (pingResults != null )
            {
                foreach(var item in pingResults)
                {
                    if (item != null)
                        PingStatus(item);
                }
            }
        }
        #endregion

        #region Private methods
        private void PingStatus(PingResultDTO pingResult, bool? statusReqired = null)
        {
            if (pingResult != null & (statusReqired == null | pingResult.Success == statusReqired))
            {
                string message = (pingResult.Success ? "SUCCESS" : "FAIL") + "|" + pingResult.Host;
                if (pingResult.Success == false)
                    this.Warn(message);
                else
                    this.Info(message);
            }
        }
        #endregion
    }
}