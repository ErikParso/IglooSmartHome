using Azure.Server.Utils.CustomAuthentication;

namespace IglooSmartHome.DataObjects
{
    public class Device : AccountBase
    {
        public string CustomDeviceName { get; set; }
    }
}