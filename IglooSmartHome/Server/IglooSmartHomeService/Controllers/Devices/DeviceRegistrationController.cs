using Azure.Server.Utils.CustomAuthentication;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System.Data.Entity;

namespace IglooSmartHomeService.Controllers.Devices
{
    [MobileAppController]
    public class DeviceRegistrationController : CustomRegistrationController<IglooSmartHomeContext, Device>
    {
        public DeviceRegistrationController(IglooSmartHomeContext context) : base(context)
        {
        }

        protected override DbSet<Device> GetAccountsDbSet(IglooSmartHomeContext fromContext)
        {
            return fromContext.Devices;
        }
    }
}