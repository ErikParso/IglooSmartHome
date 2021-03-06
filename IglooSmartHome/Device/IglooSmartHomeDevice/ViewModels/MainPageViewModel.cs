﻿using IglooSmartHomeDevice.RefitInterfaces;
using IglooSmartHomeDevice.Services;
using MetroLog;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace IglooSmartHomeDevice.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        #region Private fields

        private readonly AuthenticationService _authenticationService;
        private readonly DeviceConnectionService _deviceConnectionService;
        private readonly IDevicesService _devicesService;
        private readonly ILogger _logger;
        private readonly StringBuilder _deviceConnectionServiceLog;

        #endregion


        #region Ctor

        public MainPageViewModel(
            AuthenticationService authenticationService,
            DeviceConnectionService deviceConnectionService,
            IDevicesService devicesService,
            ILogger logger)
        {
            _authenticationService = authenticationService;
            _deviceConnectionService = deviceConnectionService;
            _devicesService = devicesService;
            _logger = logger;
            _deviceConnectionServiceLog = new StringBuilder();

            AddHandlers();
        }

        #endregion


        #region Event handling

        private void AddHandlers()
        {
            _deviceConnectionService.OnLog += _deviceConnectionService_OnLog;
        }

        private async void _deviceConnectionService_OnLog(object sender, string message)
        {
            _logger.Trace(message);
            _deviceConnectionServiceLog.AppendLine(message);
            await OnPropertyChanged(nameof(DeviceConnectionServiceLog));
        }

        #endregion


        #region Public properties

        public string DeviceConnectionServiceLog
        {
            get => _deviceConnectionServiceLog.ToString();
            set => throw new NotImplementedException();
        }

        #endregion


        #region Public methods

        public async Task LoginAndConnect()
        {
            await _authenticationService.LoginAsync();
            var info = await _devicesService.GetDeviceInfoAsync();
            await _deviceConnectionService.StartConnection();
        }

        public void Disconnect()
            => _deviceConnectionService.StopConnection();

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task OnPropertyChanged([CallerMemberName] string propName = "")
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            });
        }

        #endregion
    }
}
