using Refit;
using System.Collections.Generic;
using System.Linq;

namespace IglooSmartHomeDevice.Extesions
{
    public static class ApiExceptionExtensions
    {
        private const string filteredExceptionHeader = "filtered-exception";

        public static string GetFilteredExceptionName(this ApiException apiException)
        {
            if (apiException.Headers.TryGetValues(filteredExceptionHeader, out IEnumerable<string> filteredExceptions))
                return filteredExceptions.FirstOrDefault();
            else
                return null;
        }
    }
}
