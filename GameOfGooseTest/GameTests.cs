using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameOfGoose;
using GameOfGoose.Interface;
using NUnit.Framework;

namespace GameOfGooseTest
{
    [Apartment(ApartmentState.STA)]
    internal class GameTests
    {
        private Game _game;
        private Player _player;
        private List<Player> Players = Settings.Players.ToList();

        private List<ISquare> SquarePathList { get; set; } = new List<ISquare>();
        private List<int> _geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

        [SetUp]
        public void Setup()
        {
            _game = new Game();
            _player = new Player("Nick", 0);
            Players.Add(_player);
            _player = new Player("Ward", 0);
            Players.Add(_player);
        }

        [Test]
        public void CanInstantiateGameClass()
        {
            Assert.IsNotNull(_game);
        }

        [Test]
        public void Check_IfSquaresAreAddedToSquarePathList()
        {
            // Arrange
            SquarePathList = _game.InitializeSquares();
            int expectedResult = 64;
            // Act
            int numberOfSquares = SquarePathList.Count;
            //Assert
            Assert.AreEqual(expectedResult, numberOfSquares);
        }

        [Test]
        public void GivenPosition_CheckIfOnGoose_ReturnTrue()
        {
            // Arrange
            _player.Position = 9;
            // Act
            bool result = _geese.Contains(_player.Position);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Check_IsFirstThrow_ReturnTrue()
        {
            // Arrange
            var turn = 3;
            var playerCount = 4;

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
            Players[1].Position = 63;
            // Act
            var winner = Players.SingleOrDefault(player => player.Position == 63);
            // Assert
            Assert.IsNotNull(winner);
        }
    }
}