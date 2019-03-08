using IglooSmartHome.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms.Utils;

namespace IglooSmartHome.Services
{
    public interface IDeviceSubscriptionService
    {
        Task<IEnumerable<DeviceSubscription>> GetDeviceSubscriptionsAsync();
    }

    public class DeviceSubscriptionsService : IDeviceSubscriptionService
    {
        private readonly AuthMobileServiceClient _client;

        public DeviceSubscriptionsService(AuthMobileServiceClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<DeviceSubscription>> GetDeviceSubscriptionsAsync()
            => await _client.InvokeApiAsync<IEnumerable<DeviceSubscription>>(
                "subscription", HttpMethod.Get, new Dictionary<string,string>());
    }

    public class DeviceSubscriptionsDummyService : IDeviceSubscriptionService
    {
        private readonly AuthMobileServiceClient _client;

        public DeviceSubscriptionsDummyService(AuthMobileServiceClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<DeviceSubscription>> GetDeviceSubscriptionsAsync()
            => await Task.FromResult(Enumerable.Range(1, 100).Select(id => new DeviceSubscription()
            {
                CustomDeviceName = "device" + id,
                DeviceId = id,
                Id = id,
                Role = DeviceSubscriptionRole.Guest
            }));
    }
}
