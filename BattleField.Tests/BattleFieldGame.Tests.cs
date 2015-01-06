namespace BattleField.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utils;

    [TestClass]
    public class BattleFieldGameTests
    {
        [TestMethod]
        public void TestBattleFieldGameInitialization()
        {
            var battleFieldGame = new BattleFieldGame();
            Assert.IsNotNull(battleFieldGame, "Incorrect BattleFieldGame initialization.");
        }

        [TestMethod]
        public void TestIOHandlerInitialization()
        {
            var ioHandler = new InputOutputHandler(Console.In, Console.Out);
            Assert.IsNotNull(ioHandler, "Incorrect InputOutputHander initialization.");
        }

        [TestMethod]
        public void TestIOHandlerWriteLine()
        {
            var output = new StringBuilder();
            var ioHandler = new InputOutputHandler(Console.In, new StringWriter(output));
            ioHandler.WriteLine(2);
            Assert.AreEqual(output.ToString(), "2\r\n", "Incorrect InputOutputHandler WriteLine result.");
        }

        [TestMethod]
        public void TestIOHandlerReadCorrectBattleFieldSize()
        {
            var ioHandler = new InputOutputHandler(new StringReader("7"), Console.Out);
            var size = ioHandler.ReadBattleFieldSize();
            Assert.AreEqual(size, 7, "Incorrect InputOutputHandler ReadBattleFieldSize result.");
        }

        [TestMethod]
        public void TestIOHandlerReadIncorrectBattleFieldSize()
        {
            var ioHandler = new InputOutputHandler(new StringReader("-5\r\n4"), Console.Out);
            var size = ioHandler.ReadBattleFieldSize();
            Assert.AreEqual(size, 4, "Incorrect InputOutputHandler ReadBattleFieldSize result.");
        }

        [TestMethod]
        public void TestIOHandlerReadCoordinates()
        {
            var ioHandler = new InputOutputHandler(new StringReader("-11\r\n6  8"), Console.Out);
            var coordinates = ioHandler.ReadCoordinates();
            Assert.AreEqual(coordinates.Col, 8, "Incorrect InputOutputHandler ReadCoordinates result.");
        }

        [TestMethod]
        public void TestCoordinatesRow()
        {
            var coordinates = new Coordinates(5, 6);
            Assert.AreEqual(coordinates.Row, 5, "Incorrect coordinates initialization.");
        }

        [TestMethod]
        public void TestCoordinatesCol()
        {
            var coordinates = new Coordinates(2, 12);
            Assert.AreEqual(coordinates.Col, 12, "Incorrect coordinates initialization.");
        }
    }
}
