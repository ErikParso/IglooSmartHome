using IglooSmartHome.DataObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IglooSmartHomeService.DataObjects
{
    public class DeviceSubscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public SubscriptionRole Role { get; set; }
    }
}