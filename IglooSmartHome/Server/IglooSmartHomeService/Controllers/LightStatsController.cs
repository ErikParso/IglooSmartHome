using System.Web.Http;
using IglooSmartHomeService.SignalR;
using Microsoft.Azure.Mobile.Server.Config;

namespace IglooSmartHomeService.Controllers
{
    [MobileAppController]
    public class LightStatsController : ApiController
    {
        private readonly LightStatsHub _lightstatsHub;

        public LightStatsController(LightStatsHub lightStatsHub)
        {
            _lightstatsHub = lightStatsHub;
        }

        // GET api/LightStats
        public string Get(int deviceId)
        {
            return _lightstatsHub.SendMessageAndWaitForResponse(deviceId, "param0002");
        }
    }
}
