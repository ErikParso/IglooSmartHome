using Azure.Server.Utils.Extensions;
using IglooSmartHome.Models;
using IglooSmartHomeService.Services;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            => Clients.Clients(GetUserConnectionsSubscribedForDevice(e)
                .ToList())
                .deviceOnline(e);

        private void NotifyDeviceOffline(object sender, int e)
             => Clients.Clients(GetUserConnectionsSubscribedForDevice(e)
                .ToList())
                .deviceOffline(e);

        public override Task OnConnected()
        {
            var userId = Context.User.GetCurrentUserAccount(_context.Accounts).Id;
            _userConnections.Add(userId, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userId = Context.User.GetCurrentUserAccount(_context.Accounts).Id;
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

        private string GetCurrentUserId()
            => ((ClaimsPrincipal)Context.User).FindFirst(ClaimTypes.NameIdentifier).Value;

        private IEnumerable<string> GetUserConnectionsSubscribedForDevice(int deviceId)
            => GetUsersSubscribedForDevice(deviceId)
               .SelectMany(userId => _userConnections.GetConnections(userId));

        private IEnumerable<int> GetUsersSubscribedForDevice(int deviceId)
            => _context.DeviceSubscriptions
                .Where(subs => subs.DeviceId == deviceId)
                .Select(subs => subs.AccountId);
    }
}