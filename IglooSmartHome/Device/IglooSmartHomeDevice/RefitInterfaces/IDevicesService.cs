using IglooSmartHomeDevice.Model;
using Refit;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.RefitInterfaces
{
    public interface IDevicesService
    {
        [Post("/api/Devices")]
        Task<string> GetDeviceInfoAsync();
    }
}
