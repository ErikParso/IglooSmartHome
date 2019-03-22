using IglooSmartHome.Services;
using IglooSmartHome.SignalR;
using System.Threading.Tasks;
using Xamarin.Forms.Utils.Services;

namespace IglooSmartHome.ViewModels
{
    public class MasterViewModel
    {
        #region Private fields

        private readonly SignalRConnectionService _signalRConnectionService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IDeviceOnlineStatusService _deviceOnlineStatusService;

        #endregion


        #region Ctor

        public MasterViewModel(
            DeviceSubscriptionsViewModel deviceSubscriptionsViewModel,
            SignalRConnectionService signalRConnectionService,
            IAuthenticationService authenticationService,
            IDeviceOnlineStatusService deviceOnlineStatusService)
        {
            DeviceSubscriptionsViewModel = deviceSubscriptionsViewModel;
            _signalRConnectionService = signalRConnectionService;
            _authenticationService = authenticationService;
            this._deviceOnlineStatusService = deviceOnlineStatusService;
        }

        #endregion


        #region Public properties

        public DeviceSubscriptionsViewModel DeviceSubscriptionsViewModel { get; private set; }

        #endregion


        #region Public methods

        public async Task LogoutAndStopConnection()
        {
            _signalRConnectionService.StopConnection();
            _deviceOnlineStatusService.ClearCache();
            await _authenticationService.Logout();
        }

        #endregion
    }
}
