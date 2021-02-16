using GameOfGoose;
using NUnit.Framework;

namespace GameOfGooseTest
{
    internal class GameTests
    {
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _game = new Game();
        }

        [Test]
        public void CanInstantiateGameClass()
        {
            Assert.IsNotNull(_game);
        }
    }
}