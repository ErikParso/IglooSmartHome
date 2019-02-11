using Autofac;
using IglooSmartHome.View;
using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Utils;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IglooSmartHome
{
    public class App : AppBase
    {
        public App(Action<ContainerBuilder> registerPlatformSpecific) :
            base(Constants.ApplicationURL, registerPlatformSpecific)
        {

        }

        protected override void RegisterSharedTypes(ContainerBuilder builder)
        {
            builder.RegisterType<LoginPage>().SingleInstance();
        }

        protected override Page GetMainPage()
        {
            return Container.Resolve<LoginPage>();
        }

    }
}