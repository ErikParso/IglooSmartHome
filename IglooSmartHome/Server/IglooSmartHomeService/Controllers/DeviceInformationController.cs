using System.Linq;
using System.Net;
using System.Web.Http;
using Azure.Server.Utils.Extensions;
using Azure.Server.Utils.Results;
using IglooSmartHome.Models;
using IglooSmartHomeService.ExceptionFilters;
using IglooSmartHomeService.Exceptions;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class DeviceInformationController : ApiController
    {
        private readonly IglooSmartHomeContext _context;

        public DeviceInformationController(IglooSmartHomeContext context)
        {
            _context = context;
        }

        [Authorize]
        [ExceptionFilter]
        public IHttpActionResult GetDeviceInformation(int deviceId)
        {
            // Check if device exists.
            var device = _context.Devices.SingleOrDefault(d => d.Id == deviceId);
            if (device == null)
                throw new DeviceNotFoundException(deviceId);

            // Check device information permission.
            var acc = User.GetCurrentUserAccount(_context.Accounts);
            var subs = _context.DeviceSubscriptions
                .SingleOrDefault(s => s.AccountId == acc.Id && s.DeviceId == deviceId);
            if (subs == null)
            {
                return new ForbiddenResult(Request, $"You have no access to information about device with id '{deviceId}'.");
            }

            // Return device information.
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
