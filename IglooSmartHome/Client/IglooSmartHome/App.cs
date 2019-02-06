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
            base(registerPlatformSpecific)
        {

        }

        protected override void RegisterSharedTypes(ContainerBuilder builder)
        {
            //MobileServiceClient
            builder.RegisterInstance(new MobileServiceClient(Constants.ApplicationURL));
            //Pages
            builder.RegisterType<LoginPage>().SingleInstance();
            //ViewModels

        }

        protected override Page GetMainPage()
        {
            return Container.Resolve<LoginPage>();
        }

    }
}