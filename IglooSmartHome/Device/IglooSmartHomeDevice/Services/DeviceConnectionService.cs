using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class DeviceConnectionService
    {
        private HubConnection _hubConnection;
        private IHubProxy _deviceConnectionHubProxy;
        private readonly AuthenticationService _authenticationService;      

        public event EventHandler<string> OnLog;

        public DeviceConnectionService(
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            _hubConnection = new HubConnection(Environment.ServerAddress, new Dictionary<string, string> { { "deviceName", "rpi" } });
            _hubConnection.Headers.Add("ZUMO-API-VERSION", "2.0.0");

            _hubConnection.Closed += OnDisconnected;
            _hubConnection.Error += OnError;
            _hubConnection.StateChanged += OnStateChanged;
            _hubConnection.Reconnecting += Reconnecting;
            _hubConnection.Reconnected += Reconnected;
            _hubConnection.ConnectionSlow += ConnectionSlow;
            _hubConnection.Received += Received;

            _deviceConnectionHubProxy = _hubConnection.CreateHubProxy("DeviceConnectionHub");
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

        private void Received(string obj)
        {
            OnLog(this, DateTime.Now + $": Received " + obj);
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

        async void OnDisconnected()
        {
            OnLog(this, DateTime.Now + ": Connection closed.");
            await StartConnection();
        }
    }
}
