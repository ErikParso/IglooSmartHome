using IglooSmartHome.Models;
using System.Threading.Tasks;

namespace IglooSmartHome.Services
{
    public interface IDeviceInformationService
    {
        Task<DeviceInformation> GetDeviceInformationAsync(int deviceId);
    }
}
