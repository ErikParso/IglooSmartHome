using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace IglooSmartHomeService.SignalR
{
    public class DeviceConnectionHub : Hub
    {
        private static readonly HashSet<string> _connectedIds = new HashSet<string>();

        public override Task OnConnected()
        {
            _connectedIds.Add(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _connectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public IEnumerable<string> GetConnectedDevices()
            => _connectedIds;
    }
}