using GameOfGoose;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameOfGooseTest
{
    internal class DiceTests
    {
        private Dice _dice;

        [SetUp]
        public void Setup()
        {
            _dice = new Dice();
        }

        [Test]
        public void CanInstantiateRollDiceClass()
        {
            Assert.IsNotNull(_dice);
        }

        [Test]
        public void Dice_WhenRolled_ReturnIntArray()
        {
            // Arrange
            int[] diceResult;

            // Act
            diceResult = _dice.Roll();

            // Assert
            Assert.IsNotNull(diceResult);
        }
    }
}