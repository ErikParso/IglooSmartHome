using IglooSmartHome.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IglooSmartHome.ViewModels
{
    public class LogPopupViewModel : INotifyPropertyChanged
    {

        #region Private fields

        private readonly ILogService _logService;

        private string _log;

        #endregion


        #region Ctor

        public LogPopupViewModel(ILogService logService)
        {
            _logService = logService;
            _log = logService.GetLog();
            AddHandlers();
        }

        #endregion


        #region EventHandling

        private void AddHandlers()
        {
            _logService.MessageLogged += _logService_MessageLogged;
        }

        private void _logService_MessageLogged(object sender, string e)
        {
            Log += e + Environment.NewLine;
        }

        #endregion


        #region Public properties

        public string Log
        {
            get => _log;
            set { _log = value; RaisePropertyChanged(); }
        }

        #endregion


        #region INotifyPropertyChanged

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
