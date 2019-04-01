using System.Threading.Tasks;

namespace IglooSmartHome.Services
{
    public class DeviceInformationService : IDeviceInformationService
    {
        public async Task<string> GetDeviceName(int deviceId)
        {
            return await Task.FromResult("My Device");
        }
    }
}
