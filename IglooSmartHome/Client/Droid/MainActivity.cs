using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
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

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            Forms.Init (this, bundle);

            App app = new App();
            app.UseAuthentication(this, "<3passwordiq<3", Constants.ApplicationURL, "igloosmarthome");
            app.Build();

            LoadApplication(app);
		}

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}

