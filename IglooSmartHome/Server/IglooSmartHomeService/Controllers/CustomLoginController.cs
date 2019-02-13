using Azure.Server.Utils.CustomAuthentication;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Data.Entity;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class CustomLoginController : CustomLoginController<IglooSmartHomeContext, Account>
    {
        public CustomLoginController(IglooSmartHomeContext context)
            : base(context, "https://igloosmarthome.azurewebsites.net/", TimeSpan.FromSeconds(180))
        {

        }

        protected override DbSet<Account> GetAccountsDbSet(IglooSmartHomeContext fromContext)
        {
            return fromContext.Accounts;
        }
    }
}