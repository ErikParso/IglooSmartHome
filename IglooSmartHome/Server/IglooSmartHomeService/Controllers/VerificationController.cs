using Azure.Server.Utils.CustomAuthentication;
using Azure.Server.Utils.Email;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class VerificationController : VerificationController<Account>
    {
        public VerificationController(IglooSmartHomeContext context, IEmailService emailService) : base(context, emailService)
        {
        }
    }
}