namespace BattleField.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BattleFieldTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The minimum battle field size is 2.", AllowDerivedTypes = true)]
        public void TestBattleFieldWithSizeZero()
        {
            var invalidBattleFieldSize = new BattleField(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The minimum battle field size is 2.", AllowDerivedTypes = true)]
        public void TestBattleFieldWithNegativeSize()
        {
            var invalidBattleFieldSize = new BattleField(-12);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The maximum battle field size is 10.", AllowDerivedTypes = true)]
        public void TestBattleFieldWithSizeGreatedThanTen()
        {
            var invalidBattleFieldSize = new BattleField(11);
        }

        [TestMethod]
        public void TestBattleFieldWithCorrectSize()
        {
            var battleField = new BattleField(5);
            Assert.AreEqual(battleField.Size, 5, "Incorect battlefield size.");
        }

        [TestMethod]
        public void TestDetonatedMinesCountAtStart()
        {
            var battleField = new BattleField(2);
            Assert.AreEqual(battleField.DetonatedMinesCount, 0, "Incorect detonated mines count. It should be 0 at start.");
        }

        [TestMethod]
        public void TestRemainingMinesAtStart()
        {
            var battleField = new BattleField(7);
            Assert.AreNotEqual(battleField.RemainingMines, 0, "All mines should not be detonated at start.");
        }

        [TestMethod]
        public void TestBoardBounds()
        {
            var battleField = new BattleField(9);
            Assert.IsFalse(battleField.Board.GetLowerBound(0) == 9 && battleField.Board.GetUpperBound(1) == 9, "Board should have 2 dimensions with valid lengths.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The column number must be within the board.", AllowDerivedTypes = true)]
        public void TestProcessMoveWithInvalidColumn()
        {
            var battleField = new BattleField(6);
            battleField.ProccessMove(4, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The row number must be within the board.", AllowDerivedTypes = true)]
        public void TestProcessMoveWithInvalidRow()
        {
            var battleField = new BattleField(5);
            battleField.ProccessMove(6, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The selected field must have mine on it.", AllowDerivedTypes = true)]
        public void TestProcessMoveOnFieldWithoutMine()
        {
            var battleField = new BattleField(4, new Random(0));
            battleField.ProccessMove(2, 1);
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeOne()
        {
            var battleField = new BattleField(6, new Random(0));
            battleField.ProccessMove(5, 5);
            Assert.AreEqual(battleField.Board[4, 4], "X", "Incorrect detonation of mine with size 1.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeTwo()
        {
            var battleField = new BattleField(6, new Random(0));
            battleField.ProccessMove(2, 5);
            Assert.AreEqual(battleField.Board[1, 4], "X", "Incorrect detonation of mine with size 2.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeThree()
        {
            var battleField = new BattleField(6, new Random(0));
            battleField.ProccessMove(4, 4);
            Assert.AreEqual(battleField.Board[4, 2], "X", "Incorrect detonation of mine with size 3.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeFour()
        {
            var battleField = new BattleField(6, new Random(0));
            battleField.ProccessMove(1, 2);
            Assert.AreEqual(battleField.Board[3, 1], "X", "Incorrect detonation of mine with size 4.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeFive()
        {
            var battleField = new BattleField(6, new Random(0));
            battleField.ProccessMove(4, 1);
            Assert.AreEqual(battleField.Board[2, 3], "X", "Incorrect detonation of mine with size 5.");
        }

        [TestMethod]
        public void TestBattleFieldToString()
        {
            var battleField = new BattleField(3, new Random(0));
            Assert.AreEqual(
                battleField.ToString(),
                "   0  1  2  \r\n   ----------\r\n0| -  5  - \r\n1| -  -  2 \r\n2| -  -  3 \r\n",
                "Incorrect ToString() behaviour.");
        }
    }
}
