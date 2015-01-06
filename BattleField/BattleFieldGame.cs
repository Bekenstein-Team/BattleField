namespace BattleField
{
    using System;
    using System.IO;
    using Models;
    using Utils;

    public class BattleFieldGame
    {
        private readonly InputOutputHandler inputOutputHandler;

        public BattleFieldGame()
            : this(Console.In, Console.Out)
        {
        }

        public BattleFieldGame(TextReader reader, TextWriter writer)
        {
            this.inputOutputHandler = new InputOutputHandler(reader, writer);
        }

        public void Run()
        {
            this.inputOutputHandler.WriteLine("Welcome to the \"Battle Field\" game.");

            int battleFieldSize = this.inputOutputHandler.ReadBattleFieldSize();
            var battleField = new BattleField(battleFieldSize, new BoardInitializer());
            this.inputOutputHandler.WriteLine(battleField);

            while (battleField.RemainingMines > 0)
            {
                var coordinates = this.inputOutputHandler.ReadCoordinates();
                while (!battleField.CoordinatesAreValid(coordinates.Row, coordinates.Col))
                {
                    this.inputOutputHandler.WriteLine("Invalid coordinates. You must select a field within the board with mine on it.");
                    coordinates = this.inputOutputHandler.ReadCoordinates();
                }

                battleField.ProccessMove(coordinates.Row, coordinates.Col);
                this.inputOutputHandler.WriteLine(battleField);
            }

            this.inputOutputHandler.WriteLine("Game over. Detonated mines: " + battleField.DetonatedMinesCount);
        }
    }
}
