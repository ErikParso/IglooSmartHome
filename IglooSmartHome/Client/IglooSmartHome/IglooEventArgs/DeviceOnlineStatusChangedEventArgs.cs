using System;

namespace IglooSmartHome.IglooEventArgs
{
    public class DeviceOnlineStatusChangedEventArgs : EventArgs
    {
        public DeviceOnlineStatusChangedEventArgs(int deviceId, bool onlineStatus)
        {
            DeviceId = deviceId;
            OnlineStatus = onlineStatus;
        }

        public readonly int DeviceId;

        public readonly bool OnlineStatus;
    }
}
