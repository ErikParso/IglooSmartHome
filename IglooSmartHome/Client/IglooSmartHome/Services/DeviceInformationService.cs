using IglooSmartHome.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms.Utils;

namespace IglooSmartHome.Services
{
    public class DeviceInformationService : IDeviceInformationService
    {
        private readonly AuthMobileServiceClient _client;
        private readonly Dictionary<int, DeviceInformation> _deviceInformationCache;

        public DeviceInformationService(AuthMobileServiceClient client)
        {
            _client = client;
            _deviceInformationCache = new Dictionary<int, DeviceInformation>();
        }

        public async Task<DeviceInformation> GetDeviceInformationAsync(int deviceId)
        {
            if (!_deviceInformationCache.TryGetValue(deviceId, out DeviceInformation deviceInformation))
            {
                deviceInformation = await _client.InvokeApiAsync<DeviceInformation>(
                    "deviceInformation", HttpMethod.Get, new Dictionary<string, string>()
                    { {"deviceId", deviceId.ToString()} });
                _deviceInformationCache[deviceId] = deviceInformation;
            }
            return deviceInformation;
        }
    }
}
