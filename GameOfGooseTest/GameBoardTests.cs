using GameOfGoose;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace GameOfGooseTest
{
    internal class GameBoardTests
    {
        private List<Player> _players;
        private Player _player;
        private GameBoard _gameBoard;
        private Settings _settings;

        [SetUp]
        public void Setup()
        {
            _player = new Player();
            _gameBoard = new GameBoard();
            _settings = new Settings();
            _players = _settings.GetPlayers();
        }

        [Test]
        public void CanInstantiateGameBoardClass()
        {
            Assert.IsNotNull(_gameBoard);
        }

        [Test]
        public void Check_IfSquaresAreAddedToSquarePathList()
        {
            // Arrange
            _gameBoard.GetSquares();
            int expectedResult = 64;
            // Act
            int numberOfSquares = _gameBoard.SquarePathList.Count;
            //Assert
            Assert.AreEqual(expectedResult, numberOfSquares);
        }

        [Test]
        public void GivenPosition_CheckIfOnGoose_ReturnTrue()
        {
            // Arrange
            _player.Position = 9;
            // Act
            bool result = _gameBoard.Geese.Contains(_player.Position);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Check_IsFirstThrow_ReturnTrue()
        {
            // Arrange
            var turn = _settings.Turn;
            var playerCount = _players.Count;

            // Act
            int round = turn / playerCount;
            bool result = round == 0;
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Check_WhenWinner_ReturnTrue()
        {
            // Arrange
            _players[0].Position = 63;
            // Act
            var winner = _settings.Players.SingleOrDefault(player => player.Position == 63);
            // Assert
            Assert.IsNotNull(winner);
        }
    }
}