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

        [Test]
        public void MoveToSkipsGeesePositions()
        {
            //Assign
            int _position = 0;
            int[] _throw = { 4, 1 };
            //Act
            User user = new User("Ward", "/pathtowardimg.jpg");
            int result = user.moveto(_throw, _position);
            //Assert
            Assert.AreEqual(10, result);
        }
    }
}