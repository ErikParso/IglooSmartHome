using Autofac;
using IglooSmartHome.ViewModels;
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
        }

        public async Task ReloadDevicesAsync()
            => await _viewModel.ReloadDevicesAsync();
    }
}