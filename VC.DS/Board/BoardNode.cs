using System;
using System.Collections.Generic;

namespace VC.DS.Board
{
    public class BoardNode<T>        
    {
        public T Value { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }

        public IList<BoardNode<T>> AdjecentNodes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"> Node Value</param>
        /// <param name="rowInd">row index of node in board</param>
        /// <param name="colInd">column index of node in board</param>
        public BoardNode(T val, int rowInd, int colInd)
        {            
            if (val == null)
                throw new ArgumentNullException("val is required");

            if (rowInd < 0)
                throw new ArgumentException("rowInd must be positive integer");

            if (colInd < 0)
                throw new ArgumentException("colInd must be positive integer");

            Value = val;
            RowIndex = rowInd;
            ColIndex = colInd;
            AdjecentNodes = new List<BoardNode<T>>();
        }

        /// <summary>
        /// Add given node to adjecent nodes list
        /// </summary>
        /// <param name="node">Board Node</param>
        public void AddAdjecentNode(BoardNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("node is required");

            AdjecentNodes.Add(node);
        }
    }
}
