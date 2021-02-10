using GameOfGoose;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameOfGooseTest
{
    public class Tests
    {
        private Player _player;
        private Dice _dice;
        private GameBoard _gameBoard;

        [SetUp]
        public void Setup()
        {
            _player = new Player();
            _dice = new Dice();
            _gameBoard = new GameBoard();
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
            _player.Position = 7;
            int expectedResult = _player.Position + dice[0] + dice[1];

            // Act
            _player.Move(dice);

            if (_gameBoard.Geese.Contains(_player.Position))
            {
                _player.Move(dice);
            }

            //Assert
            Assert.AreEqual(expectedResult, _player.Position);
        }
    }
}