using System;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class DetailViewViewModel
    {
        public DetailViewViewModel(
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
