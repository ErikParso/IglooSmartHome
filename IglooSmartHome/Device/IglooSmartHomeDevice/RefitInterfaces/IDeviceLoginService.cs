using IglooSmartHomeDevice.Model;
using Refit;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.RefitInterfaces
{
    public interface IDeviceLoginService
    {
        [Post("/api/DeviceLogin")]
        Task<CustomLoginResult> LoginDeviceAsync([Body]CustomLoginRequest loginrequest);

        [Get("/api/DeviceLogin?userId={deviceName}&refreshToken={refreshToken}")]
        Task<CustomLoginResult> RefreshTokenAsync(string deviceName, string refreshToken);

    }
}
