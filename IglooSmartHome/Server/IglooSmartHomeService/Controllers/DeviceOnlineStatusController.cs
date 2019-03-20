using System.Linq;
using System.Web.Http;
using Azure.Server.Utils.Extensions;
using Azure.Server.Utils.Results;
using IglooSmartHome.Models;
using IglooSmartHomeService.Services;
using IglooSmartHomeService.SignalR;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class DeviceOnlineStatusController : ApiController
    {
        private readonly ConnectionMapping<int> _connections;
        private readonly IglooSmartHomeContext _context;

        public DeviceOnlineStatusController(
            DeviceConnectionsMappingService connections,
            IglooSmartHomeContext context)
        {
            _connections = connections;
            _context = context;
        }

        [Authorize]
        public IHttpActionResult Get(int deviceId)
        {
            var acc = User.GetCurrentUserAccount(_context.Accounts);
            var subs = _context.DeviceSubscriptions
                .SingleOrDefault(s => s.DeviceId == deviceId && s.AccountId == acc.Id);
            return subs != null
                ? Ok(_connections.GetConnections().ContainsKey(deviceId))
                : (IHttpActionResult)new ForbiddenResult(Request, "You have no access to device information.");
        }
    }
}
