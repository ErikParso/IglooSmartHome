﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IglooSmartHome.Services
{
    public class DeviceInformationService : IDeviceInformationService
    {
        public async Task<string> GetDeviceName(int deviceId)
        {
            return await Task.FromResult("My Device");
        }
    }
}