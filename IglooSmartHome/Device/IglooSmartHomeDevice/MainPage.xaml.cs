using System;
using Autofac;
using IglooSmartHomeDevice.RefitInterfaces;
using IglooSmartHomeDevice.Services;
using MetroLog;
using MetroLog.Targets;
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
        private readonly ILogger _logger;

        public MainPage()
        {
            InitializeComponent();
            BootstrapContainer.Initialize();
            _authenticationService = BootstrapContainer.Instance.Resolve<AuthenticationService>();
            _devicesService = BootstrapContainer.Instance.Resolve<IDevicesService>();
            _deviceConnectionService = BootstrapContainer.Instance.Resolve<DeviceConnectionService>();
            _deviceConnectionService.OnLog += Log;

            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new StreamingFileTarget());
            _logger = LogManagerFactory.DefaultLogManager.GetLogger<MainPage>();
        }

        private async void Log(object sender, string message)
        {
            await log.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                log.Text += message + System.Environment.NewLine;
                _logger.Trace(message);
            });
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await _authenticationService.LoginAsync();
            var info = await _devicesService.GetDeviceInfoAsync();
            await _deviceConnectionService.StartConnection();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _deviceConnectionService.StopConnection();
        }
    }
}
