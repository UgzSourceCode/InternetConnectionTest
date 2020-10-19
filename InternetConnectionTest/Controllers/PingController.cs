using InternetConnectionTest.DTO;
using InternetConnectionTest.Extensions;
using InternetConnectionTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace InternetConnectionTest.Controllers
{
    public class PingController: IPingController
    {
        #region Fields
        private IConfigController _configController;
        private IPingFactory _pingFactory;
        #endregion

        #region Properties
        public IList<string> Hosts
        {
            get { return _configController.Config.Hosts; }
        }
        #endregion

        #region Constructors
        public PingController(IConfigController configController, IPingFactory pingFactory)
        {
            this._configController = configController;
            this._pingFactory = pingFactory;
        }
        #endregion

        #region Public methods
        public IList<PingResultDTO> DoForAll()
        {
            List<PingResultDTO> result = new List<PingResultDTO>();

            foreach (string host in Hosts)
            {
                PingResultDTO answer = new PingResultDTO();
                answer.Host = host;
                answer.Success = Ping(host);
                result.Add(answer);
            }

            return result;
        }
        #endregion

        #region Private methods
        private bool Ping(string host)
        {
            try
            {
                IPing myPing = this._pingFactory.CreateNewPing();
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = this._pingFactory.CreateNewPingOptions();
                IPingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
