using IglooSmartHome.Models;
using IglooSmartHome.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class DeviceSubscriptionsViewModel : INotifyPropertyChanged
    {
        private readonly IDeviceSubscriptionService _devicesService;
        private IEnumerable<DeviceSubscription> _subscriptions;

        public DeviceSubscriptionsViewModel(IDeviceSubscriptionService devicesService)
        {
            _devicesService = devicesService;
            _subscriptions = Enumerable.Empty<DeviceSubscription>();
        }

        public IEnumerable<DeviceSubscription> Subscriptions {
            get => _subscriptions;
            set { _subscriptions = value; RaisePropertyChanged(); }
        }

        public async Task ReloadDevicesAsync()
            => Subscriptions = await _devicesService.GetDeviceSubscriptionsAsync();

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}