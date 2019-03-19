using IglooSmartHome.SignalR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IglooSmartHome.View.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly SignalRConnectionService _signalRConnectionService;

        public MasterDetailView(SignalRConnectionService signalRConnectionService)
        {
            InitializeComponent();
            _signalRConnectionService = signalRConnectionService;
        }

        private async void MasterDetailPage_Appearing(object sender, System.EventArgs e)
            => await _signalRConnectionService.StartConnection();
    }
}