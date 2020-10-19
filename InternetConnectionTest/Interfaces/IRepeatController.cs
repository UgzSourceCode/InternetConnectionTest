using InternetConnectionTest.Controllers;
using InternetConnectionTest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetConnectionTest.Interfaces
{
    public interface IRepeatController
    {
        public RepeatConfigDTO Config { get; }
        public void Start(DoThat toDo);
    }
}
