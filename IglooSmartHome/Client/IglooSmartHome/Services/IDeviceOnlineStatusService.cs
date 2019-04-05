using IglooSmartHome.IglooEventArgs;
using System;
using System.Threading.Tasks;

namespace IglooSmartHome.Services
{
    public interface IDeviceOnlineStatusService
    {
        event EventHandler<DeviceOnlineStatusChangedEventArgs> DeviceOnlineStatusChanged;

        Task<bool> IsDeviceOnline(int deviceId);

        void ClearCache();
    }
}
