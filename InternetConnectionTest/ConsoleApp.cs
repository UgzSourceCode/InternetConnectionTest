using InternetConnectionTest.Controllers;
using InternetConnectionTest.DTO;
using InternetConnectionTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetConnectionTest
{
    public class ConsoleApp: IConsoleApp
    {
        #region Fields
        private IRepeatController _repeatController;
        private IPingController _pingController;
        private ILogController _logController;
        #endregion

        #region Constructor
        public ConsoleApp(ILogController logController, IRepeatController repeatController, IPingController pingController)
        {
            this._logController = logController;
            this._repeatController = repeatController;
            this._pingController = pingController;
        }
        #endregion

        #region Public methods
        public void Run()
        {
            try
            {
                _repeatController.Start(() =>
                {
                    IList<PingResultDTO> result = _pingController.DoForAll();
                    _logController.Ping(result);
                });
            }
            catch(Exception ex)
            {
                this._logController.Error(ex.Message);
            }
        } 
        #endregion
    }
}
