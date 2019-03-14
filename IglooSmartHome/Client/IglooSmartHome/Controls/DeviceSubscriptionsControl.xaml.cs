using Autofac;
using IglooSmartHome.View.PopupPages;
using IglooSmartHome.ViewModels;
using Rg.Plugins.Popup.Extensions;
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
        private readonly DeviceSubscriptionsViewModel _viewModel;

        public DeviceSubscriptionsControl()
        {
            InitializeComponent();
            _viewModel = AppBase.CurrentAppContainer.Resolve<DeviceSubscriptionsViewModel>();
            BindingContext = _viewModel;
        }

        public async Task ReloadDevicesAsync()
            => await _viewModel.ReloadDevicesAsync();

        private void DeviceSubscription_Tapped(object sender, EventArgs e)
        {

        }

        private async void ButtonAddDevice_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new DeviceSubscriptionPopupPage());
        }
    }
}