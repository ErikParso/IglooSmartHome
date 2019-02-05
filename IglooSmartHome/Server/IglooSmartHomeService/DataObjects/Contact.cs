using Microsoft.Azure.Mobile.Server;

namespace IglooSmartHome.DataObjects
{
    public class Contact : EntityData
    {
        public string ContactName { get; set; }

        public Account Account { get; set; }
    }
}