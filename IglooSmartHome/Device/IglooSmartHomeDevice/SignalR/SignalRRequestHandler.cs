using IglooSmartHomeDevice.Services;
using Microsoft.AspNet.SignalR.Client;
using System;

namespace IglooSmartHomeDevice.SignalR
{
    public abstract class SignalRRequestHandler<Q,S>
    {
        private IHubProxy _hubProxy;

        public void InitializeHubProxy(HubConnection hubConnection)
        {
            if (_hubProxy != null)
                throw new Exception($"HubProxy '{HubProxyName}' was already initialized.");
            _hubProxy = hubConnection.CreateHubProxy(HubProxyName);
            _hubProxy.On<Guid, Q>("getResponse", (guid, parameter) =>
            {
                _hubProxy.Invoke("setResponse", guid, ProcessRequestAndCreateresponse(parameter));
            });
        }

        protected abstract string HubProxyName { get; }

        protected abstract S ProcessRequestAndCreateresponse(Q request);
    }
}
