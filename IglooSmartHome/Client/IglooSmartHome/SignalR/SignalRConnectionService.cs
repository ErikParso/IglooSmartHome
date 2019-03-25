using IglooSmartHome.Services;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Utils.Services;

namespace IglooSmartHome.SignalR
{
    public class SignalRConnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogService _logService;

        public readonly IHubProxy UserConnectionHubProxy;

        public SignalRConnectionService(
            IAuthenticationService authenticationService,
            ILogService logService)
        {
            _authenticationService = authenticationService;
            _logService = logService;
            _hubConnection = new HubConnection(Constants.ApplicationURL);
            _hubConnection.Headers.Add("ZUMO-API-VERSION", "2.0.0");

            UserConnectionHubProxy = _hubConnection.CreateHubProxy("UserConnectionHub");

            AddHandlers();
        }

        private void AddHandlers()
        {
            _hubConnection.Closed += _hubConnection_Closed;
            _hubConnection.ConnectionSlow += _hubConnection_ConnectionSlow;
            _hubConnection.Error += _hubConnection_Error;
            _hubConnection.Received += _hubConnection_Received;
            _hubConnection.Reconnected += _hubConnection_Reconnected;
            _hubConnection.Reconnecting += _hubConnection_Reconnecting;
            _hubConnection.StateChanged += _hubConnection_StateChanged;
        }

        private void _hubConnection_StateChanged(StateChange obj)
            => _logService.LogMesage($"Connection state changed from {obj.OldState} to {obj.NewState}.");

        private void _hubConnection_Reconnecting()
            => _logService.LogMesage($"Reconnecting...");

        private void _hubConnection_Reconnected()
            => _logService.LogMesage($"Reconnected.");

        private void _hubConnection_Received(string obj)
            => _logService.LogMesage($"Received {obj}");

        private void _hubConnection_Error(Exception obj)
            => _logService.LogMesage($"Connection error: {obj.Message}");

        private void _hubConnection_ConnectionSlow()
            => _logService.LogMesage($"Connection slow...");

        private void _hubConnection_Closed()
            => _logService.LogMesage($"Connection closed.");

        public async Task StartConnection()
        {
            _logService.LogMesage($"Starting connection...");
            _hubConnection.Headers["X-ZUMO-AUTH"] = await _authenticationService.Authenticate();
            await _hubConnection.Start();
            _logService.LogMesage($"Connection started.");
        }

        internal void StopConnection()
        {
            _logService.LogMesage($"Stopping connection...");
            _hubConnection.Stop();
            _logService.LogMesage($"Stopping stopped.");
        }
    }
}
