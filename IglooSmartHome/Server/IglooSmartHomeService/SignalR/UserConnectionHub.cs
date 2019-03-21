using Azure.Server.Utils.Extensions;
using IglooSmartHome.Models;
using IglooSmartHomeService.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IglooSmartHomeService.SignalR
{
    [Authorize]
    public class UserConnectionHub : Hub
    {
        private readonly ConnectionMapping<int> _userConnections;
        private readonly ConnectionMapping<int> _deviceConnections;
        private readonly IglooSmartHomeContext _context;

        public UserConnectionHub(
            UserConnectionsMappingService userConnections,
            DeviceConnectionsMappingService deviceConnections,
            IglooSmartHomeContext context)
        {
            _userConnections = userConnections;
            _deviceConnections = deviceConnections;
            _context = context;
            AddHandlers();
        }

        private void AddHandlers()
        {
            _deviceConnections.Offline += NotifyDeviceOffline;
            _deviceConnections.Online += NotifyDeviceOnline;
        }

        private void NotifyDeviceOnline(object sender, int e)
        {
            try
            {
                var connectionIds = GetUserConnectionsSubscribedForDevice(e);
                Trace.TraceInformation(
                    $"Notifying users device with id '{e}' is online. User connections: {string.Join(", ", connectionIds)}");
                Clients.Clients(connectionIds)
                    .deviceOnline(e);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Device online notification error:" + ex.Message);
            }
        }

        private void NotifyDeviceOffline(object sender, int e)
        {
            try
            {
                var connectionIds = GetUserConnectionsSubscribedForDevice(e);
                Trace.TraceInformation(
                    $"Notifying users device with id '{e}' is offline. User connections: {string.Join(", ", connectionIds)}");
                Clients.Clients(connectionIds)
                    .deviceOffline(e);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Device online notification error:" + ex.Message);
            }
        }

        public override Task OnConnected()
        {
            var userId = Context.User.GetCurrentUserAccount(_context.Accounts).Id;
            Trace.TraceInformation(
                $"User with id '{userId}' is connected with connection id '{Context.ConnectionId}'");
            _userConnections.Add(userId, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userId = Context.User.GetCurrentUserAccount(_context.Accounts).Id;
            Trace.TraceInformation(
                $"User with id '{userId}' was disconnected with connection id '{Context.ConnectionId}'");
            _userConnections.Remove(userId, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var userId = Context.User.GetCurrentUserAccount(_context.Accounts).Id;
            if (!_userConnections.GetConnections(userId).Contains(Context.ConnectionId))
                _userConnections.Add(userId, Context.ConnectionId);
            return base.OnReconnected();
        }

        private List<string> GetUserConnectionsSubscribedForDevice(int deviceId)
            => GetUsersSubscribedForDevice(deviceId)
                .SelectMany(userId => _userConnections.GetConnections(userId))
                .ToList();

        private IEnumerable<int> GetUsersSubscribedForDevice(int deviceId)
            => _context.DeviceSubscriptions
                .Where(subs => subs.DeviceId == deviceId)
                .Select(subs => subs.AccountId);
    }
}