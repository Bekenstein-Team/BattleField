namespace BattleField.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BattleFieldTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Should not create battle field with size smaller than 1.", AllowDerivedTypes = true)]
        public void TestBattleFieldSizeWiht0ShouldReturnArgementOutOfRange()
        {
            // test when list is empty
            var invalidBattleFieldSize = new BattleField(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Should not create battle field with size smaller than 1.", AllowDerivedTypes = true)]
        public void TestBattleFieldSizeWihtNegativeValueShouldReturnArgementOutOfRange()
        {
            // test when list is empty
            var invalidBattleFieldSize = new BattleField(-12);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Should not create battle field with size bigger than 10.", AllowDerivedTypes = true)]
        public void TestBattleFieldSizeWiht11ShouldReturnArgementOutOfRange()
        {
            // test when list is empty
            var invalidBattleFieldSize = new BattleField(11);
        }

        [TestMethod]
        public void TestBattleFieldSizeWihtValidNumberShouldReturnMatrixWithSizes5()
        {
            var validBattleFieldSize = new BattleField(5);
            Assert.AreEqual(validBattleFieldSize.Size, 5, "Incorect battlefield size.");
        }

        [TestMethod]
        public void TestDetonatedMinesCountAtStartShouldReturn0()
        {
            var validBattleFieldSize = new BattleField(2);
            Assert.AreEqual(validBattleFieldSize.DetonatedMinesCount, 0, "Incorect Detonated mines count. It should be 0 at start.");
        }

        [TestMethod]
        public void TestAllMinesAreDetonatedAtStartShouldReturnFalse()
        {
            var validBattleField = new BattleField(7);
            Assert.AreNotEqual(validBattleField.RemainingMines, 0, "All mines should not be detonated at start.");
        }

        [TestMethod]
        public void TestBoardArrayLenghtWith9ShouldReturn9()
        {
            var validBattleField = new BattleField(9);
            Assert.IsFalse(validBattleField.Board.GetLowerBound(0) == 9 && validBattleField.Board.GetUpperBound(1) == 9, "Board should have 2 dimensions with valid lengths");
        }
    }
}
