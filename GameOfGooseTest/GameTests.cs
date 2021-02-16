using System.Threading;
using GameOfGoose;
using NUnit.Framework;

namespace GameOfGooseTest
{
    [Apartment(ApartmentState.STA)]
    internal class GameTests
    {
        private Game _game;
        private GameBoard _gameBoard;

        [SetUp]
        public void Setup()
        {
            //_gameBoard = new GameBoard();
            _game = new Game(null);
        }

        [Test]
        public void CanInstantiateGameClass()
        {
            Assert.IsNotNull(_game);
        }
    }
}