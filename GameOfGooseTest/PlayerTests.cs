using GameOfGoose;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameOfGooseTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanInstantiatePlayerClass()
        {
            Player player = new Player();

            Assert.IsNotNull(player);
        }

        [Test]
        public void MovePlayer_WhenCalledWithNumber_CalculatePositionIsCorrect()
        {
            // Arrange
            Player player = new Player();
            int amountOfEyes = 5;
            player.Position = 7;

            List<int> geese = player.Geese;
            // Act
            player.Move(amountOfEyes);

            if (true)
            {
            }
            //Assert
            Assert.AreEqual(12, player.Position);
        }
    }
}