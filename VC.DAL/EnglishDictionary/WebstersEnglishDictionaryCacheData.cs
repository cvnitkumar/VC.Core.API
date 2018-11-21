using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace VC.DAL.EnglishDictionary
{
    public class WebstersEnglishDictionaryCacheData : IEnglishDictionaryData
    {
        readonly string _webstersEnglishDictionaryApiUrl;
        readonly IMemoryCache _iMemoryCache;
        readonly short _cacheExpiryInMins;
        const string _WebstersEnglishDictionaryCacheKey = "WDict";


        public WebstersEnglishDictionaryCacheData(IConfiguration config, IMemoryCache memoryCache)
        {
            _webstersEnglishDictionaryApiUrl = config.GetSection("WebstersEnglishDictionaryApiUrl").Value;
            _iMemoryCache = memoryCache;
            _cacheExpiryInMins = short.Parse(config.GetSection("CacheExpiryInMins").Value);
        }

        /// <summary>
        /// Get the English dictionary using websters API
        /// </summary>
        /// <returns>Dictionary of english words</returns>
        public async Task<Dictionary<string, string>> Get()
        {
            Dictionary<string, string> wDic;
            // Look for cache key.
            if (!_iMemoryCache.TryGetValue(_WebstersEnglishDictionaryCacheKey, out wDic))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("text/plain"));

                    var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                    var streamTask = client.GetStringAsync(_webstersEnglishDictionaryApiUrl);

                    // Deserialize JSON string to dictionary
                    wDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(await streamTask);

                    //set cache
                    _iMemoryCache.Set(_WebstersEnglishDictionaryCacheKey, wDic, new MemoryCacheEntryOptions() {
                        AbsoluteExpirationRelativeToNow = System.TimeSpan.FromMinutes(_cacheExpiryInMins)
                    });
                }
            }

            return wDic;
        }
    }
}
