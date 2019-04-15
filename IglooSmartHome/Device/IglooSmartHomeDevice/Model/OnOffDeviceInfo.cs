namespace IglooSmartHomeDevice.Model
{
    public class OnOffDeviceInfo
    {
        public string Id { get; set; }

        public int DeviceId { get; set; }

        public string Name { get; set; }

        public OnOffDeviceType Type { get; set; }

        public OnOffDeviceState State { get; set; }
    }
}
