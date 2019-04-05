using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public class ConnectionNotFoundException<T> : FilteredException
    {
        public ConnectionNotFoundException(T connectionMappingKey)
            : base(HttpStatusCode.NotFound, $"Client with key '{connectionMappingKey}' has no active connections.")
        {

        }
    }
}