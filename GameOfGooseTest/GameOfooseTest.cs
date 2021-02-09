using NUnit.Framework;
using GameOfGoose;

namespace GameOfGooseTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanInstatiateGogGame()
        {
            GogGame gogGame = new GogGame();
        }
    }
}