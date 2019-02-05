using Azure.Server.Utils.CustomAuthentication;
using Azure.Server.Utils.Extensions;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class AccountsController : AccountsController<Account>
    {
        private readonly IglooSmartHomeContext _context;

        public AccountsController(IglooSmartHomeContext context)
            : base(context)
        {
            _context = context;
        }

        [HttpDelete]
        public void DeleteAll() => _context.Database.ExecuteSqlCommand("DELETE FROM [Accounts]");

        [HttpGet]
        public IEnumerable<Account> GetAll() => _context.Accounts;

        protected override void CreateNewAccount(Account newAccount)
        {
            switch (newAccount.Provider)
            {
                case Provider.Google:
                    newAccount.Name = this.GetCurrentUserClaim("name");
                    newAccount.Provider = Provider.Google;
                    newAccount.PhotoUrl = this.GetCurrentUserClaim("picture");
                    break;
                case Provider.Facebook:
                    newAccount.Name = this.GetCurrentUserClaim(ClaimTypes.Name);
                    newAccount.Provider = Provider.Facebook;
                    newAccount.PhotoUrl = $"https://graph.facebook.com/v3.2/{newAccount.Sid}/picture?type=large";
                    break;
                default:
                    throw new NotImplementedException($"Provider {User.Identity.AuthenticationType} is not supported.");
            }
        }
    }
}