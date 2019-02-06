using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Foundation;
using UIKit;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Autofac;

namespace IglooSmartHome.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// Initialize Azure Mobile Apps
			CurrentPlatform.Init();

			// Initialize Xamarin Forms
			Forms.Init();

			LoadApplication(new App (registerPlatformSpecific));

			return base.FinishedLaunching(app, options);
		}

        private void registerPlatformSpecific(ContainerBuilder obj)
        {
            throw new NotImplementedException();
        }
    }
}

