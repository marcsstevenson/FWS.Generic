using System;
using System.Threading.Tasks;
using FWS.Generic.Framework.Models;

namespace FWS.Generic.Framework.Interfaces
{
    public interface ICacheProvider
    {
         CacheStringResponse  StringGet(string cacheKey);

         CacheStringResponse  StringSet(string cacheKey, string value);

         CacheStringResponse  KeyDelete(string cacheKey);

         CacheStringResponse  TestConnection();

         CacheObjectResponse  ObjectGet(string cacheKey);

         CacheObjectResponse  ObjectSet(string cacheKey, object value);

         string GetAddress();
         void ClearCache();
    }
}