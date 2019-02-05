using Azure.Server.Utils.Communication.Authentication;
using Azure.Server.Utils.CustomAuthentication;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class CustomRegistrationController : CustomRegistrationController<Account>
    {
        public CustomRegistrationController(IglooSmartHomeContext context) : base(context)
        {

        }

        protected override void CreateUserProfile(Account account, RegistrationRequest registrationRequest)
        {
            account.Name = registrationRequest.Email;
            account.PhotoUrl = $"https://identicon-api.herokuapp.com/{registrationRequest.Email}/50?format=png";
        }
    }
}
