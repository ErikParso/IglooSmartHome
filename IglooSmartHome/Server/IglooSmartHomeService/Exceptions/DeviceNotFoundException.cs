using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public class DeviceNotFoundException : FilteredException
    {
        public DeviceNotFoundException(int deviceId)
            :base(HttpStatusCode.NotFound, $"Device with id '{deviceId}' was not found.")
        {

        }
    }
}