using Autofac;
using IglooSmartHome.SignalR;
using IglooSmartHome.View.MasterDetail;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly MasterDetailView _masterDetailView;

        public LoginPage(MasterDetailView masterDetailView)
        {
            InitializeComponent();
            _masterDetailView = masterDetailView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loginControl.Authenticate();
        }

        private void OnUserAuthenticated(object sender, EventArgs args)
        {
            App.Current.MainPage = _masterDetailView;
        }
    }
}