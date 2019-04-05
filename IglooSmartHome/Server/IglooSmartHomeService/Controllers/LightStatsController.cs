using System.Web.Http;
using IglooSmartHomeService.ExceptionFilters;
using IglooSmartHomeService.Services;
using IglooSmartHomeService.SignalR;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class LightStatsController : ApiController
    {
        private readonly LightStatsHub _lightstatsHub;
        private readonly DevicesService _devicesService;

        public LightStatsController(
            LightStatsHub lightStatsHub,
            DevicesService devicesService)
        {
            _lightstatsHub = lightStatsHub;
            _devicesService = devicesService;
        }

        [Authorize]
        [ExceptionFilter]
        public IHttpActionResult Get(int deviceId)
        {
            _devicesService.GetDeviceWithPermissions(deviceId, User);
            var lightState = _lightstatsHub.SendMessageAndWaitForResponse(deviceId, "param0002");
            return Ok(lightState);
        }
    }
}
