using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IglooSmartHome.Models;
using IglooSmartHome.ViewModels;
using Xamarin.Forms.Utils;

namespace IglooSmartHome.Services
{
    public class DeviceSubscriptionsService
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
}
