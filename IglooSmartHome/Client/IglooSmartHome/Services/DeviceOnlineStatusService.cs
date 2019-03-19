using IglooSmartHome.IglooEventArgs;
using IglooSmartHome.SignalR;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms.Utils;

namespace IglooSmartHome.Services
{
    public interface IDeviceOnlineStatusService
    {
        event EventHandler<DeviceOnlineStatusChangedEventArgs> DeviceOnlineStatusChanged;

        Task<bool> IsDeviceOnline(int deviceId);
    }

    public class DeviceOnlineStatusService : IDeviceOnlineStatusService
    {
        private const string deviceOnlineStatusControllerName = "DeviceOnlineStatus";
        private const string deviceOfflineMessage = "deviceOffline";
        private const string deviceOnlineMessage = "deviceOnline";

        private readonly AuthMobileServiceClient _client;
        private readonly SignalRConnectionService _signalRConnectionService;

        public DeviceOnlineStatusService(
            AuthMobileServiceClient client,
            SignalRConnectionService signalRConnectionService)
        {
            _client = client;
            _signalRConnectionService = signalRConnectionService;
            SubscribeSignalREvents();
        }

        private void SubscribeSignalREvents()
        {
            _signalRConnectionService.UserConnectionHubProxy
                .On<int>(deviceOfflineMessage, deviceId =>
                    DeviceOnlineStatusChanged?.Invoke(this, new DeviceOnlineStatusChangedEventArgs(deviceId, false)));
            _signalRConnectionService.UserConnectionHubProxy
                .On<int>(deviceOnlineMessage, deviceId =>
                    DeviceOnlineStatusChanged?.Invoke(this, new DeviceOnlineStatusChangedEventArgs(deviceId, true)));
        }

        public event EventHandler<DeviceOnlineStatusChangedEventArgs> DeviceOnlineStatusChanged;

        public async Task<bool> IsDeviceOnline(int deviceId)
            => await _client.InvokeApiAsync<bool>(deviceOnlineStatusControllerName, HttpMethod.Get,
                new Dictionary<string, string>() { { "deviceId", deviceId.ToString() } });
    }
}
