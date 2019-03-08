namespace IglooSmartHome.Models
{
    public class DeviceSubscription
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public string CustomDeviceName { get; set; }

        public DeviceSubscriptionRole Role { get; set; }
    }
}
