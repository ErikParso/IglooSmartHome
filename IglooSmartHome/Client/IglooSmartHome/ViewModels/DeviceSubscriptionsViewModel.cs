using IglooSmartHome.IglooEventArgs;
using IglooSmartHome.Models;
using IglooSmartHome.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace IglooSmartHome.ViewModels
{
    public class DeviceSubscriptionsViewModel : INotifyPropertyChanged
    {
        #region Private fields

        private readonly IDeviceSubscriptionService _devicesSubscriptionService;
        private readonly IDeviceOnlineStatusService _deviceOnlineStatusService;
        private readonly IDeviceSelectionService _deviceSelectionService;

        private IEnumerable<DeviceSubscription> _subscriptions;

        #endregion


        #region Ctor

        public DeviceSubscriptionsViewModel(
            IDeviceSubscriptionService devicesSubscriptionService,
            IDeviceOnlineStatusService deviceOnlineStatusService,
            IDeviceSelectionService deviceSelectionService)
        {
            _devicesSubscriptionService = devicesSubscriptionService;
            _deviceOnlineStatusService = deviceOnlineStatusService;
            _deviceSelectionService = deviceSelectionService;
            _subscriptions = Enumerable.Empty<DeviceSubscription>();

            SubscriptionSelectedCommand = new Command<DeviceSubscription>(SubscriptionSelected);

            AddHandlers();
        }

        #endregion


        #region Event handling

        private void AddHandlers()
        {
            _devicesSubscriptionService.DeviceSubscriptionsLoaded += _devicesSubscriptionService_DeviceSubscriptionsLoaded;
            _devicesSubscriptionService.NewDeviceSubscribed += _devicesSubscriptionService_NewDeviceSubscribed;
            _deviceOnlineStatusService.DeviceOnlineStatusChanged += _deviceOnlineStatusService_DeviceOnlineStatusChanged;
        }

        private async void _devicesSubscriptionService_DeviceSubscriptionsLoaded(
            object sender,
            IEnumerable<DeviceSubscription> subscriptions)
        {
            Subscriptions = subscriptions;
            foreach (var subscription in Subscriptions)
            {
                subscription.IsOnline =
                    await _deviceOnlineStatusService.IsDeviceOnline(subscription.DeviceId);
            }
            _deviceSelectionService.SetSelectedDeviceId(Subscriptions.FirstOrDefault()?.DeviceId);
        }

        private void _devicesSubscriptionService_NewDeviceSubscribed(object sender, DeviceSubscription e)
        {
            Subscriptions = Subscriptions.Concat(new[] { e });
        }

        private void _deviceOnlineStatusService_DeviceOnlineStatusChanged(object sender, DeviceOnlineStatusChangedEventArgs e)
        {
            foreach (var subs in Subscriptions.Where(s => s.DeviceId == e.DeviceId))
            {
                subs.IsOnline = e.OnlineStatus;
            }
        }

        #endregion


        #region Public properties

        public IEnumerable<DeviceSubscription> Subscriptions
        {
            get => _subscriptions;
            set { _subscriptions = value; RaisePropertyChanged(); }
        }

        #endregion


        #region Commands

        public ICommand SubscriptionSelectedCommand { get; private set; }

        private void SubscriptionSelected(DeviceSubscription subscription)
            => _deviceSelectionService.SetSelectedDeviceId(subscription.DeviceId);

        #endregion


        #region INotifyPropertyChanged

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}