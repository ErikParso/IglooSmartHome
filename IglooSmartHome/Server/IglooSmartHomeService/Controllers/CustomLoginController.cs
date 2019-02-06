using Azure.Server.Utils.CustomAuthentication;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class CustomLoginController : CustomLoginController<Account>
    {
        public CustomLoginController(IglooSmartHomeContext context)
            : base(context, "https://igloosmarthome.azurewebsites.net/", TimeSpan.FromDays(60))
        {

        }
    }
}