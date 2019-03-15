using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace IglooSmartHomeDevice.Services
{
    public class DeviceConnectionService
    {
        private HubConnection _hubConnection;
        private IHubProxy _deviceConnectionHubProxy;
        private readonly AuthenticationService _authenticationService;      

        public event EventHandler<string> OnLog;

        public DeviceConnectionService(
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            NetworkInterface.GetIsNetworkAvailable();
        }

        public void Connect()
        {
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            if (_hubConnection != null)
            {
                // Clean up previous connection
                _hubConnection.Closed -= OnDisconnected;
                _hubConnection.Error -= OnError;
                _hubConnection.StateChanged -= OnStateChenged;
                _hubConnection.Reconnecting -= Reconnecting;
                _hubConnection.Reconnected -= Reconnected;
                _hubConnection.ConnectionSlow -= ConnectionSlow;
                _hubConnection.Received -= Received;
            }

            _hubConnection = new HubConnection(Environment.ServerAddress, new Dictionary<string, string> { { "deviceName", "rpi" } });

            _hubConnection.Closed += OnDisconnected;
            _hubConnection.Error += OnError;
            _hubConnection.StateChanged += OnStateChenged;
            _hubConnection.Reconnecting += Reconnecting;
            _hubConnection.Reconnected += Reconnected;
            _hubConnection.ConnectionSlow += ConnectionSlow;
            _hubConnection.Received += Received;

            _deviceConnectionHubProxy = _hubConnection.CreateHubProxy("DeviceConnectionHub");
            OnLog(this, DateTime.Now + ": Initialized new HubConnection.");

            ConnectWithRetry();
        }

        private void Received(string obj)
        {
            OnLog(this, DateTime.Now + $": Received " + obj);
        }

        private void ConnectionSlow()
        {
            OnLog(this, DateTime.Now + $": Connection slow...");
        }

        private void Reconnected()
        {
            OnLog(this, DateTime.Now + $": Reconnected.");
        }

        private void Reconnecting()
        {
            OnLog(this, DateTime.Now + $": Reconnecting...");
        }

        private void OnStateChenged(StateChange obj)
        {
            OnLog(this, DateTime.Now + $": Connection state changed. ({obj.OldState} => {obj.NewState})");
        }

        internal void StopConnection()
        {
            _hubConnection.Stop();
        }

        private void OnError(Exception obj)
        {
            OnLog(this, DateTime.Now + $": Connection error.");
        }

        void OnDisconnected()
        {
            OnLog(this, DateTime.Now + ": Connection closed.");
        }

        private void ConnectWithRetry()
        {
            var attemp = 1;
            var t = _hubConnection.Start();
            t.ContinueWith(task =>
            {
                if (_hubConnection.State == ConnectionState.Connected)
                {
                    OnLog(this, DateTime.Now + ": Connected.");
                }
                else
                {
                    OnLog(this, DateTime.Now + $": Connection attemp {attemp++} failed.");
                    throw new Exception();
                }
            }).Wait();
        }
    }
}
