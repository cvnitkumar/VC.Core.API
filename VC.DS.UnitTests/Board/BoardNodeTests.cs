using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace VC.DS.Board.UnitTests
{
    [TestClass]
    public class BoardNodeTests
    {
        #region Constructor
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_GivenNegativeRowInd_ShouldThrowArgumentException()
        {
            var boardNode = new BoardNode<char>('a', -1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_GivenNegativeColInd_ShouldThrowArgumentException()
        {
            var boardNode = new BoardNode<char>('a', 1 , -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullValue_ShouldThrowArgumentNullException()
        {
            var boardNode = new BoardNode<char?>(null, 1, 1);
        }

        [TestMethod]
        public void Constructor_GivenValidParams_ShouldCreateObject()
        {
            var boardNode = new BoardNode<char?>('a', 2, 1);
            Assert.AreEqual('a', boardNode.Value);
            Assert.AreEqual(2, boardNode.RowIndex);
            Assert.AreEqual(1, boardNode.ColIndex);
            Assert.IsTrue(boardNode.AdjecentNodes.Count == 0);
        }

        #endregion

        #region AddAdjecentNode
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddAdjecentNode_GivenNullNode_ShouldThrowArgumentNullException()
        {
            var boardNode = new BoardNode<char?>('a', 1, 1);
            boardNode.AddAdjecentNode(null);
        }

        [TestMethod]
        public void AddAdjecentNode_GivenNode_ShouldAddNodeToAdjecentNodesList()
        {
            var boardNode = new BoardNode<char>('a', 1, 1);
            boardNode.AddAdjecentNode(new BoardNode<char>('b', 1, 0));
            Assert.IsTrue(boardNode.AdjecentNodes.Count == 1);
            Assert.AreEqual('b', boardNode.AdjecentNodes.FirstOrDefault().Value);
            Assert.AreEqual(1, boardNode.AdjecentNodes.FirstOrDefault().RowIndex);
            Assert.AreEqual(0, boardNode.AdjecentNodes.FirstOrDefault().ColIndex);
        }
        #endregion
    }
}
