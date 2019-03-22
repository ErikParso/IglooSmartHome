using Autofac;
using IglooSmartHome.SignalR;
using IglooSmartHome.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Utils;
using Xamarin.Forms.Utils.Services;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : ContentPage
    {
        public MasterView()
        {
            InitializeComponent();
        }

        private async void ProfileBar_LogoutClicked(object sender, EventArgs e)
        {
            await ((MasterViewModel)BindingContext).LogoutAndStopConnection();
            AppBase.Current.MainPage = AppBase.CurrentAppContainer.Resolve<LoginPage>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await profileBar.Initialize();
        }
    }
}