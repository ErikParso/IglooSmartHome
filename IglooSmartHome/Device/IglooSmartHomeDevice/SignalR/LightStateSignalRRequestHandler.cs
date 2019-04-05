namespace IglooSmartHomeDevice.SignalR
{
    public class LightStateSignalRRequestHandler : SignalRRequestHandler<string, string>
    {
        protected override string HubProxyName => "LightStatsHub";

        protected override string ProcessRequestAndCreateresponse(string request)
            => $"Light '{request}' state is on!";
    }
}
