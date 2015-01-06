namespace BattleField.Utils
{
    using System.IO;
    using System.Text.RegularExpressions;
    using Models;

    public class InputOutputHandler
    {
        private readonly TextReader textReader;
        private readonly TextWriter textWriter;

        public InputOutputHandler(TextReader reader, TextWriter writer)
        {
            this.textReader = reader;
            this.textWriter = writer;
        }

        public void WriteLine(object text)
        {
            this.textWriter.WriteLine(text);
        }

        public Coordinates ReadCoordinates()
        {
            this.textWriter.Write("Please enter coordinates X and Y separated by whitespace: ");
            string input = this.textReader.ReadLine();
            string[] inputCoordinates = Regex.Split(input, "\\s+");
            int row;
            int col;

            while (inputCoordinates.Length != 2 ||
                   !int.TryParse(inputCoordinates[0], out row) ||
                   !int.TryParse(inputCoordinates[1], out col))
            {
                this.textWriter.Write("Please enter two integer numbers separated by whitespace: ");
                input = this.textReader.ReadLine();
                inputCoordinates = Regex.Split(input, "\\s+");
            }

            return new Coordinates(row, col);
        }

        public int ReadBattleFieldSize()
        {
            this.textWriter.Write("Please enter the size of the battle field between 2 and 10: ");
            string input = this.textReader.ReadLine();
            int size;
            while (!int.TryParse(input, out size) ||
                   size < BattleField.MinBoardSize ||
                   size > BattleField.MaxBoardSize)
            {
                this.textWriter.Write("You must enter a valid integer between 2 and 10: ");
                input = this.textReader.ReadLine();
            }

            return size;
        }
    }
}
