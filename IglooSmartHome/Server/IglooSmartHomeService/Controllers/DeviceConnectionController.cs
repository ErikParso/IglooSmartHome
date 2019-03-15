using System.Web.Http;
using IglooSmartHomeService.SignalR;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class DeviceConnectionController : ApiController
    {
        private readonly DeviceConnectionHub _deviceConnectionHub;

        public DeviceConnectionController(DeviceConnectionHub deviceConnectionHub)
        {
            _deviceConnectionHub = deviceConnectionHub;
        }

        public IHttpActionResult Get()
            => Ok(_deviceConnectionHub.GetConnectedDevices());

        public IHttpActionResult Delete()
        {
            _deviceConnectionHub.Clear();
            return Ok();
        }
    }
}
