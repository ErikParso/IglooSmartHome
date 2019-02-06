using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Auth;
using Xamarin.Droid.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace IglooSmartHome.Droid
{
    [Activity(Theme = "@style/AppTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			CurrentPlatform.Init();
			Forms.Init (this, bundle);

            App app = new App(RegisterPlatfirmSpecific);
            LoadApplication (app);
		}

        private void RegisterPlatfirmSpecific(ContainerBuilder builder)
        {
            builder.RegisterContext(this);
            builder.RegisterAccountStore(AccountStore.Create(this, "pwd"));
            builder.RegisterAccountStoreService("igloosmarthome");
            builder.RegisterAuthenticationService();
            builder.RegisterCustomLoginService();
            builder.RegisterProviderLoginService("igloosmarthome");
            builder.RegisterVerificationService();
            builder.RegisterAccountInformationService();
        }
    }
}

