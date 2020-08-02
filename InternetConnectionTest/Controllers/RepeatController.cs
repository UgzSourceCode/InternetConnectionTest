using InternetConnectionTest.DTO;
using InternetConnectionTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InternetConnectionTest.Controllers
{
    public delegate void DoThat();
    public class RepeatController : IRepeatController
    {
        #region Fields
        private IConfigController _configController;
        #endregion

        #region Properties
        public RepeatConfigDTO Config
        {
            get { return this._configController.Config.RepeatConfig; }
        }
        #endregion

        #region Constructors
        public RepeatController(IConfigController configController)
        {
            this._configController = configController;
        }
        #endregion

        #region Public methods
        public void Start(DoThat toDo)
        {
            int i = 1;
            do
            {
                toDo();
                Task.Delay(this.Config.Delay).Wait();
                i++;
            }
            while (CanContinue(i));
        }
        #endregion

        #region Private methods
        private bool CanContinue(int i)
        {
            bool result = true;
            if (this.Config.NumberOfRepetitions != 0 && this.Config.NumberOfRepetitions < i)
                result = false;

            return result;
        }
        #endregion
    }
}
