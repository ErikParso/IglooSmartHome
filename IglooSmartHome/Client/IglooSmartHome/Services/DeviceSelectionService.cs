using System;
using System.Collections.Generic;
using System.Text;

namespace IglooSmartHome.Services
{
    public class DeviceSelectionService : IDeviceSelectionService
    {
        public int? SelectedDeviceId { get; private set; }

        public event EventHandler<int?> SelectedDeviceChanged;

        public void SetSelectedDeviceId(int? deviceId)
        {
            SelectedDeviceId = deviceId;
            SelectedDeviceChanged?.Invoke(this, deviceId);
        }
    }
}
