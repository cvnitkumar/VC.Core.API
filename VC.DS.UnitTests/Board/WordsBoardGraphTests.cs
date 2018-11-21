using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace VC.DS.Board.UnitTests
{    
    [TestClass]
    public class WordsBoardGraphTests
    {
        #region Constructor
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullBoard_ShouldThrowArgumentNullException()
        {
            var wordBoardGraph = new WordsBoardGraph(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenEmptylBoard_ShouldThrowArgumentNullException()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[0, 0]);
        }

        [TestMethod]
        public void Constructor_GivenValidBoard_ShouldCreateObject()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsNotNull(wordBoardGraph);
        }

        #endregion

        #region DoesPathExists

        [TestMethod]
        public void DoesPathExists_GivenNullWord_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsFalse(wordBoardGraph.DoesPathExists(null));
        }

        [TestMethod]
        public void DoesPathExists_GivenEmptyWord_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsFalse(wordBoardGraph.DoesPathExists(string.Empty));
        }


        [TestMethod]
        public void DoesPathExists_GivenWordHavingMoreCharsThanBoard_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsFalse(wordBoardGraph.DoesPathExists("abcx"));
        }

        [TestMethod]
        public void DoesPathExists_GivenWordWhenStartCharDoesntExistsInBoard_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsFalse(wordBoardGraph.DoesPathExists("xbc"));
        }

        [TestMethod]
        public void DoesPathExists_GivenWordWhenLastCharDoesntExistsInBoard_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsFalse(wordBoardGraph.DoesPathExists("abx"));
        }

        [TestMethod]
        public void DoesPathExists_GivenWordWhenAnyCharDoesntExistsInBoard_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsFalse(wordBoardGraph.DoesPathExists("abxc"));
        }

        [TestMethod]
        public void DoesPathExists_GivenWordDoesntExistsInBoard_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsFalse(wordBoardGraph.DoesPathExists("axb"));
        }

        [TestMethod]
        public void DoesPathExists_GivenWordExistsInBoard_ShouldReturnTrue()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[2, 2] { { 'a', 'b' }, { 'c', 'd' } });
            Assert.IsTrue(wordBoardGraph.DoesPathExists("ab"));
        }

        [TestMethod]
        public void DoesPathExists_GivenWordExistsWithRepeatChar_ShouldReturnTrue()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'x', 'o' ,'l'}
            });
            Assert.IsTrue(wordBoardGraph.DoesPathExists("hello"));
        }

        [TestMethod]
        public void DoesPathExists_GivenWordNonAdjacantChars_ShouldReturnFalse()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'o', 'x' ,'l'}
            });
            Assert.IsFalse(wordBoardGraph.DoesPathExists("hello"));
        }

        [TestMethod]
        public void DoesPathExists_GivenUpperCaseWordExistsInBoard_ShouldReturnTrue()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'x', 'o' ,'l'}
            });
            Assert.IsTrue(wordBoardGraph.DoesPathExists("HELLO"));
        }

        [TestMethod]
        public void DoesPathExists_WithBigBoardGivenWordExistsInBoard_ShouldReturnTrue()
        {
            var wordBoardGraph = new WordsBoardGraph(new char[10, 10] {
                  { 'a','v','c','n','g','h','b','p','f','h'}
                , { 'a','v','c','n','g','c','e','l','t','y'}
                , { 'a','v','c','n','g','x','o','l','w','r'}
                , { 'a','v','c','n','g','x','w','h','w','r'}
                , { 'a','v','c','n','g','o','s','t','w','r'}
                , { 'a','v','c','n','g','r','l','r','w','r'}
                , { 'a','v','c','n','g','x','d','s','w','r'}
                , { 'a','v','c','n','g','x','l','a','w','r'}
                , { 'a','v','c','n','g','x','w','s','w','r'}
                , { 'a','v','c','n','g','x','s','a','w','r'}
            });
            Assert.IsTrue(wordBoardGraph.DoesPathExists("HelloWorld"));
        }
        #endregion
    }
}
