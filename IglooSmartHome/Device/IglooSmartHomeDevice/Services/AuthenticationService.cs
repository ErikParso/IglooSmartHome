using IglooSmartHomeDevice.Model;
using IglooSmartHomeDevice.RefitInterfaces;
using Newtonsoft.Json;
using Refit;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class AuthenticationService
    {
        private const string loginEndpoint = "/api/DeviceLogin";
        private const string credentialsConfig = "Credentials.json";

        private readonly Credentials _credentials;
        private readonly HttpClient _httpClient;
        private readonly IDeviceLoginService _deviceLoginService;

        private string _accessToken;
        private string _refreshToken;

        public AuthenticationService(HttpClient httpClient)
        {
            var credentialsJson = File.ReadAllText(credentialsConfig);
            _credentials = JsonConvert.DeserializeObject<Credentials>(credentialsJson);
            _httpClient = httpClient;
            _deviceLoginService = RestService.For<IDeviceLoginService>(_httpClient);
        }

        public async Task LoginAsync()
        {
            var result = await _deviceLoginService.LoginDeviceAsync(new CustomLoginRequest()
            {
                UserId = _credentials.DeviceName, Password = _credentials.Password
            });
            _accessToken = result.MobileServiceAuthenticationToken;
            _refreshToken = result.RefreshToken;
        }

        public async Task<string> RefreshTokenAsync()
        {
            var result = await _deviceLoginService.RefreshTokenAsync(_credentials.DeviceName, _refreshToken);
            _accessToken = result.MobileServiceAuthenticationToken;
            _refreshToken = result.RefreshToken;
            return _accessToken;
        }

    }
}
