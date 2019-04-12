using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public class OnOffDeviceAlreadyRegisteredException : FilteredException
    {
        public OnOffDeviceAlreadyRegisteredException(string onOffDeviceId)
            : base(HttpStatusCode.Conflict, $"OnOff device with id '{onOffDeviceId}' is already registered.")
        {
        }
    }
}