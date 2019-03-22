using System;
using System.Collections.Generic;
using System.Text;

namespace IglooSmartHome.Services
{
    public interface IDeviceSelectionService
    {
        event EventHandler<int?> SelectedDeviceChanged;

        int? SelectedDeviceId { get; }

        void SetSelectedDeviceId(int? deviceId);
    }
}
