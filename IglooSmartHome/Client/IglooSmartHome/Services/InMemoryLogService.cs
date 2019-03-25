using System;
using System.Linq;
using System.Text;

namespace IglooSmartHome.Services
{
    public class InMemoryLogService : ILogService
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public event EventHandler<string> MessageLogged;

        public string GetLog()
            => _stringBuilder.ToString();

        public void LogMesage(string message)
        {
            var timeMessage = string.Format("{0}: {1}", DateTime.Now, message);
            _stringBuilder.AppendLine(timeMessage);
            MessageLogged?.Invoke(this, timeMessage);
        }
    }
}
