using System;
using Autofac;
using IglooSmartHomeDevice.RefitInterfaces;
using IglooSmartHomeDevice.Services;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IglooSmartHomeDevice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly AuthenticationService _authenticationService;
        private readonly DeviceConnectionService _deviceConnectionService;
        private readonly IDevicesService _devicesService;

        public MainPage()
        {
            InitializeComponent();
            BootstrapContainer.Initialize();
            _authenticationService = BootstrapContainer.Instance.Resolve<AuthenticationService>();
            _devicesService = BootstrapContainer.Instance.Resolve<IDevicesService>();
            _deviceConnectionService = BootstrapContainer.Instance.Resolve<DeviceConnectionService>();
            _deviceConnectionService.OnLog += Log;
        }

        private async void Log(object sender, string e)
        {
            await log.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                log.Text += e + System.Environment.NewLine;
            });
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await _authenticationService.LoginAsync();
            var info = await _devicesService.GetDeviceInfoAsync();
            _deviceConnectionService.Connect();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
