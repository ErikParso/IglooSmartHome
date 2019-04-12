using IglooSmartHomeService.ExceptionFilters;
using IglooSmartHomeService.Services;
using Microsoft.Azure.Mobile.Server.Config;
using System.Web.Http;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class DeviceInformationController : ApiController
    {
        private readonly DevicesService _devicesService;

        public DeviceInformationController(DevicesService devicesService)
        {
            _devicesService = devicesService;
        }

        [Authorize]
        [ExceptionFilter]
        public IHttpActionResult GetDeviceInformation(int deviceId)
        {
            var device = _devicesService.GetDevice(deviceId, User);
            return Ok(new
            {
                device.Id,
                device.Sid,
                device.CustomDeviceName,
                device.Verified
            });
        }
    }
}
