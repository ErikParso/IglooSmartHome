using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IglooSmartHomeService.SignalR
{
    public class DeviceConnectionHub : Hub
    {
        private static readonly List<(string, string)> _connectedIds = new List<(string, string)>();

        public override Task OnConnected()
        {
            _connectedIds.Add((DateTime.Now + " - OnConnected", Context.ConnectionId));
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _connectedIds.Add((DateTime.Now + " - OnDisconnected", Context.ConnectionId));
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            _connectedIds.Add((DateTime.Now + " - OnReconnected", Context.ConnectionId));
            return base.OnReconnected();
        }

        public List<(string, string)> GetConnectedDevices()
            => _connectedIds;

        public void Clear()
            => _connectedIds.Clear();
    }
}