using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IglooSmartHomeService.SignalR
{
    public abstract class SignalRResponseListener<M,K,T,U> : Hub
        where M : ConnectionMapping<K>
    {
        private readonly Dictionary<Guid, TaskCompletionSource<T>> _taskCompletionSources;
        private readonly ConnectionMapping<K> _connectionMapping;

        public SignalRResponseListener(ConnectionMapping<K> connectionMapping)
        {
            _connectionMapping = connectionMapping;
            _taskCompletionSources = new Dictionary<Guid, TaskCompletionSource<T>>();
        }

        public bool TrySendMessageAndWaitForResponse(
            K connectionMappingKey,
            U parameter,
            out T result)
        {
            var ret = false;
            var requestId = Guid.NewGuid();
            var tcs = new TaskCompletionSource<T>();
            _taskCompletionSources[requestId] = tcs;

            Clients.Client(_connectionMapping.GetConnections(connectionMappingKey).First())
                .getResponse(requestId, parameter);

            Task.Delay(5000)
                .ContinueWith(task => tcs.TrySetCanceled());

            try
            {
                tcs.Task.Wait();
                result = tcs.Task.Result;
                ret = true;
            }
            catch (Exception)
            {
                result = default(T);
                ret = false;
            }

            _taskCompletionSources.Remove(requestId);
            return ret;
        }

        public void setResponse(Guid requestId, T response)
        {
            if (_taskCompletionSources.TryGetValue(requestId, out TaskCompletionSource<T> tcs))
            {
                tcs.TrySetResult(response);
            }
        }
    }
}