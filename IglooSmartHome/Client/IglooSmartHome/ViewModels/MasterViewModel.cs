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

        #endregion


        #region Ctor

        public MasterViewModel(
            DeviceSubscriptionsViewModel deviceSubscriptionsViewModel,
            SignalRConnectionService signalRConnectionService,
            IAuthenticationService authenticationService)
        {
            DeviceSubscriptionsViewModel = deviceSubscriptionsViewModel;
            _signalRConnectionService = signalRConnectionService;
            _authenticationService = authenticationService;
        }

        #endregion


        #region Public properties

        public DeviceSubscriptionsViewModel DeviceSubscriptionsViewModel { get; private set; }

        #endregion


        #region Public methods

        public async Task LogoutAndStopConnection()
        {
            _signalRConnectionService.StopConnection();
            await _authenticationService.Logout();
        }

        #endregion
    }
}
