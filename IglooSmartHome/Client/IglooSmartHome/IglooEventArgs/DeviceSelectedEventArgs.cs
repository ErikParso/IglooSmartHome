using System;

namespace IglooSmartHome.IglooEventArgs
{
    public class DeviceSelectedEventArgs : EventArgs
    {
        public DeviceSelectedEventArgs(int deviceId)
        {
            DeviceId = deviceId;
        }

        public readonly int DeviceId;
    }
}
