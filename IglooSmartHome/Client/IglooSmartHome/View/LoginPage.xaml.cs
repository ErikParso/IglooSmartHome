using IglooSmartHome.View.MasterDetail;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loginControl.Authenticate();
        }

        private void OnUserAuthenticated(object sender, EventArgs args)
        {
            App.Current.MainPage = new MasterDetailView();
        }
    }
}