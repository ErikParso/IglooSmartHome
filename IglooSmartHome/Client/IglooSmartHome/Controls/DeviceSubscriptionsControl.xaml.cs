using Autofac;
using IglooSmartHome.IglooEventArgs;
using IglooSmartHome.Models;
using IglooSmartHome.View.PopupPages;
using IglooSmartHome.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Utils;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSubscriptionsControl : ContentView
    {
        public DeviceSubscriptionsControl()
        {
            InitializeComponent();
        }

        private async void ButtonAddDevice_Clicked(object sender, EventArgs e)
            => await PopupNavigation.Instance.PushAsync(new DeviceSubscriptionPopupPage());
    }
}