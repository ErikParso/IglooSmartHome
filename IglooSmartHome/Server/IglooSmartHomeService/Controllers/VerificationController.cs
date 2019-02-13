using System.Data.Entity;
using Azure.Server.Utils.CustomAuthentication;
using Azure.Server.Utils.Email;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class VerificationController : VerificationController<IglooSmartHomeContext ,Account>
    {
        public VerificationController(IglooSmartHomeContext context, IEmailService<Account> emailService) : base(context, emailService)
        {
        }

        protected override DbSet<Account> GetAccountsDbSet(IglooSmartHomeContext fromContext)
        {
            return fromContext.Accounts;
        }
    }
}