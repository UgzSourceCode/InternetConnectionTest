using AutoMapper;
using InternetConnectionTest.Controllers;
using InternetConnectionTest.DTO;
using InternetConnectionTest.Facedes;
using InternetConnectionTest.Factories;
using InternetConnectionTest.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace InternetConnectionTest
{
    class Program
    {
        protected static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            InitializeServiceProvider();

            IConsoleApp app = _serviceProvider.GetService<IConsoleApp>();
            app.Run();
        }

        protected static void InitializeServiceProvider()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddAutoMapper(mapperConfig => { mapperConfig.CreateMap<PingReply, IPingReply>(); })
                .AddTransient<IPing, PingFacade>()
                .AddSingleton<IPingFactory, PingFactory>()
                .AddSingleton<ILogController>(InitializeLogControler())
                .AddSingleton<IConfigController, ConfigController>()
                .AddTransient<IPingController, PingController>()
                .AddTransient<IRepeatController, RepeatController>()
                .AddSingleton<IConsoleApp, ConsoleApp>()
                .BuildServiceProvider();
        }

        protected static ILogController InitializeLogControler()
        {
            return new LogController(LogManager.GetCurrentClassLogger());
        }
    }
}
