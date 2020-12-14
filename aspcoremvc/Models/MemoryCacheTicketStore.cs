using System;
using System.Threading.Tasks;
using Common.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Models
{
    public class MemoryCacheTicketStore : ITicketStore
    {
        private const string KeyPrefix = "AuthSessionStore-";
        //private IMemoryCache _cache;
        private readonly CacheBase _cache;

        public MemoryCacheTicketStore(CacheBase parCache)
        {
            _cache = parCache;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var guid = Guid.NewGuid();
            var key = KeyPrefix + guid.ToString();
            await RenewAsync(key, ticket);

            return key;
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            _cache.InsertCachePlain(key, ticket, 60);
            return Task.FromResult(0);
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            AuthenticationTicket ticket = _cache.GetCachePlain<AuthenticationTicket>(key);
            return Task.FromResult(ticket);
        }

        public Task RemoveAsync(string key)
        {
            _cache.RemoveCache(key);
            return Task.FromResult(0);
        }
    }
}