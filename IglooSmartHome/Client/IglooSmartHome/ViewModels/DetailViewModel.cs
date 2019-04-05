using System;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class DetailViewModel
    {
        public DetailViewModel(
            DeviceHeaderViewModel deviceHeaderViewModel,
            DeviceControllerViewModel deviceControllerViewModel)
        {
            DeviceHeaderViewModel = deviceHeaderViewModel;
            DeviceControllerViewModel = deviceControllerViewModel;
        }

        public DeviceHeaderViewModel DeviceHeaderViewModel { get; private set; }
        public DeviceControllerViewModel DeviceControllerViewModel { get; private set; }
    }
}
