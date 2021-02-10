using GameOfGoose;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameOfGooseTest
{
    internal class DiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanInstantiateRollDiceClass()
        {
            Dice dice = new Dice();

            Assert.IsNotNull(dice);
        }

        [Test]
        public void Dice_WhenRolled_ReturnIntArray()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}