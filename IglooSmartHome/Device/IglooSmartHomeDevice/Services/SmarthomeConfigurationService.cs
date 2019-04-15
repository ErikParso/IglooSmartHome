using IglooSmartHomeDevice.Extesions;
using IglooSmartHomeDevice.Model;
using IglooSmartHomeDevice.Model.SmarthomeConfiguration;
using IglooSmartHomeDevice.RefitInterfaces;
using Newtonsoft.Json;
using Refit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class SmarthomeConfigurationService
    {
        private const string smarthomeConfigurationFile = "SmarthomeConfiguration.json";

        public readonly SmarthomeConfiguration Configuration;

        public SmarthomeConfigurationService(IOnOffDeviceRefitService onOffDeviceRefitService)
        {
            Configuration = JsonConvert.DeserializeObject<SmarthomeConfiguration>(
                File.ReadAllText(smarthomeConfigurationFile));
        }

        public OnOffDeviceConfiguration GetOnOffDeviceConfiguration(string onOffDeviceId)
            => Configuration.OnOffDevices.First(d => d.Id == onOffDeviceId);
    }
}