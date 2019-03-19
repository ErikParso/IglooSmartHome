using IglooSmartHome.IglooEventArgs;
using IglooSmartHome.Models;
using IglooSmartHome.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class DeviceSubscriptionsViewModel : INotifyPropertyChanged
    {
        private readonly IDeviceSubscriptionService _devicesSubscriptionService;
        private readonly IDeviceOnlineStatusService _deviceOnlineStatusService;
        private IEnumerable<DeviceSubscription> _subscriptions;

        public DeviceSubscriptionsViewModel(
            IDeviceSubscriptionService devicesSubscriptionService,
            IDeviceOnlineStatusService deviceOnlineStatusService)
        {
            _devicesSubscriptionService = devicesSubscriptionService;
            _deviceOnlineStatusService = deviceOnlineStatusService;
            _subscriptions = Enumerable.Empty<DeviceSubscription>();
            AddHandlers();
        }

        private void AddHandlers()
        {
            _devicesSubscriptionService.NewDeviceSubscribed += NewDeviceSubscribed;
            _deviceOnlineStatusService.DeviceOnlineStatusChanged += DeviceOnlineStatusChanged;
        }

        private void DeviceOnlineStatusChanged(object sender, DeviceOnlineStatusChangedEventArgs e)
        {
            foreach (var subs in Subscriptions.Where(s => s.DeviceId == e.DeviceId))
            {
                subs.IsOnline = e.OnlineStatus;
            }
        }

        private void NewDeviceSubscribed(object sender, DeviceSubscription e)
        {
            Subscriptions = Subscriptions.Concat(new[] { e });
        }

        public IEnumerable<DeviceSubscription> Subscriptions
        {
            get => _subscriptions;
            set { _subscriptions = value; RaisePropertyChanged(); }
        }

        public async Task ReloadDevicesAsync()
        {
            Subscriptions = await _devicesSubscriptionService.GetDeviceSubscriptionsAsync();
            foreach (var subscription in Subscriptions)
            {
                subscription.IsOnline = await _deviceOnlineStatusService.IsDeviceOnline(subscription.DeviceId);
            }
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}