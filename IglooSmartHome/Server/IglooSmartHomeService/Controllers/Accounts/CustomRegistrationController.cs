using Azure.Server.Utils.CustomAuthentication;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System.Data.Entity;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class CustomRegistrationController : CustomRegistrationController<IglooSmartHomeContext, Account>
    {
        public CustomRegistrationController(IglooSmartHomeContext context) : base(context)
        {

        }

        protected override DbSet<Account> GetAccountsDbSet(IglooSmartHomeContext fromContext)
        {
            return fromContext.Accounts;
        }
    }
}
