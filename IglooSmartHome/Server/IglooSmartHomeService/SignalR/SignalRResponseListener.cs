using IglooSmartHomeService.Exceptions;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IglooSmartHomeService.SignalR
{
    public abstract class SignalRResponseListener<M, K, T, U> : Hub
        where M : ConnectionMapping<K>
    {
        private readonly Dictionary<Guid, TaskCompletionSource<T>> _taskCompletionSources;
        private readonly ConnectionMapping<K> _connectionMapping;

        public SignalRResponseListener(ConnectionMapping<K> connectionMapping)
        {
            _connectionMapping = connectionMapping;
            _taskCompletionSources = new Dictionary<Guid, TaskCompletionSource<T>>();
        }

        protected virtual int Timeout { get; } = 30_000;

        public T SendMessageAndWaitForResponse(
            K connectionMappingKey,
            U parameter)
        {
            var requestId = Guid.NewGuid();
            var tcs = new TaskCompletionSource<T>();
            _taskCompletionSources[requestId] = tcs;

            var connection = _connectionMapping.GetConnections(connectionMappingKey)
                .FirstOrDefault();
            if (string.IsNullOrEmpty(connection))
                throw new ConnectionNotFoundException<K>(connectionMappingKey);

            Clients.Client(connection)
                .getResponse(requestId, parameter);

            Task.Delay(Timeout)
                .ContinueWith(task => tcs.TrySetCanceled());

            tcs.Task.Wait();

            _taskCompletionSources.Remove(requestId);
            return tcs.Task.Result;
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