using IglooSmartHome.IglooEventArgs;
using IglooSmartHome.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IglooSmartHome.ViewModels
{
    public class DeviceControllerViewModel : INotifyPropertyChanged
    {
        #region Private fields

        private readonly IDeviceSelectionService _deviceSelectionService;
        private readonly IDeviceOnlineStatusService _deviceOnlineStatusService;

        private int? _deviceId;
        private bool _isOnline;

        #endregion


        #region Ctor

        public DeviceControllerViewModel(
            IDeviceSelectionService deviceSelectionService,
            IDeviceOnlineStatusService deviceOnlineStatusService)
        {
            _deviceSelectionService = deviceSelectionService;
            _deviceOnlineStatusService = deviceOnlineStatusService;

            AddHandlers();
        }

        #endregion


        #region Event handlers

        private void AddHandlers()
        {
            _deviceOnlineStatusService.DeviceOnlineStatusChanged += _deviceOnlineStatusService_DeviceOnlineStatusChanged;
            _deviceSelectionService.SelectedDeviceChanged += _deviceSelectionService_SelectedDeviceChanged;
        }

        private async void _deviceSelectionService_SelectedDeviceChanged(object sender, int? deviceId)
        {
            _deviceId = deviceId;
            IsOnline = deviceId.HasValue
                ? await _deviceOnlineStatusService.IsDeviceOnline(deviceId.Value)
                : false;
        }

        private void _deviceOnlineStatusService_DeviceOnlineStatusChanged(object sender, DeviceOnlineStatusChangedEventArgs e)
        {
            if (_deviceId.HasValue && e.DeviceId == _deviceId.Value)
            {
                IsOnline = e.OnlineStatus;
            }
        }

        #endregion


        #region Public properties

        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                _isOnline = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        #region INotifyPropertyChanged

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
