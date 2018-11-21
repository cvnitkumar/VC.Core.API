using System.Collections.Generic;
using System.Threading.Tasks;

namespace VC.DAL.EnglishDictionary
{
    public interface IEnglishDictionaryData
    {
        /// <summary>
        /// Fetch the dictionary words from external resource
        /// </summary>
        /// <returns>dictionary of words and their meaning</returns>
        Task<Dictionary<string, string>> Get();
    }
}
