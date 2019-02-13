using Azure.Server.Utils.CustomAuthentication;
using Azure.Server.Utils.Extensions;
using IglooSmartHome.DataObjects;
using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Web.Http;

namespace IglooSmartHome.Controllers
{
    [MobileAppController]
    public class AccountsController : AccountsController<IglooSmartHomeContext, Account>
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

        protected override DbSet<Account> GetAccountsDbSet(IglooSmartHomeContext fromContext)
        {
            return fromContext.Accounts;
        }

        protected override void SetAccountInformation(Account newAccount)
        {
            switch (newAccount.Provider)
            {
                case "google":
                    newAccount.Name = this.GetCurrentUserClaim("name");
                    newAccount.PhotoUrl = this.GetCurrentUserClaim("picture");
                    newAccount.Email = this.GetCurrentUserClaim(ClaimTypes.Email);
                    break;
                case "facebook":
                    newAccount.Name = this.GetCurrentUserClaim(ClaimTypes.Name);
                    newAccount.PhotoUrl = $"https://graph.facebook.com/v3.2/{newAccount.Sid}/picture?type=large";
                    newAccount.Email = this.GetCurrentUserClaim(ClaimTypes.Email);
                    break;
                case "Federation":
                    newAccount.Name = newAccount.Sid;
                    newAccount.PhotoUrl = $"https://identicon-api.herokuapp.com/{newAccount.Name}/50?format=png";
                    newAccount.Email = newAccount.Name;
                    break;
                default:
                    throw new NotImplementedException($"This account creation method for provider {newAccount.Provider} is not supported.");
            }
        }
    }
}