using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); // genereic verison
        object Get(string key); // generic olmayan verison
        void Add(string key, object value, int duration);
        bool IsAdd(string key); // cache'de var mı için
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
