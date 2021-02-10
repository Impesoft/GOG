using GameOfGoose;
using NUnit.Framework;

namespace GameOfGooseTest
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
        public void CanInstantiateGameBoardClass()
        {
            GameBoard gameBoard = new GameBoard();

            Assert.IsNotNull(gameBoard);
        }

        [Test]
        public void GivenPosition_WhenOnGoose_ReturnTrue()
        {
            // Arrange
            _player.Position = 9;
            // Act
            bool result = _gameBoard.Geese.Contains(_player.Position);
            // Assert
            Assert.AreEqual(true, result);
        }
    }
}