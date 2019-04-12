using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Model.SmarthomeConfiguration
{
    public class OnOffDeviceConfiguration
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public OnOffDeviceType Type { get; set; }

        public OnOffDeviceState InitialState { get; set; }

        public OnOffDeviceConfigurationI2C ConfigurationI2C { get; set; }

        public OnOffDeviceConfigurationGPIO ConfigurationGPIO { get; set; }

    }
}
