using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Model.SmarthomeConfiguration
{
    public class SmarthomeConfiguration
    {
        public IEnumerable<OnOffDeviceConfiguration> OnOffDevices { get; set; }
    }
}
