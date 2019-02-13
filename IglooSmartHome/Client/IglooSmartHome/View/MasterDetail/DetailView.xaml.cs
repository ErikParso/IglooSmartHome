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
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailView : ContentPage
    {
        private readonly MobileServiceClient _client;

        public DetailView()
        {
            InitializeComponent();
            _client = App.CurrentAppContainer.Resolve<MobileServiceClient>();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = JsonConvert.SerializeObject(
                    await _client.InvokeApiAsync("Accounts", HttpMethod.Post, new Dictionary<string, string>()),
                    Formatting.Indented);
            }
            catch(Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }
    }
}