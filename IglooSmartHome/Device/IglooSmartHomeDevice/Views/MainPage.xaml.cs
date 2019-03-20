using System;
using Autofac;
using IglooSmartHomeDevice.RefitInterfaces;
using IglooSmartHomeDevice.Services;
using IglooSmartHomeDevice.ViewModels;
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
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = BootstrapContainer.Instance.Resolve<MainPageViewModel>();
            DataContext = _viewModel;
        }

        private async void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
            => await _viewModel.LoginAndConnect();

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
            => _viewModel.Disconnect();
    }
}
