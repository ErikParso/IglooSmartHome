using IglooSmartHomeDevice.Model;
using Refit;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.RefitInterfaces
{
    public interface IOnOffDeviceRefitService
    {
        [Get("/api/OnOffDevice?onOffDeviceId={onOffDeviceId}")]
        Task<OnOffDeviceInfo> GetOnOffDeviceInfoAsync(string onOffDeviceId);

    }
}
