using Autofac;
using IglooSmartHomeDevice.RefitInterfaces;
using IglooSmartHomeDevice.Services;
using IglooSmartHomeDevice.SignalR;
using IglooSmartHomeDevice.ViewModels;
using MetroLog;
using MetroLog.Targets;
using Refit;
using System;
using System.Net.Http;

namespace IglooSmartHomeDevice
{
    public class BootstrapContainer
    {
        public static IContainer Instance { get; private set; }

        public static void Initialize()
        {
            var builder= new ContainerBuilder();
            // Http
            builder.RegisterType<RefreshTokenHandler>()
                .OnActivated(e => e.Instance.AuthenticationService = e.Context.Resolve<AuthenticationService>())
                .SingleInstance();
            builder.RegisterType<DeviceHttpClient>()
                .As<HttpClient>()
                .SingleInstance();
            // Refit
            builder.Register(c => RestService.For<IDeviceLoginService>(c.Resolve<HttpClient>()))
                .SingleInstance();
            builder.Register(c => RestService.For<IDevicesService>(c.Resolve<HttpClient>()))
                .SingleInstance();
            builder.Register(c => RestService.For<IOnOffDeviceRefitService>(c.Resolve<HttpClient>()))
                .SingleInstance();
            // Services
            builder.RegisterType<AuthenticationService>()
                .SingleInstance();
            builder.RegisterType<DeviceConnectionService>()
                .SingleInstance();
            builder.RegisterType<SmarthomeConfigurationService>()
                .SingleInstance();
            // Handlers
            builder.RegisterType<LightStateSignalRRequestHandler>()
                .SingleInstance();
            // Views
            builder.RegisterType<MainPage>()
                .SingleInstance();
            // ViewModels
            builder.RegisterType<MainPageViewModel>()
                .SingleInstance();
            // Logger
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new StreamingFileTarget());
            var logger = LogManagerFactory.DefaultLogManager.GetLogger("Log");
            builder.RegisterInstance(logger);

            Instance = builder.Build();
        }
    }
}
