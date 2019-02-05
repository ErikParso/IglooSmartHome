using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace IglooSmartHome.Controllers
{
    /// <summary>
    /// ValuesController.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [MobileAppController]
    [Authorize]
    public class ValuesController : ApiController
    {
        private readonly IglooSmartHomeContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="balanseContext">The balanse context.</param>
        public ValuesController(IglooSmartHomeContext balanseContext)
        {
            _context = balanseContext;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>value</returns>
        [HttpGet]
        public string Get()
        {
            return GetUserId(User) + " via Get";
        }

        /// <summary>
        /// Posts this instance.
        /// </summary>
        /// <returns>value</returns>
        [HttpPost]
        public string Post()
        {
            return GetUserId(User) + " via Post";
        }

        private string GetUserId(IPrincipal user)
        {
            ClaimsPrincipal claimsUser = (ClaimsPrincipal)user;
            string sid = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return sid;
        }
    }
}
