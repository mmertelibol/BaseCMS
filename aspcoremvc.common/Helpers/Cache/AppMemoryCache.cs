using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Common.Dto;

namespace Common.Helpers
{
    public class AppMemoryCache : CacheBase
    {
        private readonly IMemoryCache _cache;

        public AppMemoryCache(IMemoryCache parcache)
        {
            _cache = parcache;
        }

        public override void InsertCache(string key, object value)
        {
            const int fiveMinute = 5;
            _cache.Set(key, value.JsonEncode(), TimeSpan.FromMinutes(fiveMinute));
        }

        public override void InsertCache(string key, object value, int minutes)
        {
            _cache.Set(key, value.JsonEncode(), TimeSpan.FromMinutes(minutes));
        }

        public override void InsertCachePlain(string key, object value, int minutes)
        {
            _cache.Set(key, value, TimeSpan.FromMinutes(minutes));
        }

        public override T GetCachePlain<T>(string key)
        {
            var res = _cache.Get(key);

            if (res == null)
                return default(T);

            return (T)res;
        }

        public override T GetCache<T>(string key)
        {
            var res = _cache.Get(key);

            if (res == null)
                return default(T);

            return (res as string).JsonDecode<T>();
        }

        public override bool RemoveCache(string key)
        {
            _cache.Remove(key);
            return true;
        }

        public override List<LookupDto> GetAllKeys()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(_cache) as ICollection;
            var items = new List<LookupDto>();

            if (collection != null)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);

                    if (val.ToString().IndexOf("Microsoft") == -1)
                        items.Add(new LookupDto { Text = val.ToString() });
                }

            return items;
        }

        //public override void Clear()
        //{
        //    var keys = _cache;
        //    if (keys != null)
        //    {
        //        foreach (DictionaryEntry item in keys)
        //        {
        //            _cache.Remove(item.Key.ToString());
        //        }
        //    }
        //}
    }
}