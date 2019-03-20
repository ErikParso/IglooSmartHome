using IglooSmartHomeDevice.Services;
using System;
using System.Net.Http;

namespace IglooSmartHomeDevice
{
    public class DeviceHttpClient: HttpClient
    {
        private const string baseAddress = "https://igloosmarthome.azurewebsites.net";

        public DeviceHttpClient(RefreshTokenHandler refreshTokenHandler)
            : base(refreshTokenHandler)
        {
            BaseAddress = new Uri(baseAddress);
            DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
        }
    }
}
