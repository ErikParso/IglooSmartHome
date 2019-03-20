using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using Xamarin.Forms.Utils.Services;

namespace IglooSmartHome.SignalR
{
    public class SignalRConnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IAuthenticationService _authenticationService;

        public readonly IHubProxy UserConnectionHubProxy;

        public SignalRConnectionService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _hubConnection = new HubConnection(Constants.ApplicationURL);
            _hubConnection.Headers.Add("ZUMO-API-VERSION", "2.0.0");

            UserConnectionHubProxy = _hubConnection.CreateHubProxy("UserConnectionHub");
        }

        public async Task StartConnection()
        {
            _hubConnection.Headers["X-ZUMO-AUTH"] = await _authenticationService.Authenticate();
            await _hubConnection.Start();
        }

        internal void StopConnection()
        {
            _hubConnection.Stop();
        }
    }
}
