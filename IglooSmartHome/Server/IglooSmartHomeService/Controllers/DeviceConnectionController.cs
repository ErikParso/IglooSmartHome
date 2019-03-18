using System.Web.Http;
using IglooSmartHomeService.SignalR;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class DeviceConnectionController : ApiController
    {
        private readonly ConnectionMapping<string> _deviceConnectionMapping;

        public DeviceConnectionController(ConnectionMapping<string> deviceConnectionMapping)
        {
            _deviceConnectionMapping = deviceConnectionMapping;
        }

        public IHttpActionResult Get()
            => Ok(_deviceConnectionMapping.GetConnections());
    }
}
