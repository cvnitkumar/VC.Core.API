using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace VC.DAL.EnglishDictionary
{
    public class WebstersEnglishDictionaryData : IEnglishDictionaryData
    {
        readonly string _webstersEnglishDictionaryApiUrl;

        public WebstersEnglishDictionaryData(IConfiguration config)
        {            
            _webstersEnglishDictionaryApiUrl = config.GetSection("WebstersEnglishDictionaryApiUrl").Value;
        }

        /// <summary>
        /// Get the English dictionary using websters API
        /// </summary>
        /// <returns>Dictionary of english words</returns>
        public async Task<Dictionary<string, string>> Get()
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
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(await streamTask);
                return dict;
            }
        }
    }
}
