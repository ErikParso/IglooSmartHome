using Azure.Server.Utils.Extensions;
using IglooSmartHome.Models;
using IglooSmartHomeService.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using System.Linq;
using System.Web.Http;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class SubscriptionController : ApiController
    {
        private readonly IglooSmartHomeContext _context;

        public SubscriptionController(IglooSmartHomeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_context.DeviceSubscriptions);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult PostInitializeDeviceOwner(string deviceName)
        {
            var acc = this.GetCurrentUserAccount(_context.Accounts);

            var device = _context.Devices.SingleOrDefault(d => d.Sid == deviceName);
            if (device == null)
                return BadRequest($"Device with identified '{deviceName}' is not registered.");

            var ownerSubscriptions = _context.DeviceSubscriptions
                .Where(s => s.DeviceId == device.Id && s.Role == SubscriptionRole.Owner);
            if (ownerSubscriptions.Count() > 0)
                return BadRequest("Device owner is already initialzied.");

            var subscription = new DeviceSubscription()
            {
                AccountId = acc.Id,
                Account = acc,
                DeviceId = device.Id,
                Device = device,
                Role = SubscriptionRole.Owner,
            };
            _context.DeviceSubscriptions.Add(subscription);
            _context.SaveChanges();
            return Ok("You was set as device owner");
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM [DeviceSubscriptions]");
            return Ok("Table was cleared.");
        }
    }
}
