using IglooSmartHome.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms.Utils;

namespace IglooSmartHome.Services
{
    public class DeviceSubscriptionsService : IDeviceSubscriptionService
    {
        private readonly AuthMobileServiceClient _client;

        public DeviceSubscriptionsService(AuthMobileServiceClient client)
        {
            _client = client;
        }

        public event EventHandler<DeviceSubscription> NewDeviceSubscribed;
        public event EventHandler<IEnumerable<DeviceSubscription>> DeviceSubscriptionsLoaded;

        public async Task LoadSubscriptionsAsync()
        {
            var deviceSubscriptions = await _client.InvokeApiAsync<IEnumerable<DeviceSubscription>>(
                "subscription", HttpMethod.Get, new Dictionary<string, string>());
            DeviceSubscriptionsLoaded?.Invoke(this, deviceSubscriptions);
        }

        public async Task<DeviceSubscription> SubscribeToDeviceAsync(string deviceId, string customDeviceName)
        {
            var subscription = await _client.InvokeApiAsync<DeviceSubscription>(
                "subscription", HttpMethod.Post, new Dictionary<string, string>()
                {
                    { "deviceCode", deviceId }, { "customDeviceName", customDeviceName }
                });
            NewDeviceSubscribed?.Invoke(this, subscription);
            return subscription;
        }
    }
}
