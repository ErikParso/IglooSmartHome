using IglooSmartHome.Models;
using IglooSmartHome.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class DeviceSubscriptionPopupViewModel : INotifyPropertyChanged
    {
        private readonly IDeviceSubscriptionService _deviceSubscriptionService;

        private string _deviceId;
        private string _customDeviceName;

        public DeviceSubscriptionPopupViewModel(IDeviceSubscriptionService deviceSubscriptionService)
        {
            _deviceSubscriptionService = deviceSubscriptionService;
        }

        public string DeviceId
        {
            get => _deviceId;
            set { _deviceId = value; RaisePropertyChanged(); }
        }

        public string CustomDeviceName
        {
            get => _customDeviceName;
            set { _customDeviceName = value; RaisePropertyChanged(); }
        }

        public async Task<DeviceSubscription> SetDeviceOwnerAsync()
            => await _deviceSubscriptionService.SubscribeToDeviceAsync(_deviceId, _customDeviceName);

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
