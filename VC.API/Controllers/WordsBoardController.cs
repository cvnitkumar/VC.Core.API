using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VC.BLL.Board;

namespace VC.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WordsBoardController : ControllerBase
    {
        // services
        private readonly IWordsBoard _wordsBoardBO;

        public WordsBoardController(IWordsBoard iWordsBoard)
        {
            _wordsBoardBO = iWordsBoard;
        }

        /// <summary>
        /// This API will return all valid words in a given board.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/GetAllWords
        ///        [
        ///          [ 'h', 'b', 'p']
        ///         ,[ 'c', 'e', 'l']
        ///         ,[ 'x', 'o', 'l']
        ///        ]
        /// </remarks>
        /// <param name="board"></param>
        /// <returns>All matching words in a board along with their meaning</returns> 
        /// <response code="200">Returns all matching words in a board along with their meaning</response>
        /// <response code="400">If the board is null or empty</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("GetAllWords")]
        public async Task<ActionResult<Dictionary<string, string>>> GetAllWords([FromBody] char[,] board)
        {
            var result = await _wordsBoardBO.GetAllWords(board);
            return Ok(result);
        }

        /// <summary>
        /// This API will search the given word in the given baord and returns true if it exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/DoesWordExists/hello
        ///        [
        ///          [ 'h', 'b', 'p']
        ///         ,[ 'c', 'e', 'l']
        ///         ,[ 'x', 'o', 'l']
        ///        ]        ///
        /// </remarks>
        /// <param name="board">board characters</param>
        /// <param name="word">word to search</param>
        /// <returns>bool</returns>    
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the board is null or empty</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("DoesWordExists/{word}")]
        public async Task<ActionResult<bool>> DoesWordExists(string word, [FromBody] char[,] board)
        {
            var result = await _wordsBoardBO.DoesWordExists(board, word);
            return Ok(result);
        }
    }
}