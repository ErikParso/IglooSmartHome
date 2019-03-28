using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IglooSmartHomeService.SignalR
{
    public abstract class SignalRResponseListener<T,U>
    {
        private Dictionary<Guid, TaskCompletionSource<T>> _taskCompletionSources
            = new Dictionary<Guid, TaskCompletionSource<T>>();

        public bool TrySendMessageAndWaitForResponse(
            Action<Guid, string, U> messageMethod,
            string connectionId,
            U parameter,
            out T result)
        {
            var ret = false;
            var requestId = Guid.NewGuid();
            var tcs = new TaskCompletionSource<T>();
            _taskCompletionSources[requestId] = tcs;

            messageMethod(requestId, connectionId, parameter);

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

        public void SetResponse(Guid requestId, T response)
        {
            if (_taskCompletionSources.TryGetValue(requestId, out TaskCompletionSource<T> tcs))
            {
                tcs.TrySetResult(response);
            }
        }


    }
}