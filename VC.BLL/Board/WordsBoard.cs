using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VC.DAL.EnglishDictionary;
using VC.DS.Board;

namespace VC.BLL.Board
{
    public class WordsBoard : IWordsBoard
    {
        private readonly IEnglishDictionaryData _englishDictionaryData;
        private readonly ILogger _logger;

        public WordsBoard(IEnglishDictionaryData iEnglishDictionaryData, ILogger<WordsBoard> logger)
        {
            _englishDictionaryData = iEnglishDictionaryData;
            _logger = logger;
        }

        /// <summary>
        /// Get all matching words in a give board
        /// </summary>
        /// <param name="board">Board of characaters</param>
        /// <returns>all matching words in a give board along with meaning</returns>
        public async Task<Dictionary<string, string>> GetAllWords(char[,] board)
        {
            var matchingWords = new Dictionary<string, string>();
            try
            {
                // get all words from dictionary
                _logger.LogInformation("Loading dictionary data - Begin");
                var dictionaryTask = _englishDictionaryData.Get();

                // create and build the Words Board graph
                _logger.LogInformation("Building WordsBoardGraph - Begin");
                var boardGraph = new WordsBoardGraph(board);
                _logger.LogInformation("Building WordsBoardGraph - Completed");

                // wait for dictionary task to return data
                var dictionaryDate = await dictionaryTask;

                _logger.LogInformation("Loading dictionary data - Completed");
                _logger.LogInformation("Searching Words in graph - Begin");
                
                // Loop through each dictionary word and search the word in graph, 
                // if found in board then add it to list            
                foreach (var word in dictionaryDate)
                {
                    // Word is nothing but a graph path in a board, 
                    // so search the path (word) in graph, if the length of path is not fewer than 3 chars
                    if (word.Key.Length >= 3 && boardGraph.DoesPathExists(word.Key))
                        matchingWords.Add(word.Key, word.Value);
                }

                _logger.LogInformation("Searching Words in graph - Completed");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }

            // return the list
            return matchingWords;
        }

        /// <summary>
        /// Verify whether given word exists in a given baord
        /// </summary>
        /// <param name="board">Board of characaters</param>
        /// <param name="word">Word to search</param>
        /// <returns>true/false</returns>
        public async Task<bool> DoesWordExists(char[,] board, string word)
        {
            var boardGraphObj = new WordsBoardGraph(board);

            // Word is nothing but a graph path in a board, 
            // so search the path (word) in graph
            return await Task.Run<bool>(() => boardGraphObj.DoesPathExists(word));
        }
    }
}
