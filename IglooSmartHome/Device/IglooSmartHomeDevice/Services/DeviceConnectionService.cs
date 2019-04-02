using IglooSmartHomeDevice.SignalR;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class DeviceConnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _deviceConnectionHubProxy;
        private readonly AuthenticationService _authenticationService;
        private readonly LightStateSignalRRequestHandler _lightStateSignalRRequestHandler;

        public event EventHandler<string> OnLog;

        public DeviceConnectionService(
            AuthenticationService authenticationService,
            LightStateSignalRRequestHandler lightStateSignalRRequestHandler)
        {
            _authenticationService = authenticationService;
            _lightStateSignalRRequestHandler = lightStateSignalRRequestHandler;

            _hubConnection = new HubConnection(Constants.ServerAddress);
            _hubConnection.Headers.Add("ZUMO-API-VERSION", "2.0.0");
            _deviceConnectionHubProxy = _hubConnection.CreateHubProxy("DeviceConnectionHub");

            AddHandlers();
        }

        private void AddHandlers()
        {
            _hubConnection.Closed += OnDisconnected;
            _hubConnection.Error += OnError;
            _hubConnection.StateChanged += OnStateChanged;
            _hubConnection.Reconnecting += Reconnecting;
            _hubConnection.Reconnected += Reconnected;
            _hubConnection.ConnectionSlow += ConnectionSlow;
        }

        public async Task StartConnection()
        {
            OnLog(this, DateTime.Now + ": Starting connection...");

            _lightStateSignalRRequestHandler.InitializeHubProxy(_hubConnection);

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
