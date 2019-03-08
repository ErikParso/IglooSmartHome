﻿using IglooSmartHome.Models;
using IglooSmartHome.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class DeviceSubscriptionsViewModel : INotifyPropertyChanged
    {
        private readonly DeviceSubscriptionsService _devicesService;
        private IEnumerable<DeviceSubscription> _subscriptions;

        public DeviceSubscriptionsViewModel(DeviceSubscriptionsService devicesService)
        {
            _devicesService = devicesService;
        }

        public IEnumerable<DeviceSubscription> Subscriptions {
            get => _subscriptions;
            set { _subscriptions = value; RaisePropertyChanged(); }
        }

        public async Task ReloadDevicesAsync()
            => Subscriptions = await _devicesService.GetDeviceSubscriptionsAsync();

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}