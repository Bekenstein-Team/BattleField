

namespace BattleField.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mock;

    [TestClass]
    public class BattleFieldTests
    {
        private readonly IBoardInitializable borInitializablerActual = new BoardInitializer();
        private readonly IBoardInitializable borInitializablerMocked = new BoardInitialezerMock();

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The minimum battle field size is 2.", AllowDerivedTypes = true)]
        public void TestBattleFieldWithSizeZero()
        {
            var invalidBattleFieldSize = new BattleField(0, this.borInitializablerActual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The minimum battle field size is 2.", AllowDerivedTypes = true)]
        public void TestBattleFieldWithNegativeSize()
        {
            var invalidBattleFieldSize = new BattleField(-12, this.borInitializablerActual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The maximum battle field size is 10.", AllowDerivedTypes = true)]
        public void TestBattleFieldWithSizeGreatedThanTen()
        {
            var invalidBattleFieldSize = new BattleField(11, this.borInitializablerActual);
        }

        [TestMethod]
        public void TestBattleFieldWithCorrectSize()
        {
            var battleField = new BattleField(5, this.borInitializablerActual);
            Assert.AreEqual(battleField.Size, 5, "Incorect battlefield size.");
        }

        [TestMethod]
        public void TestDetonatedMinesCountAtStart()
        {
            var battleField = new BattleField(2, this.borInitializablerActual);
            Assert.AreEqual(battleField.DetonatedMinesCount, 0, "Incorect detonated mines count. It should be 0 at start.");
        }

        [TestMethod]
        public void TestRemainingMinesAtStart()
        {
            var battleField = new BattleField(7, this.borInitializablerActual);
            Assert.AreNotEqual(battleField.RemainingMines, 0, "All mines should not be detonated at start.");
        }

        [TestMethod]
        public void TestBoardBounds()
        {
            var battleField = new BattleField(9, this.borInitializablerActual);
            Assert.IsFalse(battleField.Board.GetLowerBound(0) == 9 && battleField.Board.GetUpperBound(1) == 9, "Board should have 2 dimensions with valid lengths.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The column number must be within the board.", AllowDerivedTypes = true)]
        public void TestProcessMoveWithInvalidColumn()
        {
            var battleField = new BattleField(6, this.borInitializablerActual);
            battleField.ProccessMove(4, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The row number must be within the board.", AllowDerivedTypes = true)]
        public void TestProcessMoveWithInvalidRow()
        {
            var battleField = new BattleField(5, this.borInitializablerActual);
            battleField.ProccessMove(6, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The selected field must have mine on it.", AllowDerivedTypes = true)]
        public void TestProcessMoveOnFieldWithoutMine()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(2, 2);
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeOne()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(2, 8);

            var expectedBoard = new string[10, 10]
            {
                { "-", "5", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "X", "3", "X" },
                { "-", "3", "-", "2", "-", "-", "4", "-", "X", "-" },
                { "-", "-", "-", "-", "-", "3", "-", "X", "-", "X" },
                { "-", "4", "-", "-", "-", "-", "2", "-", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "-", "-", "-", "-", "-", "2", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "2", "-", "4", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "3", "-", "2", "5" },
            };

            CollectionAssert.AreEquivalent(expectedBoard, battleField.Board, "Incorrect detonation of mine with size 1.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeTwo()
        {
            var battleField = new BattleField(6, this.borInitializablerMocked);
            battleField.ProccessMove(2, 3);

            var expectedBoard = new string[10, 10]
            {
                { "-", "5", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "X", "X", "X", "-", "-", "-", "3", "-" },
                { "-", "3", "X", "X", "X", "-", "4", "-", "1", "-" },
                { "-", "-", "X", "X", "X", "3", "-", "5", "-", "4" },
                { "-", "4", "-", "-", "-", "-", "2", "-", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "-", "-", "-", "-", "-", "2", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "2", "-", "4", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "3", "-", "2", "5" },
            };

            CollectionAssert.AreEquivalent(expectedBoard, battleField.Board, "Incorrect detonation of mine with size 2.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeThree()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(1, 8);

            var expectedBoard = new string[10, 10]
            {
                { "-", "5", "-", "-", "-", "-", "-", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "-", "X", "X", "X", "X" },
                { "-", "3", "-", "2", "-", "-", "4", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "3", "-", "5", "X", "4" },
                { "-", "4", "-", "-", "-", "-", "2", "-", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "-", "-", "-", "-", "-", "2", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "2", "-", "4", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "3", "-", "2", "5" },
            };

            CollectionAssert.AreEquivalent(expectedBoard, battleField.Board, "Incorrect detonation of mine with size 3.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeFour()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(2, 6);

            var expectedBoard = new string[10, 10]
            {
                { "-", "5", "-", "-", "-", "X", "X", "X", "-", "-" },
                { "-", "-", "-", "-", "X", "X", "X", "X", "X", "-" },
                { "-", "3", "-", "2", "X", "X", "X", "X", "X", "-" },
                { "-", "-", "-", "-", "X", "X", "X", "X", "X", "4" },
                { "-", "4", "-", "-", "-", "X", "X", "X", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "-", "-", "-", "-", "-", "2", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "2", "-", "4", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "3", "-", "2", "5" },
            };

            CollectionAssert.AreEquivalent(expectedBoard, battleField.Board, "Incorrect detonation of mine with size 4.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeFive()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(0, 1);

            var expectedBoard = new string[10, 10]
            {
                { "X", "X", "X", "X", "-", "-", "-", "-", "-", "-" },
                { "X", "X", "X", "X", "-", "-", "-", "-", "3", "-" },
                { "X", "X", "X", "X", "-", "-", "4", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "3", "-", "5", "-", "4" },
                { "-", "4", "-", "-", "-", "-", "2", "-", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "-", "-", "-", "-", "-", "2", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "2", "-", "4", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "3", "-", "2", "5" },
            };
            Assert.AreEqual(battleField.Board[2, 3], "X", "Incorrect detonation of mine with size 5.");
        }

        [TestMethod]
        public void TestBattleFieldToString()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            Assert.AreEqual(
                battleField.ToString(),
                "   0  1  2  3  4  5  6  7  8  9  \r\n   -------------------------------\r\n0| -  5  -  -  -  -  -  -  -  - \r\n1| -  -  -  -  -  -  -  -  3  - \r\n2| -  3  -  2  -  -  4  -  1  - \r\n3| -  -  -  -  -  3  -  5  -  4 \r\n4| -  4  -  -  -  -  2  -  2  - \r\n5| -  -  -  -  -  -  -  -  -  - \r\n6| -  -  -  -  -  -  -  -  3  - \r\n7| -  -  -  -  -  -  2  -  1  - \r\n8| -  -  -  -  -  2  -  4  -  - \r\n9| -  -  -  -  -  -  3  -  2  5 \r\n",
                "Incorrect ToString() behaviour.");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The selected field must have mine on it.", AllowDerivedTypes = true)]
        public void TestProcessMoveOnFieldWithoutMineTestExampleMove1()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The selected field must have mine on it.", AllowDerivedTypes = true)]
        public void TestProcessMoveOnFieldWithoutMineTestExampleMove2()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(2, 2);
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeThreeTestExampleMove3()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(2, 1);

            var expectedBoard = new string[10, 10]
            {
                { "-", "X", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "X", "X", "X", "-", "-", "-", "-", "-", "3", "-" },
                { "X", "X", "X", "X", "-", "-", "4", "-", "1", "-" },
                { "X", "X", "X", "-", "-", "3", "-", "5", "-", "4" },
                { "-", "X", "-", "-", "-", "-", "2", "-", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "-", "-", "-", "-", "-", "2", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "2", "-", "4", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "3", "-", "2", "5" },
            };

            CollectionAssert.AreEquivalent(expectedBoard, battleField.Board, "Not correct board after move on field wiht a mine with size 3.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeFourTestExampleMove4()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(2, 1);
            battleField.ProccessMove(8, 7);

            var expectedBoard = new string[10, 10]
            {
                { "-", "X", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "X", "X", "X", "-", "-", "-", "-", "-", "3", "-" },
                { "X", "X", "X", "X", "-", "-", "4", "-", "1", "-" },
                { "X", "X", "X", "-", "-", "3", "-", "5", "-", "4" },
                { "-", "X", "-", "-", "-", "-", "2", "-", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "X", "X", "X", "-" },
                { "-", "-", "-", "-", "-", "X", "X", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "X", "X", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "X", "X", "X", "X", "X" },
            };

            CollectionAssert.AreEquivalent(expectedBoard, battleField.Board, "Not correct board after move on field wiht a mine with size 5.");
        }

        [TestMethod]
        public void TestProcessMoveOnMineWithSizeFiveTestExampleMove5()
        {
            var battleField = new BattleField(10, this.borInitializablerMocked);
            battleField.ProccessMove(2, 1);
            battleField.ProccessMove(8, 7);
            battleField.ProccessMove(3, 7);

            var expectedBoard = new string[10, 10]
            {
                { "-", "X", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "X", "X", "X", "-", "-", "X", "X", "X", "X", "X" },
                { "X", "X", "X", "X", "-", "X", "X", "X", "X", "X" },
                { "X", "X", "X", "-", "-", "X", "X", "X", "X", "X" },
                { "-", "X", "-", "-", "-", "X", "X", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "X", "X", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "-", "X", "X", "X", "-" },
                { "-", "-", "-", "-", "-", "X", "X", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "X", "X", "X", "X", "X" },
                { "-", "-", "-", "-", "-", "X", "X", "X", "X", "X" },
            };

            CollectionAssert.AreEquivalent(expectedBoard, battleField.Board, "Not correct board after move on field wiht a mine with size 5.");
        }
    }
}
