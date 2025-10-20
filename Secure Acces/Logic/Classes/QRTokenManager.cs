using System;
using System.Collections.Concurrent;

namespace Secure_Access.Services
{
    public class QRTokenManager
    {
        // In-memory storage
        private readonly ConcurrentDictionary<string, bool> _tokens = new();

        // Generate and store a new token
        public string GenerateToken()
        {
            var token = Guid.NewGuid().ToString();
            _tokens[token] = false;
            return token;
        }

        // Mark a token as scanned
        public bool MarkAsScanned(string token)
        {
            if (_tokens.ContainsKey(token))
            {
                _tokens[token] = true;
                return true;
            }
            return false;
        }

        // Check if token has been scanned
        public bool IsScanned(string token)
        {
            return _tokens.ContainsKey(token) && _tokens[token];
        }
    }
}
