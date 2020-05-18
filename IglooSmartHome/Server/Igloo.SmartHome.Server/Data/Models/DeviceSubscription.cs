using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Igloo.SmartHome.Server.Data.Models
{
    public class DeviceSubscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int AccountId { get; set; }

        public User Account { get; set; }

        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }

        public Device Device { get; set; }

        public SubscriptionRole Role { get; set; }
    }
}