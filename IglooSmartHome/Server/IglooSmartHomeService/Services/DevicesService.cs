using Azure.Server.Utils.Extensions;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using IglooSmartHomeService.Exceptions;
using System.Linq;
using System.Security.Principal;

namespace IglooSmartHomeService.Services
{
    public class DevicesService
    {
        private readonly IglooSmartHomeContext _context;

        public DevicesService(IglooSmartHomeContext context)
        {
            _context = context;
        }

        public Device GetDevice(int deviceId)
        {
            var device = _context.Devices.SingleOrDefault(d => d.Id == deviceId);
            if (device == null)
                throw new DeviceNotFoundException(deviceId);
            else
                return device;
        }

        public Device GetDeviceWithPermissions(int deviceId, IPrincipal user)
        {
            var account = user.GetCurrentUserAccount(_context.Accounts);
            return GetDeviceWithPermissions(deviceId, account.Id);
        }

        public Device GetDeviceWithPermissions(int deviceId, int userId)
        {
            var device = GetDevice(deviceId);
            var subs = _context.DeviceSubscriptions
                .SingleOrDefault(s => s.AccountId == userId && s.DeviceId == deviceId);
            if (subs == null)
                throw new DevicePermissionException(deviceId);
            else
                return device;
        }
    }
}