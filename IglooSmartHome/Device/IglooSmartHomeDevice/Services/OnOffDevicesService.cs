using IglooSmartHomeDevice.Extesions;
using IglooSmartHomeDevice.Model;
using IglooSmartHomeDevice.RefitInterfaces;
using Refit;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class OnOffDevicesService
    {
        private const string onOffDeviceNotFoundException = "OnOffDeviceNotFoundException";

        private readonly IOnOffDeviceRefitService _onOffDeviceRefitService;
        private readonly SmarthomeConfigurationService _smarthomeConfigurationService;

        public OnOffDevicesService(
            IOnOffDeviceRefitService onOffDeviceRefitService,
            SmarthomeConfigurationService smarthomeConfigurationService)
        {
            _onOffDeviceRefitService = onOffDeviceRefitService;
            _smarthomeConfigurationService = smarthomeConfigurationService;
        }

        public async Task<OnOffDeviceState> GetOnOffDeviceState(string onOffDeviceId)
        {
            try
            {
                var onOffDeviceInfo = await _onOffDeviceRefitService.GetOnOffDeviceInfoAsync(onOffDeviceId);
                return onOffDeviceInfo.State;
            }
            catch (ApiException ex)
            {
                if (ex.GetFilteredExceptionName() == onOffDeviceNotFoundException)
                    return await RegisterOnOffDevice(onOffDeviceId);
                else
                    throw;
            }
        }

        private async Task<OnOffDeviceState> RegisterOnOffDevice(string onOffDeviceId)
        {
            var onOffDeviceConfiguration = _smarthomeConfigurationService.GetOnOffDeviceConfiguration(onOffDeviceId);
            var result = await _onOffDeviceRefitService.RegisterOnOffDeviceAsync(new OnOffDeviceViewModel()
            {
                Id = onOffDeviceConfiguration.Id,
                Name = onOffDeviceConfiguration.Name,
                State = onOffDeviceConfiguration.InitialState,
                Type = onOffDeviceConfiguration.Type,
            });
            return onOffDeviceConfiguration.InitialState;
        }
    }
}
