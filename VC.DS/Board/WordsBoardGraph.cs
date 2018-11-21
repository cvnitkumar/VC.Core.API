using System;

namespace VC.DS.Board
{
    public class WordsBoardGraph
    {
        private BoardNode<char>[,] BoardNodes;
        private bool[] CharsInBoard = new bool[128];

        #region Constructor
        /// <summary>
        /// Words Board Graph structure
        /// </summary>
        /// <param name="boardChars"> Two dimentional array of characters</param>
        public WordsBoardGraph(char[,] boardChars)
        {
            if (boardChars == null || boardChars.Length == 0)
                throw new ArgumentNullException("boardChars is required");

            BoardNodes = new BoardNode<char>[boardChars.GetLength(0), boardChars.GetLength(1)];
            LoadBoardNodes(boardChars);
            BuildGraph();
        }

        #endregion

        #region Constructor Helpers
        /// <summary>
        /// Load the Board for creating BoardNode for each tile
        /// </summary>
        /// <param name="boardChars"></param>
        private void LoadBoardNodes(char[,] boardChars)
        {
            for (int i = 0; i < boardChars.GetLength(0); i++)
            {
                for (int j = 0; j < boardChars.GetLength(1); j++)
                {
                    // Create Board Node with upper case character
                    BoardNodes[i, j] = new BoardNode<char>(char.ToUpper(boardChars[i, j]), i, j);

                    // set the flag for each character in this array for quick validations
                    CharsInBoard[char.ToUpper(boardChars[i, j])] = true;
                }
            }
        }

        /// <summary>
        /// Build the graph by connecting adjecent nodes/tiles.
        /// A character is considered adjacent to the eight tiles that surround it (diagonal paths are allowed). 
        /// Tiles on the edge of the board are adjacent to only five tiles. 
        /// Tiles in the corner are adjacent to only three.
        /// </summary>
        private void BuildGraph()
        {
            for (int r = 0; r < BoardNodes.GetLength(0); r++)
            {
                for (int c = 0; c < BoardNodes.GetLength(1); c++)
                {
                    // Add top row adjecent nodes
                    if (r - 1 >= 0)
                    {
                        if (c - 1 >= 0)
                            BoardNodes[r, c].AddAdjecentNode(BoardNodes[r - 1, c - 1]); // Top left corner node

                        if (c + 1 < BoardNodes.GetLength(1))
                            BoardNodes[r, c].AddAdjecentNode(BoardNodes[r - 1, c + 1]); // Top right corner node

                        BoardNodes[r, c].AddAdjecentNode(BoardNodes[r - 1, c]); // Top center node
                    }

                    // Add bottom row adjecent nodes
                    if (r + 1 < BoardNodes.GetLength(0))
                    {
                        if (c - 1 >= 0)
                            BoardNodes[r, c].AddAdjecentNode(BoardNodes[r + 1, c - 1]); // Bottom left corner node

                        if (c + 1 < BoardNodes.GetLength(1))
                            BoardNodes[r, c].AddAdjecentNode(BoardNodes[r + 1, c + 1]); // Bottom right corner node

                        BoardNodes[r, c].AddAdjecentNode(BoardNodes[r + 1, c]); // Bottom center node
                    }

                    // Add same row adjecent nodes
                    if (c - 1 >= 0)
                        BoardNodes[r, c].AddAdjecentNode(BoardNodes[r, c - 1]); // left node

                    if (c + 1 < BoardNodes.GetLength(1))
                        BoardNodes[r, c].AddAdjecentNode(BoardNodes[r, c + 1]); // right node
                }
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Depth first search approach to search path in the board graph
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="visited"></param>
        /// <param name="path"></param>
        /// <param name="currentPathIndex"></param>
        /// <returns></returns>
        private bool DepthFirstSearchPath(BoardNode<char> currentNode, bool[,] visited, string path, int currentPathIndex)
        {
            // Last char case. Current node value matches the last char of path, then return true and end DFS.
            if (currentPathIndex.Equals(path.Length - 1) && currentNode.Value.Equals(path[currentPathIndex]))
                return true;

            // if the index is out of range or current node value doesn't match current char in path then end DFS.
            if (currentPathIndex >= path.Length || !currentNode.Value.Equals(path[currentPathIndex]))
                return false;

            // Mark the current node as visited 
            visited[currentNode.RowIndex, currentNode.ColIndex] = true;

            foreach (var node in currentNode.AdjecentNodes)
            {
                // Search the next node if not visited and returns true if next node return true to end DFS.
                if (!visited[node.RowIndex, node.ColIndex]
                    && DepthFirstSearchPath(node, visited, path, currentPathIndex + 1))
                    return true;
            }
            return false;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Returns true if given path exists in board.
        /// </summary>
        /// <param name="path">path to search in graph</param>
        /// <returns>true or false</returns>
        public bool DoesPathExists(string path)
        {

            // If path length is greater than total nodes on board, then it will not match
            if (string.IsNullOrWhiteSpace(path) || path.Length > BoardNodes.Length)
                return false;

            path = path.ToUpper();

            // check whether each char of path exists in board, if any char doesn't exist then return false.
            foreach (var c in path)
            {
                if (!CharsInBoard[c])
                    return false;
            }

            // Get node of starting character and do depth first search to see if the path matches any DFS path.
            for (int i = 0; i < BoardNodes.GetLength(0); i++)
            {
                for (int j = 0; j < BoardNodes.GetLength(1); j++)
                {
                    if (BoardNodes[i, j].Value.Equals(char.ToUpper(path[0])))
                    {
                        if (DepthFirstSearchPath(BoardNodes[i, j]
                            , new bool[BoardNodes.GetLength(0), BoardNodes.GetLength(1)]
                            , path.ToUpper()
                            , 0))
                            return true;
                    }
                }
            }

            // This line will reach if path doesnt exists in any graph path
            return false;
        }

        #endregion
    }
}
