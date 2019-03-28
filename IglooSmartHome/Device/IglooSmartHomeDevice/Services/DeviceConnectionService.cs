using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class DeviceConnectionService
    {
        private HubConnection _hubConnection;
        private IHubProxy _deviceConnectionHubProxy;
        private IHubProxy _lightStatsHubProxy;
        private readonly AuthenticationService _authenticationService;

        public event EventHandler<string> OnLog;

        public DeviceConnectionService(
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            _hubConnection = new HubConnection(Constants.ServerAddress);
            _hubConnection.Headers.Add("ZUMO-API-VERSION", "2.0.0");

            _hubConnection.Closed += OnDisconnected;
            _hubConnection.Error += OnError;
            _hubConnection.StateChanged += OnStateChanged;
            _hubConnection.Reconnecting += Reconnecting;
            _hubConnection.Reconnected += Reconnected;
            _hubConnection.ConnectionSlow += ConnectionSlow;

            _deviceConnectionHubProxy = _hubConnection.CreateHubProxy("DeviceConnectionHub");
            _lightStatsHubProxy = _hubConnection.CreateHubProxy("LightStatsHub");
            _lightStatsHubProxy.On<Guid, string>("getLightState", (guid, parameter) =>
            {
                OnLog(this, DateTime.Now + $": {parameter} Light stats request with id {guid}");
                _lightStatsHubProxy.Invoke("sendLightState", guid, "Lighs are on!");
            });
        }

        public async Task StartConnection()
        {
            OnLog(this, DateTime.Now + ": Connecting...");
            _hubConnection.Headers["X-ZUMO-AUTH"] = await _authenticationService.RefreshTokenAsync();
            await _hubConnection.Start();
        }

        internal void StopConnection()
        {
            OnLog(this, DateTime.Now + ": Disconnecting...");
            _hubConnection.Stop();
        }

        private void ConnectionSlow()
        {
            OnLog(this, DateTime.Now + $": Connection slow...");
        }

        private void Reconnected()
        {
            OnLog(this, DateTime.Now + $": Reconnected.");
        }

        private void Reconnecting()
        {
            OnLog(this, DateTime.Now + $": Reconnecting...");
        }

        private void OnStateChanged(StateChange obj)
        {
            OnLog(this, DateTime.Now + $": Connection state changed. ({obj.OldState} => {obj.NewState})");
        }

        private void OnError(Exception obj)
        {
            OnLog(this, DateTime.Now + $": Connection error. {obj.Message}");
        }

        private void OnDisconnected()
        {
            OnLog(this, DateTime.Now + ": Connection closed.");
        }
    }
}
