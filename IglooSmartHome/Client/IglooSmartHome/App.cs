using Autofac;
using IglooSmartHome.Services;
using IglooSmartHome.Themes;
using IglooSmartHome.View;
using IglooSmartHome.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Utils;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IglooSmartHome
{
    public class App : AppBase
    {
        public App() : base()
        {
            RegisterTypes(RegisterViews);
            RegisterTypes(RegisterViewModels);
            RegisterTypes(RegisterServices);
            Resources = new White();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<LoginPage>().SingleInstance();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceSubscriptionsViewModel>().SingleInstance();
            builder.RegisterType<DeviceSubscriptionPopupViewModel>().SingleInstance();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceSubscriptionsService>()
                .As<IDeviceSubscriptionService>()
                .SingleInstance();
        }

        protected override void OnStart()
        {
            base.OnStart();
            MainPage = CurrentAppContainer.Resolve<LoginPage>();
        }

    }
}