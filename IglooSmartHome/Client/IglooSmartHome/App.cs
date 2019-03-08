using Autofac;
using IglooSmartHome.Services;
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
        }

        protected void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<LoginPage>().SingleInstance();
        }

        protected void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceSubscriptionsViewModel>().SingleInstance();
        }

        protected void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceSubscriptionsService>().SingleInstance();
        }

        protected override void OnStart()
        {
            base.OnStart();
            MainPage = CurrentAppContainer.Resolve<LoginPage>();
        }

    }
}