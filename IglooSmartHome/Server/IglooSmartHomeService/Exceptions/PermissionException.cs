using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public class PermissionException : FilteredException
    {
        public PermissionException(string message)
            : base(HttpStatusCode.Forbidden, message)
        {

        }
    }
}