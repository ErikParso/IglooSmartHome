using Azure.Server.Utils.CustomAuthentication;
using IglooSmartHome.Models;
using IglooSmartHomeService.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Data.Entity;

namespace IglooSmartHome.Controllers.Devices
{
    [MobileAppController]
    public class DeviceLoginController : CustomLoginController<IglooSmartHomeContext, Device>
    {
        public DeviceLoginController(IglooSmartHomeContext context)
            : base(context, "https://igloosmarthome.azurewebsites.net/", TimeSpan.FromSeconds(180))
        {

        }

        protected override DbSet<Device> GetAccountsDbSet(IglooSmartHomeContext fromContext)
        {
            return fromContext.Devices;
        }
    }
}