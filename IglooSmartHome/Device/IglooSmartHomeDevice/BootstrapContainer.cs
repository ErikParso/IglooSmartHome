using Autofac;
using IglooSmartHomeDevice.RefitInterfaces;
using IglooSmartHomeDevice.Services;
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
            builder.RegisterType<RefreshTokenHandler>()
                .OnActivated(e => e.Instance.AuthenticationService = e.Context.Resolve<AuthenticationService>())
                .SingleInstance();
            builder.RegisterType<DeviceHttpClient>()
                .As<HttpClient>()
                .SingleInstance();
            builder.RegisterType<AuthenticationService>()
                .SingleInstance();
            builder.Register(c => RestService.For<IDeviceLoginService>(c.Resolve<HttpClient>()))
                .SingleInstance();
            builder.Register(c => RestService.For<IDevicesService>(c.Resolve<HttpClient>()))
                .SingleInstance();
            builder.RegisterType<MainPage>()
                .SingleInstance();

            Instance = builder.Build();
        }

        private static object Func<T>()
        {
            throw new NotImplementedException();
        }
    }
}
