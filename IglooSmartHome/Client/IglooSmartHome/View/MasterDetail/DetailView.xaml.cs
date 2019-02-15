using Autofac;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Utils;
using Xamarin.Forms.Utils.Services;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailView : ContentPage
    {
        private readonly AuthMobileServiceClient _client;
        private readonly IAccountStoreService _accountStore;

        public DetailView()
        {
            InitializeComponent();
            _client = App.CurrentAppContainer.Resolve<AuthMobileServiceClient>();
            _accountStore = App.CurrentAppContainer.Resolve<IAccountStoreService>();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = JsonConvert.SerializeObject(
                    await _client.InvokeApiAsync("Accounts", HttpMethod.Post, new Dictionary<string, string>()),
                    Formatting.Indented);
                lblInfo.Text += Environment.NewLine;
                lblInfo.Text += JsonConvert.SerializeObject(
                    _accountStore.RetrieveTokenFromSecureStore(),
                    Formatting.Indented);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }
    }
}