using Azure.Server.Utils.Extensions;
using IglooSmartHomeService.DataObjects;
using IglooSmartHomeService.ExceptionFilters;
using IglooSmartHomeService.Services;
using IglooSmartHomeService.ViewModels;
using Microsoft.Azure.Mobile.Server.Config;
using System.Web.Http;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class OnOffDeviceController : ApiController
    {
        private readonly OnOffDevicesService _onOffDevicesService;

        public OnOffDeviceController(OnOffDevicesService onOffDevicesService)
        {
            _onOffDevicesService = onOffDevicesService;
        }

        [HttpGet]
        [Authorize]
        [ExceptionFilter]
        public IHttpActionResult GetOnOffDeviceInfo(string onOffDeviceId)
        {
            var onOffDevice = _onOffDevicesService.GetOnOffDevice(onOffDeviceId, User);
            return Ok(new
            {
                onOffDevice.Id,
                onOffDevice.DeviceId,
                onOffDevice.Name,
                onOffDevice.Type,
                onOffDevice.State
            });
        }

        [HttpPut]
        [Authorize]
        [ExceptionFilter]
        public IHttpActionResult RegisterOnOffDevice(OnOffDeviceViewModel viewModel)
        {
            _onOffDevicesService.RegisterOnOffDevice(viewModel, User);
            return Ok("Device successfully registered");
        }

        [HttpPost]
        [Authorize]
        [ExceptionFilter]
        public IHttpActionResult UpdateOnOffDeviceState(string onOffDeviceId, OnOffDeviceState state)
        {
            _onOffDevicesService.UpdateOnOffDeviceState(onOffDeviceId, state, User);
            return Ok("Device state updated.");
        }
    }
}
