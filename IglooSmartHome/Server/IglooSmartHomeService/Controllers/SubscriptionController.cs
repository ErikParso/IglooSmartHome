using IglooSmartHome.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System.Web.Http;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class SubscriptionController : ApiController
    {
        private readonly IglooSmartHomeContext _context;

        public SubscriptionController(IglooSmartHomeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_context.DeviceSubscriptions);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Set(string deviceId)
        {
            return Ok();
        }
    }
}
