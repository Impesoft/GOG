using GameOfGoose;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameOfGooseTests
{
    internal class GameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanInstantiateGameClass()
        {
            GameBoard gameBoard = new GameBoard();

            Assert.IsNotNull(gameBoard);
        }
    }
}