using IglooSmartHomeService.ExceptionFilters;
using IglooSmartHomeService.Services;
using Microsoft.Azure.Mobile.Server.Config;
using System.Web.Http;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class DeviceOnlineStatusController : ApiController
    {
        private readonly DevicesService _devicesService;
        private readonly DeviceConnectionsMappingService _deviceConnectionsMappingService;

        public DeviceOnlineStatusController(
            DevicesService devicesService,
            DeviceConnectionsMappingService deviceConnectionsMappingService)
        {
            _devicesService = devicesService;
            _deviceConnectionsMappingService = deviceConnectionsMappingService;
        }

        [Authorize]
        [ExceptionFilter]
        public IHttpActionResult Get(int deviceId)
        {
             _devicesService.GetDeviceWithPermissions(deviceId, User);
            return Ok(_deviceConnectionsMappingService.IsConnected(deviceId));
        }
    }
}
