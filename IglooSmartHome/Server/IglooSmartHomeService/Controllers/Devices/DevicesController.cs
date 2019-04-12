using Azure.Server.Utils.CustomAuthentication;
using IglooSmartHome.Models;
using IglooSmartHomeService.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;

namespace IglooSmartHome.Controllers.Devices
{
    [MobileAppController]
    public class DevicesController : AccountsController<IglooSmartHomeContext, Device>
    {
        private readonly IglooSmartHomeContext _context;

        public DevicesController(IglooSmartHomeContext context)
            : base(context)
        {
            _context = context;
        }

        [HttpDelete]
        public void DeleteAll() => _context.Database.ExecuteSqlCommand("DELETE FROM [Accounts]");

        [HttpGet]
        public IEnumerable<Device> GetAll() => _context.Devices;

        protected override DbSet<Device> GetAccountsDbSet(IglooSmartHomeContext fromContext)
        {
            return fromContext.Devices;
        }

        protected override void SetAccountInformation(Device newAccount)
        {
            newAccount.Verified = true;
            newAccount.CustomDeviceName = newAccount.Sid;
        }
    }
}