﻿using Autofac;
using IglooSmartHome.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Utils;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View.PopupPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSubscriptionPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly DeviceSubscriptionPopupViewModel _viewModel;

        public DeviceSubscriptionPopupPage()
        {
            InitializeComponent();
            _viewModel = AppBase.CurrentAppContainer.Resolve<DeviceSubscriptionPopupViewModel>();
            BindingContext = _viewModel;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var subs = await _viewModel.SetDeviceOwnerAsync();
            if (subs != null)
            {
                await PopupNavigation.Instance.PopAsync(true);
            }
        }
    }
}