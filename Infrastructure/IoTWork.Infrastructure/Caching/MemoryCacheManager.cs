using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Caching
{
    public class MemoryCacheManager : ICacheManager, ICacheDirectory
    {
        private MemoryCache _cache;
        private List<String> _keys;
        private object _lock;
        private TimeSpan? _defaultTimeout;

        public MemoryCacheManager()
        {
            _cache = null;
            _lock = new object();
            _defaultTimeout = null;
            _keys = new List<string>();
        }

        public void Delete(string Key)
        {
            lock (_lock)
            {
                if (_cache != null)
                {
                    _cache.Remove(Key);
                    RemoveKey(Key);
                }
            }
        }

        public object Get(string Key)
        {
            object data = null;

            lock (_lock)
            {
                if (_cache != null && _cache.Contains(Key))
                    data = _cache.Get(Key);
                else
                    data = null;
            }
            return data;
        }

        public object GetAndDelete(string Key)
        {
            object data;

            lock (_lock)
            {
                if (_cache != null && _cache.Contains(Key))
                {
                    data = _cache.Get(Key);
                    _cache.Remove(Key);
                    RemoveKey(Key);
                }
                else
                    data = null;
            }
            return data;
        }

        public void Set(string Key, object obj)
        {
            lock (_lock)
            {
                if (_cache != null)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    if (_defaultTimeout.HasValue)
                        policy.AbsoluteExpiration = DateTime.UtcNow.Add(_defaultTimeout.Value);
                    _cache.Set(Key, obj, policy);
                    AddKey(Key);
                }
            }
        }

        public void Set(string Key, object obj, TimeSpan Timeout)
        {
            lock (_lock)
            {
                if (_cache != null)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTime.UtcNow.Add(Timeout);
                    _cache.Set(Key, obj, policy);
                    AddKey(Key);
                }
            }
        }

        public void Connect(string Name, TimeSpan? DefaultTimeout = null)
        {
            _cache = new MemoryCache(Name);
            _defaultTimeout = DefaultTimeout;
        }

        #region ICacheDictionary
        public string[] KeysByPrefix(string Prefix)
        {
            string[] keys = new string[0];

            lock (_lock)
            {
                keys = _keys.Where(x => x.StartsWith(Prefix)).ToArray();
            }

            return keys;
        }

        public string[] KeysBySuffix(string Suffix)
        {
            string[] keys = new string[0];

            lock (_lock)
            {
                keys = _keys.Where(x => x.EndsWith(Suffix)).ToArray();
            }

            return keys;
        }

        public string[] KeysByValue(string Value)
        {
            string[] keys = new string[0];

            lock (_lock)
            {
                keys = _keys.Where(x => x.IndexOf(Value) > 0).ToArray();
            }

            return keys;
        }
        #endregion

        private void RemoveKey(string Key)
        {
            var value = _cache.Get(Key);
            if (value == null)
            {
                _keys.Remove(Key);
            }
            else
            {
                if (String.IsNullOrEmpty(_keys.Where(x => x == Key).FirstOrDefault()))
                {
                    _keys.Add(Key);
                }
            }
        }

        private void AddKey(string Key)
        {
            var value = _cache.Get(Key);
            if (value != null)
            {
                if (String.IsNullOrEmpty(_keys.Where(x => x == Key).FirstOrDefault()))
                {
                    _keys.Add(Key);
                }
            }
        }

    }
}
