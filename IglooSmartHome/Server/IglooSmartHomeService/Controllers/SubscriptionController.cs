using Azure.Server.Utils.Extensions;
using Azure.Server.Utils.Results;
using IglooSmartHome.Models;
using IglooSmartHomeService.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [Authorize]
        public IHttpActionResult Get()
        {
            var acc = this.GetCurrentUserAccount(_context.Accounts);
            var myDevices = _context.DeviceSubscriptions
                .Include("Device")
                .Where(subs => subs.AccountId == acc.Id)
                .Select(subs => new {
                    subs.Id,
                    subs.DeviceId,
                    subs.Device.CustomDeviceName,
                    subs.Role,
                });
            return Ok(myDevices);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult PostInitializeDeviceOwner(string deviceCode, string customDeviceName)
        {
            var acc = this.GetCurrentUserAccount(_context.Accounts);

            var device = _context.Devices.SingleOrDefault(d => d.Sid == deviceCode);
            if (device == null)
                return BadRequest($"Device with identified '{deviceCode}' is not registered.");

            var ownerSubscriptions = _context.DeviceSubscriptions
                .Where(s => s.DeviceId == device.Id && s.Role == SubscriptionRole.Owner);
            if (ownerSubscriptions.Count() > 0)
                return BadRequest("Device owner is already initialzied.");

            if (!string.IsNullOrWhiteSpace(customDeviceName))
                device.CustomDeviceName = customDeviceName;

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
        [Authorize]
        public IHttpActionResult Delete(int subscriptionId)
        {
            var acc = this.GetCurrentUserAccount(_context.Accounts);
            var subs = _context.DeviceSubscriptions
                .Where(s => s.Id == subscriptionId)
                .SingleOrDefault();
            if (subs == null)
                return BadRequest($"Subscription with id '{subscriptionId}' does not exists.");

            if (subs.AccountId == acc.Id)
            {
                _context.DeviceSubscriptions.Remove(subs);
                _context.SaveChanges();
                return Ok("Unsubscribed to device");
            }

            var ownerSubs = _context.DeviceSubscriptions
                .Where(s => s.DeviceId == subs.DeviceId && s.Role == SubscriptionRole.Owner)
                .SingleOrDefault();

            if (ownerSubs.AccountId == acc.Id)
            {
                _context.DeviceSubscriptions.Remove(subs);
                _context.SaveChanges();
                return Ok("Unsubscribed to device");
            }

            return new ForbiddenResult(Request, "You are not alowed to unsubscribe device.");
        }
    }
}
