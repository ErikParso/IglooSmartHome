using IglooSmartHome.IglooEventArgs;
using IglooSmartHome.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IglooSmartHome.ViewModels
{
    public class DeviceHeaderViewModel : INotifyPropertyChanged
    {
        #region Private fields

        private readonly IDeviceOnlineStatusService _deviceOnlineStatusService;
        private readonly IDeviceInformationService _deviceInformationService;
        private readonly IDeviceSelectionService _deviceSelectionService;

        private int? _deviceId;
        private string _deviceName;
        private bool _isOnline;

        #endregion


        #region Ctor

        public DeviceHeaderViewModel(
            IDeviceOnlineStatusService deviceOnlineStatusService,
            IDeviceInformationService deviceInformationService,
            IDeviceSelectionService deviceSelectionService)
        {
            _deviceOnlineStatusService = deviceOnlineStatusService;
            _deviceInformationService = deviceInformationService;
            _deviceSelectionService = deviceSelectionService;

            AddHandlers();
        }

        #endregion


        #region Event handling

        private void AddHandlers()
        {
            _deviceOnlineStatusService.DeviceOnlineStatusChanged += _deviceOnlineStatusService_DeviceOnlineStatusChanged;
            _deviceSelectionService.SelectedDeviceChanged += _deviceSelectionService_SelectedDeviceChanged;
        }

        private async void _deviceSelectionService_SelectedDeviceChanged(object sender, int? deviceId)
        {
            _deviceId = deviceId;
            if (deviceId.HasValue)
            {
                DeviceName = await _deviceInformationService.GetDeviceName(deviceId.Value);
                IsOnline = await _deviceOnlineStatusService.IsDeviceOnline(deviceId.Value);
            }
            else
            {
                DeviceName = string.Empty;
                IsOnline = false;
            }
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

        public string DeviceName
        {
            get => _deviceName;
            set
            {
                _deviceName = value;
                RaisePropertyChanged();
            }
        }

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
