using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public class ClientNotRespondingException<K> : FilteredException
    {
        public ClientNotRespondingException(K connectionMappingKey, int timeout)
            : base(HttpStatusCode.RequestTimeout, $"Client with key '{connectionMappingKey}' did not respon in timeout {timeout}ms.")
        {

        }
    }
}