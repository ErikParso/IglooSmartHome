using IglooSmartHomeDevice.Model.SmarthomeConfiguration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class SmarthomeConfigurationService
    {
        private const string smarthomeConfigurationFile = "SmarthomeConfiguration.json";
        public readonly SmarthomeConfiguration Configuration;

        public SmarthomeConfigurationService()
        {
            Configuration = JsonConvert.DeserializeObject<SmarthomeConfiguration>(
                File.ReadAllText(smarthomeConfigurationFile));
        }
    }
}
