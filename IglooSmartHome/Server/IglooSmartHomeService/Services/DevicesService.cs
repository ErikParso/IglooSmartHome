using Azure.Server.Utils.Extensions;
using IglooSmartHome.Controllers;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using IglooSmartHomeService.DataObjects;
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

        public Device GetDevice(int deviceId, IPrincipal user)
        {
            var loginType = user.GetAuthenticatedByClaim();
            if (string.IsNullOrEmpty(loginType) || loginType == nameof(CustomLoginController))
            {
                // User wants access device info.
                var userAccount = user.GetCurrentUserAccount(_context.Accounts);
                return GetDevice(deviceId, userAccount);
            }
            else
            {
                // Device wants access device info.
                var deviceAccount = user.GetCurrentUserAccount(_context.Devices);
                return GetDevice(deviceId, deviceAccount);
            }
        }

        private Device GetDevice(int deviceId, Account account)
        {
            var device = GetDevice(deviceId);
            var subs = _context.DeviceSubscriptions
                .SingleOrDefault(s => s.AccountId == account.Id && s.DeviceId == deviceId);
            if (subs == null)
                throw new DevicePermissionException(deviceId);
            else
                return device;
        }

        private Device GetDevice(int deviceId, Device account)
        {
            var device = GetDevice(deviceId);
            if (account.Id != device.Id)
                throw new DevicePermissionException(deviceId);
            return device;
        }
    }
}