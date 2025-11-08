using Logic.Classes;
using System;
using System.Collections.Concurrent;

namespace Secure_Access.Services
{
    public class QRTokenManager
    {
        private readonly ConcurrentDictionary<string, QRTokenInfo> _tokens = new();

        public string GenerateToken(string name, string email, int doorId)
        {
            var token = Guid.NewGuid().ToString();
            _tokens[token] = new QRTokenInfo
            {
                Scanned = false,
                Name = name,
                Email = email,
                DoorId = doorId,
            };
            return token;
        }

        public bool MarkAsScanned(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            if (_tokens.TryGetValue(token, out var info))
            {
                info.Scanned = true;
                return true;
            }
            return false;
        }

        public QRTokenInfo? GetInfo(string token)
        {
            return _tokens.TryGetValue(token, out var info) ? info : null;
        }
        public bool IsScanned(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            return _tokens.TryGetValue(token, out var info) && info.Scanned;
        }
    }
}
