using IglooSmartHome.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IglooSmartHome.Services
{
    public interface IDeviceSubscriptionService
    {
        event EventHandler<DeviceSubscription> NewDeviceSubscribed;
        Task<IEnumerable<DeviceSubscription>> GetDeviceSubscriptionsAsync();
        Task<DeviceSubscription> SubscribeToDeviceAsync(string deviceId, string customDeviceName);
    }
}
