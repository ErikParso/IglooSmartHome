using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IglooSmartHome.Models
{
    public class DeviceSubscription : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public string CustomDeviceName { get; set; }

        public DeviceSubscriptionRole Role { get; set; }

        private bool _isOnline;

        [JsonIgnore]
        public bool IsOnline
        {
            get => _isOnline;
            set { _isOnline = value; RaisePropertyChanged(); }
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
