using Azure.Server.Utils.Extensions;
using IglooSmartHome.Models;
using IglooSmartHomeService.DataObjects;
using IglooSmartHomeService.Exceptions;
using IglooSmartHomeService.ViewModels;
using System.Linq;
using System.Security.Principal;

namespace IglooSmartHomeService.Services
{
    public class OnOffDevicesService
    {
        private readonly IglooSmartHomeContext _context;
        private readonly DevicesService _deviceService;

        public OnOffDevicesService(
            IglooSmartHomeContext context,
            DevicesService deviceService)
        {
            _context = context;
            _deviceService = deviceService;
        }

        public OnOffDevice GetOnOffDevice(string onOffDeviceId, IPrincipal user)
        {
            var onOffDevice = GetOnOffDevice(onOffDeviceId);
            var device = _deviceService.GetDevice(onOffDevice.DeviceId, user);
            return onOffDevice;
        }

        public OnOffDevice GetOnOffDevice(string onOffDeviceId)
        {
            var onOffDevice = _context.OnOffDevices
                .SingleOrDefault(d => d.Id == onOffDeviceId);
            if (onOffDevice == null)
                throw new OnOffDeviceNotFoundException(onOffDeviceId);
            return onOffDevice;
        }

        public void RegisterOnOffDevice(OnOffDeviceViewModel viewModel, IPrincipal user)
        {
            var deviceAccount = user.GetCurrentUserAccount(_context.Devices);
            if (deviceAccount == null)
                throw new PermissionException("OnOff device can be registered only by hosting device.");

            var onOffDevice = _context.OnOffDevices
                .SingleOrDefault(d => d.Id == viewModel.Id);
            if (onOffDevice != null)
                throw new OnOffDeviceAlreadyRegisteredException(viewModel.Id);

            onOffDevice = new OnOffDevice()
            {
                Id = viewModel.Id,
                Device = deviceAccount,
                DeviceId = deviceAccount.Id,
                Name = viewModel.Name,
                State = viewModel.State,
                Type = viewModel.Type,
            };
            _context.OnOffDevices.Add(onOffDevice);
            _context.SaveChanges();
        }
    }
}