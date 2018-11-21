using System.Collections.Generic;
using System.Threading.Tasks;

namespace VC.BLL.Board
{
    public interface IWordsBoard
    {
        /// <summary>
        /// Get all matching words in a give board
        /// </summary>
        /// <param name="board">Board of characaters</param>
        /// <returns>all matching words in a give board along with meaning</returns>
        Task<Dictionary<string, string>> GetAllWords(char[,] board);

        /// <summary>
        /// Verify whether given word exists in a given baord
        /// </summary>
        /// <param name="board">Board of characaters</param>
        /// <param name="word">Word to search</param>
        /// <returns>true/false</returns>
        Task<bool> DoesWordExists(char[,] board, string word);
    }
}
