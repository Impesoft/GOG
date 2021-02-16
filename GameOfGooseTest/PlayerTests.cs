using System.Threading;
using GameOfGoose;
using NUnit.Framework;

namespace GameOfGooseTest
{
    [Apartment(ApartmentState.STA)]
    internal class PlayerTests
    {
        private Player _player;
        private Dice _dice;

        [SetUp]
        public void Setup()
        {
            _player = new Player("Nick", 0);
            _dice = new Dice();
        }

        [Test]
        public void CanInstantiatePlayerClass()
        {
            Assert.IsNotNull(_player);
        }

        [Test]
        public void MovePlayer_WhenCalledWithNumber_CalculatePositionIsCorrect()
        {
            // Arrange
            int[] dice = { 5, 3 };
            int direction = 1;
            _player.Position = 7;
            int expectedResult = _player.Position + dice[0] + dice[1];

            // Act
            _player.Move((dice[0] + dice[1]) * direction);

            //Assert
            Assert.AreEqual(expectedResult, _player.Position);
        }
    }
}