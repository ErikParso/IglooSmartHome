using Azure.Server.Utils.CustomAuthentication;
using System.Collections.Generic;

namespace IglooSmartHome.DataObjects
{
    public class Account : AccountBase
    {
        public string PhotoUrl { get; set; }
        public string Name { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}