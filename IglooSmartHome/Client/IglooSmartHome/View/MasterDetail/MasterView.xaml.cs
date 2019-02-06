using Autofac;
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
            var authenticationService = AppBase.CurrentAppContainer.Resolve<IAuthenticationService>();
            await authenticationService.Logout();
            AppBase.Current.MainPage = AppBase.CurrentAppContainer.Resolve<LoginPage>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            profileBar.LoadAccountInformation();
        }
    }
}