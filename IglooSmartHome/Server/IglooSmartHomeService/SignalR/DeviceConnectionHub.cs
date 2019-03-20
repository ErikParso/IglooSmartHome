using Azure.Server.Utils.Extensions;
using IglooSmartHome.Models;
using IglooSmartHomeService.Services;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IglooSmartHomeService.SignalR
{
    [Authorize]
    public class DeviceConnectionHub : Hub
    {
        private readonly ConnectionMapping<int> _connections;
        private readonly IglooSmartHomeContext _context;

        public DeviceConnectionHub(
            DeviceConnectionsMappingService connections,
            IglooSmartHomeContext context)
        {
            _connections = connections;
            _context = context;
        }

        public override Task OnConnected()
        {
            var deviceId = Context.User.GetCurrentUserAccount(_context.Devices).Id;
            Trace.TraceInformation(
                $"Device with id '{deviceId}' was connected with connection id '{Context.ConnectionId}'");
            _connections.Add(deviceId, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var deviceId = Context.User.GetCurrentUserAccount(_context.Devices).Id;
            Trace.TraceInformation(
                $"Device with id '{deviceId}' was disconnected with connection id '{Context.ConnectionId}'");
            _connections.Remove(deviceId, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var deviceId = Context.User.GetCurrentUserAccount(_context.Devices).Id;
            if (!_connections.GetConnections(deviceId).Contains(Context.ConnectionId))
                _connections.Add(deviceId, Context.ConnectionId);
            return base.OnReconnected();
        }
    }
}