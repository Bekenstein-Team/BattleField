namespace BattleField
{
    using System;
    using System.Text.RegularExpressions;

    public class BattleFieldGame
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to the \"Battle Field\" game.");
            Console.Write("Enter the size of the battle field between 1 and 10. ");

            int battleFieldSize;

            battleFieldSize = ReadCellsNumber();

            var battleField = new BattleField(battleFieldSize, new BoardInitializer());
            Console.WriteLine(battleField);
            while (battleField.RemainingMines > 0)
            {
                Console.Write("Please enter coordinates: ");
                string input = Console.ReadLine();
                if (input == null)
                {
                    throw new ArgumentException("You must enter coordinates.");
                }

                int row, col;
                string[] inputCoordinates = Regex.Split(input, "\\s+");
                if (inputCoordinates.Length != 2 ||
                    !int.TryParse(inputCoordinates[0], out row) ||
                    !int.TryParse(inputCoordinates[1], out col))
                {
                    throw new ArgumentException("You must enter at least two integer coordinates.");
                }

                try
                {
                    battleField.ProccessMove(row, col);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine(battleField);
            }

            Console.WriteLine("Game over. Detonated mines: {0}", battleField.DetonatedMinesCount);
        }
        public static int ReadCellsNumber()
        {
            string readThings;
            int readNumber;
            do
            {
                Console.Write("You must enter a valid integer = ");
                readThings = Console.ReadLine();

                if (!(Int32.TryParse(
                    readThings, out readNumber)))
                {
                    readNumber = -1;
                }
            }
            while (!(sizeChecker(readNumber)));

            return readNumber;
        }
        public static bool sizeChecker(int inputNumber)
        {
            if ((inputNumber < 1) || (inputNumber > 10))
                return false;
            else
                return true;
        }
    }

}
