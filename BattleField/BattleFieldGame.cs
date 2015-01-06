namespace BattleField
{
    using System;
    using System.Text.RegularExpressions;

    public class BattleFieldGame
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to the \"Battle Field\" game.");

            int battleFieldSize = ReadBattleFieldSize();
            var battleField = new BattleField(battleFieldSize, new BoardInitializer());
            Console.WriteLine(battleField);

            while (battleField.RemainingMines > 0)
            {
                int row;
                int col;
                ValidateCoordinates(battleFieldSize, out row, out col);

                try
                {
                    battleField.ProccessMove(row, col);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message + "\r\n");
                }
                Console.WriteLine(battleField);

            }
            Console.WriteLine("Game over. Detonated mines: {0}", battleField.DetonatedMinesCount);
        }

        private static void ValidateCoordinates(int battleFieldSize, out int row, out int col)
        {
            Console.Write("Please enter coordinates X and Y with space between: ");
            string input;
            row = 0;
            col = 0;
            bool isValidCoordinates;
            do
            {
                input = Console.ReadLine();
                string[] inputCoordinates = Regex.Split(input, "\\s+");

                if (!int.TryParse(inputCoordinates[0], out row) ||
                    !int.TryParse(inputCoordinates[1], out col))
                {
                    Console.WriteLine("You must enter two integer numbers.");
                }
                isValidCoordinates = row < battleFieldSize && row >= 0 &&
                                    col < battleFieldSize && col >= 0;
            } while (false || (!isValidCoordinates));
        }

        private static int ReadBattleFieldSize()
        {
            Console.Write("Please enter the size of the battle field between 2 and 10: ");
            string input = Console.ReadLine();
            int size;
            while (!int.TryParse(input, out size) || size < 2 || size > 10)
            {
                Console.Write("You must enter a valid integer between 2 and 10: ");
                input = Console.ReadLine();
            }
            return size;
        }
    }
}
