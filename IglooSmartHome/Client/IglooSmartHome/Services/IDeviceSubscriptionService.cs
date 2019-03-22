using IglooSmartHome.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IglooSmartHome.Services
{
    public interface IDeviceSubscriptionService
    {
        event EventHandler<IEnumerable<DeviceSubscription>> DeviceSubscriptionsLoaded;

        event EventHandler<DeviceSubscription> NewDeviceSubscribed;

        Task LoadSubscriptionsAsync();

        Task<DeviceSubscription> SubscribeToDeviceAsync(string deviceId, string customDeviceName);
    }
}
