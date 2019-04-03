using System;
using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public abstract class FilteredException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public FilteredException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}