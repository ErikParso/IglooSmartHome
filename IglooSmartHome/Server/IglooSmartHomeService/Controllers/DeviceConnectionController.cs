using System.Web.Http;
using IglooSmartHomeService.Services;
using IglooSmartHomeService.SignalR;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class DeviceConnectionController : ApiController
    {
        private readonly ConnectionMapping<int> _connections;

        public DeviceConnectionController(DeviceConnectionsMappingService connections)
        {
            _connections = connections;
        }

        public IHttpActionResult Get()
            => Ok(_connections.GetConnections());
    }
}
