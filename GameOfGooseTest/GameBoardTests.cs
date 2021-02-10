using GameOfGoose;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameOfGooseTests
{
    internal class GameBoardTests
    {
        private Player _player;
        private GameBoard _gameBoard;

        [SetUp]
        public void Setup()
        {
            _player = new Player();
            _gameBoard = new GameBoard();
        }

        [Test]
        public void CanInstantiateGameClass()
        {
            GameBoard gameBoard = new GameBoard();

            Assert.IsNotNull(gameBoard);
        }

        [Test]
        public void GivenPosition_WhenOnGoose_ReturnTrue()
        {
            // Arrange
            _player.Position = 9;
            bool expectedResult = true;
            // Act
            bool result = _gameBoard.Geese.Contains(_player.Position);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}