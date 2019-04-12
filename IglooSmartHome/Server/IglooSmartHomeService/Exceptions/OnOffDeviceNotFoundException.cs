using System.Net;

namespace IglooSmartHomeService.Exceptions
{
    public class OnOffDeviceNotFoundException : FilteredException
    {
        public OnOffDeviceNotFoundException(string onOffDeviceId)
            : base(HttpStatusCode.NotFound, $"OnOff device with id '{onOffDeviceId}' was not registered yet.")
        {

        }
    }
}