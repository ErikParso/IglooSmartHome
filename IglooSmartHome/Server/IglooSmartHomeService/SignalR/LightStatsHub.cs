using IglooSmartHomeService.Services;

namespace IglooSmartHomeService.SignalR
{
    public class LightStatsHub : SignalRResponseListener<ConnectionMapping<int>, int, string, string>
    {
        public LightStatsHub(DeviceConnectionsMappingService connectionMapping)
            : base(connectionMapping)
        {

        }
    }
}