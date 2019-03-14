using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class DeviceConnectionService
    {
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _deviceConnectionHubProxy;
        private readonly AuthenticationService _authenticationService;

        public DeviceConnectionService(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _hubConnection = new HubConnection(Environment.ServerAddress);
            _deviceConnectionHubProxy = _hubConnection.CreateHubProxy("DeviceConnectionHub");
        }

        public async Task Connect()
        {
            _hubConnection.ConnectionId = "bc10b038-2c1a-4ccc-a504-d1a040403981";
            await _hubConnection.Start();
        }
    }
}
