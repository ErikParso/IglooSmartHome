using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public class DevicePermissionException : FilteredException
    {
        public DevicePermissionException(int deviceId)
            : base(HttpStatusCode.Forbidden, $"You have no permission for device with id '{deviceId}'.")
        {
        }
    }
}