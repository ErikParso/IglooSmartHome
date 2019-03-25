using System;

namespace IglooSmartHome.Services
{
    public interface ILogService
    {
        event EventHandler<string> MessageLogged;

        string GetLog();

        void LogMesage(string message);
    }
}
