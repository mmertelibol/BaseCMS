using System;
using System.Collections.Generic;
using StackExchange.Redis;
using Common.Dto;

namespace Common.Helpers
{
    public class RedisCacheManager : CacheBase
    {
        private static IDatabase _db;
        private static readonly string Host = HttpHelper.GetConfig<string>("AppConfig:RedisHost");
        private static readonly int Port = HttpHelper.GetConfig<int>("AppConfig:RedisPort"); //6379;

        public RedisCacheManager()
        {
            CreateRedisDb();
        }

        private static IDatabase CreateRedisDb()
        {
            if (null == _db)
            {
                var option = new ConfigurationOptions();

                option.Ssl = false;
                //option.Password = HttpHelper.GetConfig<string>("AppConfig:RedisPassword");
                option.EndPoints.Add(Host, Port);
                option.ConnectTimeout = 10 * 1000;
                option.SyncTimeout = 10 * 1000;
                option.ResponseTimeout = 10 * 1000;

                var connect = ConnectionMultiplexer.Connect(option);
                _db = connect.GetDatabase();
            }

            return _db;
        }

        //public override object GetCache(string key)
        //{
        //    var rValue = _db.StringGet(key);
        //
        //    if (!rValue.HasValue)
        //        return null;
        //
        //    var result = rValue.ToString().JsonDecode();
        //    return result;
        //}

        public override T GetCache<T>(string key)
        {
            var rValue = _db.StringGet(key);

            if (!rValue.HasValue)
                return default(T);

            var result = rValue.ToString().JsonDecode<T>();
            return result;
        }

        public override T GetCachePlain<T>(string key)
        {
            var rValue = _db.StringGet(key);

            if (!rValue.HasValue)
                return default(T);

            var result = (T)(rValue as object);
            return result;
        }

        public override void InsertCache(string key, object value)
        {
            const int fiveMinute = 5;
            InsertCache(key, value, fiveMinute);
        }

        public override void InsertCache(string key, object value, int minutes)
        {
            if (value == null)
                return;

            var entryBytes = value.JsonEncode();

            _db.StringSet(key, entryBytes);

            var expiresIn = TimeSpan.FromMinutes(minutes);

            if (minutes > 0)
                _db.KeyExpire(key, expiresIn);
        }

        public override void InsertCachePlain(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var entryBytes = (string)data;

            _db.StringSet(key, entryBytes);

            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            if (cacheTime > 0)
                _db.KeyExpire(key, expiresIn);
        }

        public override bool RemoveCache(string key)
        {
            return _db.KeyDelete(key);
        }

        //public override bool ClearCacheWithoutParams(string key)
        //{
        //    var server = _db.Multiplexer.GetServer(Host, Port);
        //
        //    foreach (var item in server.Keys(pattern:  key + "*"))
        //        _db.KeyDelete(item);
        //
        //    return true;
        //}

        public void RemoveByPattern(string pattern)
        {
            var server = _db.Multiplexer.GetServer(Host, Port);

            foreach (var item in server.Keys(pattern: "*" + pattern + "*"))
                _db.KeyDelete(item);
        }

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public override List<LookupDto> GetAllKeys()
        {
            return new List<LookupDto>();
        }

        //public override List<CacheObjectModel> GetCacheAll()
        //{
        //    var result = new List<CacheObjectModel>();
        //    var server = _db.Multiplexer.GetServer(Host, Port);
        //    var keys = server.Keys();
        //
        //    if (keys != null)
        //    {
        //        foreach (var item in keys)
        //        {
        //            result.Add(new CacheObjectModel { Key = item.ToString() });
        //        }
        //    }
        //
        //    return result;
        //}

        //public override void Clear()
        //{
        //    var server = _db.Multiplexer.GetServer(Host, Port);
        //
        //    foreach (var item in server.Keys())
        //        _db.KeyDelete(item);
        //}
    }
}