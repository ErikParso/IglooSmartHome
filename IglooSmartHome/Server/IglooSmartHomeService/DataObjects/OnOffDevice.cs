using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IglooSmartHomeService.DataObjects
{
    public class OnOffDevice
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public string Name { get; set; }

        public OnOffDeviceType Type { get; set; }

        public OnOffDeviceState State { get; set; }

    }
}