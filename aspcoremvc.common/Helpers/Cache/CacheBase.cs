using System;
using System.Collections.Generic;
using Common.Dto;

namespace Common.Helpers
{
    public static class CacheKeys
    {
        public class Genel
        {
            public const string Name = "Genel";
            public static readonly Tuple<string, string> AllMenu = new Tuple<string, string>(Name, "AllMenu");
            public static readonly Tuple<string, string> Constants = new Tuple<string, string>(Name, "Constants");
        }
    }

    public abstract class CacheBase : ICache
    {
        public const int DefaultCacheMinutes = 60;

        public T CacheFetch<T>(Tuple<string, string> keyCombo, Func<T> fetcher, params object[] cacheParams) 
        {
            return CacheFetchWithTime(keyCombo, DefaultCacheMinutes, fetcher, cacheParams);
        }

        public T CacheFetchWithTime<T>(Tuple<string, string> keyCombo, int minutes, Func<T> fetcher, params object[] cacheParams) 
        {
            var keyGen = GetKeyString(keyCombo, cacheParams);
            var cacheValue = GetCache<T>(keyGen);

            if (cacheValue != null)
                return cacheValue;

            var data = fetcher();

            if (data != null)
                InsertCache(keyGen, data, minutes);

            return GetCache<T>(keyGen);
        }

        public static string GetKeyString(Tuple<string, string> keyCombo, params object[] cacheParams)
        {
            var keyGen = $"{keyCombo.Item1}.{keyCombo.Item2}";

            if (cacheParams.Length > 0)
                foreach (var cacheParam in cacheParams)
                    keyGen += "~" + cacheParam.JsonEncode();

            return keyGen;
        }

        //public bool ClearCache(Tuple<string, string> keyCombo, params object[] cacheParams)
        //{
        //    return RemoveCache(GetKeyString(keyCombo, cacheParams));
        //}

        //public bool ClearCacheWithoutParams(Tuple<string, string> keyCombo)
        //{
        //    string keyGen = GetKeyString(keyCombo);
        //    return ClearCacheWithoutParams(keyGen);
        //}

        public abstract void InsertCache(string key, object value);

        public abstract void InsertCache(string key, object value, int minutes);

        public abstract void InsertCachePlain(string key, object value, int minutes);

        public abstract T GetCache<T>(string key);

        public abstract T GetCachePlain<T>(string key);

        public abstract bool RemoveCache(string key);

        public abstract List<LookupDto> GetAllKeys();

        //public abstract object GetCache(string key);

        //public abstract bool ClearCacheWithoutParams(string key);

        //public abstract List<CacheObjectModel> GetCacheAll();

        //public abstract void Clear();
    }

    public interface ICache
    {
        /// <summary>
        /// Expire süresi 5 dakikadır.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void InsertCache(string key, object value);

        /// <summary>
        /// Expire süresine göre cache'e ekleme işlemi yapar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="setExperies"></param>
        /// <param name="value"></param>
        void InsertCache(string key, object value, int minutes);

        /// <summary>
        /// Cache'deki key'e ait değeri getirir
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCache<T>(string key);

        bool RemoveCache(string key);

        List<LookupDto> GetAllKeys();

        //object GetCache(string key);

        /// <summary>
        /// Cache'i parametreleri dikkate almadan sadece keyCombo datasını kapsayan cache kayıtları arasından bulup silmeye yarar
        /// </summary>
        /// <param name="key"></param>
        //bool ClearCacheWithoutParams(string key);

        /// <summary>
        /// Cache'deki key'leri getirir
        /// </summary>
        /// <returns></returns>
        //List<CacheObjectModel> GetCacheAll();

        /// <summary>
        /// Tüm cache'i temizler
        /// </summary>
        //void Clear();
    }

    //public class CacheObjectModel
    //{
    //    public string Key { get; set; }
    //    public object Value { get; set; }
    //}
}