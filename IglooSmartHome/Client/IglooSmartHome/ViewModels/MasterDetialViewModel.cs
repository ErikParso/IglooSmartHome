using IglooSmartHome.Services;
using IglooSmartHome.SignalR;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IglooSmartHome.ViewModels
{
    public class MasterDetailViewModel: INotifyPropertyChanged
    {
        #region Private fields

        private readonly SignalRConnectionService _signalRConnectionService;
        private readonly IDeviceSelectionService _deviceSelectionService;
        private readonly IDeviceSubscriptionService _deviceSubscriptionService;

        private bool _isMasterVisible;

        #endregion


        #region Ctor and initialization

        public MasterDetailViewModel(
            MasterViewModel masterViewModel,
            DetailViewViewModel detailViewViewModel,
            SignalRConnectionService signalRConnectionService,
            IDeviceSelectionService deviceSelectionService,
            IDeviceSubscriptionService deviceSubscriptionService)
        {
            MasterViewModel = masterViewModel;
            DetailViewViewModel = detailViewViewModel;

            _signalRConnectionService = signalRConnectionService;
            _deviceSelectionService = deviceSelectionService;
            _deviceSubscriptionService = deviceSubscriptionService;

            AddHandlers();
        }

        public async Task Initialize()
        {
            await _signalRConnectionService.StartConnection();
            await _deviceSubscriptionService.LoadSubscriptionsAsync();
        }

        #endregion


        #region Event handling

        private void AddHandlers()
        {
            _deviceSelectionService.SelectedDeviceChanged += _deviceSelectionService_SelectedDeviceChanged;
        }

        private void _deviceSelectionService_SelectedDeviceChanged(object sender, int? e)
            => IsMasterVisible = false;

        #endregion


        #region Public properties

        public MasterViewModel MasterViewModel { get; private set; }
        public DetailViewViewModel DetailViewViewModel { get; private set; }

        public bool IsMasterVisible
        {
            get => _isMasterVisible;
            set { _isMasterVisible = value; RaisePropertyChanged(); }
        }

        #endregion


        #region INotifyPropertyChanged

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
