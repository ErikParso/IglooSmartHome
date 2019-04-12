using Azure.Server.Utils.CustomAuthentication;

namespace IglooSmartHomeService.DataObjects
{
    public class Device : AccountBase
    {
        public string CustomDeviceName { get; set; }
    }
}