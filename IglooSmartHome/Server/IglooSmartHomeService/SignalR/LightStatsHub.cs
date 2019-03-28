using IglooSmartHomeService.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Diagnostics;
using System.Linq;

namespace IglooSmartHomeService.SignalR
{
    public class LightStatsHub : Hub
    {
        private readonly LightStatsResponseListener _lightStatsResponseListener;
        private readonly DeviceConnectionsMappingService _deviceConnections;

        public LightStatsHub(
            LightStatsResponseListener lightStatsResponseListener,
            DeviceConnectionsMappingService deviceConnections)
        {
            _lightStatsResponseListener = lightStatsResponseListener;
            _deviceConnections = deviceConnections;
        }

        public string GetLightState(int deviceId)
        {
            try
            {
                var connectionId = _deviceConnections.GetConnections(deviceId).First();
                Trace.TraceInformation($"Requesting light state from device using connection id '{connectionId}'");
                _lightStatsResponseListener.TrySendMessageAndWaitForResponse(
                    RequestLightStats, connectionId, "parameter001", out string lightState);
                return lightState;
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                throw;
            }
        }

        private void RequestLightStats(Guid requestId, string connectionId, string parameter)
            => Clients.Client(connectionId).getLightState(requestId, parameter);

        public void sendLightState(Guid requestId, string state)
            => _lightStatsResponseListener.SetResponse(requestId, state);
    }
}