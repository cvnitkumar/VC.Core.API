using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VC.BLL.Board;
using VC.DAL.EnglishDictionary;

namespace VC.BLL.UnitTests
{
    [TestClass]
    public class WordsBoardTests
    {
        #region GetAllWords
        [TestMethod]
        public async Task GetAllWords_GivenBoardWhenEmptyDictionary_ReturnsEmptyList()
        {
            //Setup
            //mock implementation of service using Moq
            var serviceMock = new Mock<IEnglishDictionaryData>();
            var loggerMock = new Mock<ILogger<WordsBoard>>();
            var wordsBoardObj = new WordsBoard(serviceMock.Object, loggerMock.Object);
            var wordBoard = new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'x', 'o' ,'l'}
            };

            serviceMock
                .Setup(a => a.Get())
                .ReturnsAsync(new Dictionary<string, string>())
                .Verifiable();
            
            //Act
            var result = await wordsBoardObj.GetAllWords(wordBoard);

            //Verify
            // Verify that Get was called only once
            serviceMock.Verify((m => m.Get()), Times.Once());
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public async Task GetAllWords_GivenBoardWhenOneWordInDictionary_ReturnsOneWord()
        {
            //Setup
            //mock implementation of service using Moq
            var serviceMock = new Mock<IEnglishDictionaryData>();
            var loggerMock = new Mock<ILogger<WordsBoard>>();
            var wordsBoardObj = new WordsBoard(serviceMock.Object, loggerMock.Object);
            var wordBoard = new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'x', 'o' ,'l'}
            };
            var dict = new Dictionary<string, string>();
            dict.Add("Hello", "Hello world!");

            serviceMock
                .Setup(a => a.Get())
                .ReturnsAsync(dict)
                .Verifiable();

            //Act
            var result = await wordsBoardObj.GetAllWords(wordBoard);

            //Verify
            // Verify that Get was called only once
            serviceMock.Verify((m => m.Get()), Times.Once());
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.ContainsKey("Hello"));
            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAllWords_GivenEmptyBoard_ShouldThrowArgumentExcetion()
        {
            //Setup
            //mock implementation of service using Moq
            var serviceMock = new Mock<IEnglishDictionaryData>();
            var loggerMock = new Mock<ILogger<WordsBoard>>();
            var wordsBoardObj = new WordsBoard(serviceMock.Object, loggerMock.Object);
            var wordBoard = new char[0, 0];

            var dict = new Dictionary<string, string>();
            dict.Add("Hello", "Hello world!");

            serviceMock
                .Setup(a => a.Get())
                .ReturnsAsync(dict)
                .Verifiable();

            //Act
            var result = await wordsBoardObj.GetAllWords(wordBoard);

            return;
        }
        #endregion

        #region DoesWordExists
        [TestMethod]
        public async Task DoesWordExists_GivenBoardAndExistingWord_ReturnsTrue()
        {
            //Setup
            //mock implementation of service using Moq
            var serviceMock = new Mock<IEnglishDictionaryData>();
            var loggerMock = new Mock<ILogger<WordsBoard>>();
            var wordsBoardObj = new WordsBoard(serviceMock.Object, loggerMock.Object);
            var wordBoard = new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'x', 'o' ,'l'}
            };

            //Act
            var result = await wordsBoardObj.DoesWordExists(wordBoard, "Hello");

            //Verify
            Assert.IsTrue(result);
            return;
        }

        [TestMethod]
        public async Task DoesWordExists_GivenBoardAndNonAdjacantCharWord_ReturnsFalse()
        {
            //Setup
            //mock implementation of service using Moq
            var serviceMock = new Mock<IEnglishDictionaryData>();
            var loggerMock = new Mock<ILogger<WordsBoard>>();
            var wordsBoardObj = new WordsBoard(serviceMock.Object, loggerMock.Object);
            var wordBoard = new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'o', 'x' ,'l'}
            };

            //Act
            var result = await wordsBoardObj.DoesWordExists(wordBoard, "Hello");

            //Verify
            Assert.IsFalse(result);
            return;
        }

        [TestMethod]
        public async Task DoesWordExists_GivenBoardAndNonExistingWord_ReturnsFalse()
        {
            //Setup
            //mock implementation of service using Moq
            var serviceMock = new Mock<IEnglishDictionaryData>();
            var loggerMock = new Mock<ILogger<WordsBoard>>();
            var wordsBoardObj = new WordsBoard(serviceMock.Object, loggerMock.Object);
            var wordBoard = new char[3, 3] {
                { 'h', 'b', 'p' }
                , { 'c', 'e','l' }
                , { 'o', 'x' ,'l'}
            };

            //Act
            var result = await wordsBoardObj.DoesWordExists(wordBoard, "USA");

            //Verify
            Assert.IsFalse(result);
            return;
        }

        [TestMethod]
        public async Task DoesWordExists_GivenLargeBoardAndExistingWord_ReturnsTrue()
        {
            //Setup
            //mock implementation of service using Moq
            var serviceMock = new Mock<IEnglishDictionaryData>();
            var loggerMock = new Mock<ILogger<WordsBoard>>();
            var wordsBoardObj = new WordsBoard(serviceMock.Object, loggerMock.Object);
            var wordBoard = new char[10, 10] {
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
            };

            //Act
            var result = await wordsBoardObj.DoesWordExists(wordBoard, "HelloWorld");

            //Verify
            Assert.IsTrue(result);
            return;
        }
        #endregion
    }
}
