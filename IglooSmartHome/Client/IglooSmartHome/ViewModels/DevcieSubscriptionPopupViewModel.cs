using IglooSmartHome.Models;
using IglooSmartHome.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class DeviceSubscriptionPopupViewModel : INotifyPropertyChanged
    {

        #region Private fields

        private readonly IDeviceSubscriptionService _deviceSubscriptionService;

        private string _deviceId;
        private string _customDeviceName;

        #endregion


        #region Ctor

        public DeviceSubscriptionPopupViewModel(IDeviceSubscriptionService deviceSubscriptionService)
        {
            _deviceSubscriptionService = deviceSubscriptionService;
        }

        #endregion


        #region Public properties

        public string DeviceId
        {
            get => _deviceId;
            set { _deviceId = value; RaisePropertyChanged(); }
        }

        public string CustomDeviceName
        {
            get => _customDeviceName;
            set { _customDeviceName = value; RaisePropertyChanged(); }
        }

        #endregion


        #region Public methods

        public async Task<DeviceSubscription> SetDeviceOwnerAsync()
            => await _deviceSubscriptionService.SubscribeToDeviceAsync(_deviceId, _customDeviceName);

        #endregion


        #region INotifyPropertyChanged

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
