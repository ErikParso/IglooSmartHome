using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IglooSmartHomeService.SignalR
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public event EventHandler<T> Online;
        public event EventHandler<T> Offline;

        public int Count => _connections.Count;

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                if (_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    connections.Add(connectionId);
                }
                else
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                    connections.Add(connectionId);
                    Online?.Invoke(this, key);
                }
            }
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                if (_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                        Offline?.Invoke(this, key);
                    }
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
            => _connections.TryGetValue(key, out HashSet<string> connections)
               ? connections
               : Enumerable.Empty<string>();

        public bool IsConnected(T key)
            => GetConnections(key).Any();
    }
}