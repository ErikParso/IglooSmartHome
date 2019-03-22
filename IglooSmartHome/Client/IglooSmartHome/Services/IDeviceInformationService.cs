using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IglooSmartHome.Services
{
    public interface IDeviceInformationService
    {
        Task<string> GetDeviceName(int deviceId);
    }
}
