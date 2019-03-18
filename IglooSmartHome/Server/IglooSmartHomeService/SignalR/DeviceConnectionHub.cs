using Autofac;
using IglooSmartHomeService.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IglooSmartHomeService.SignalR
{
    [Authorize]
    public class DeviceConnectionHub : Hub
    {
        private readonly ConnectionMapping<string> _connections;

        public DeviceConnectionHub(DeviceConnectionsMappingService connections)
        {
            _connections = connections;
        }

        public override Task OnConnected()
        {
            string name = GetCurrentUserId();
            _connections.Add(name, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = GetCurrentUserId();
            _connections.Remove(name, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = GetCurrentUserId();
            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
                _connections.Add(name, Context.ConnectionId);
            return base.OnReconnected();
        }

        private string GetCurrentUserId()
            => ((ClaimsPrincipal)Context.User).FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}