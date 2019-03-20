using Autofac;
using IglooSmartHome.Services;
using IglooSmartHome.SignalR;
using IglooSmartHome.Themes;
using IglooSmartHome.View;
using IglooSmartHome.View.MasterDetail;
using IglooSmartHome.ViewModels;
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
            builder.RegisterType<LoginPage>()
                .SingleInstance();
            builder.RegisterType<MasterDetailView>()
                .SingleInstance();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceSubscriptionsViewModel>()
                .SingleInstance();
            builder.RegisterType<DeviceSubscriptionPopupViewModel>()
                .SingleInstance();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceSubscriptionsService>()
                .As<IDeviceSubscriptionService>()
                .SingleInstance();
            builder.RegisterType<DeviceOnlineStatusService>()
                .As<IDeviceOnlineStatusService>()
                .SingleInstance();
            builder.RegisterType<SignalRConnectionService>()
                .SingleInstance();
        }

        protected override void OnStart()
        {
            base.OnStart();
            MainPage = CurrentAppContainer.Resolve<LoginPage>();
        }

    }
}