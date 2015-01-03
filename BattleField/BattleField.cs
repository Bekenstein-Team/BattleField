namespace BattleFiled
{
    using System;
    using System.Text.RegularExpressions;

    public class BattleField
    {
        private static int battleFieldSize;
        private static string[,] battleField;

        public static void Main()
        {
            Console.WriteLine("Welcome to the \"Battle Field\" game.");
            Console.Write("Enter the size of the battle field between 1 and 10: ");
            InitializeField();

            Console.WriteLine();
            PrintField();
            Console.WriteLine();
            int moveCounter = 0;

            // main loop
            while (true)
            {
                Console.Write("Please enter coordinates: ");
                int[] userInput = ProcessUserInput();
                int row = userInput[0];
                int col = userInput[1];

                // checking the selected field
                if (battleField[row, col] == "-" || battleField[row, col] == "X")
                {
                    Console.WriteLine("Invalid move!");
                }
                else
                {
                    ProcessMineDetonation(row, col);
                    moveCounter++;
                }

                PrintField();

                // checking if there are more mines
                if (CountDetonatedFields() >= battleFieldSize * battleFieldSize)
                {
                    Console.WriteLine("Game over. Detonated mines: {0}", moveCounter);
                    break;
                }
            }
        }

        private static void PrintField()
        {
            Console.Write("   0  ");
            for (int i = 1; i < battleField.GetLength(0); i++)
            {
                Console.Write("{0}  ", i);
            }

            Console.WriteLine();
            Console.Write("   -");
            for (int i = 0; i < battleField.GetLength(0); i++)
            {
                Console.Write("---");
            }

            Console.WriteLine();
            for (int i = 0; i < battleField.GetLength(0); i++)
            {
                Console.Write("{0}|", i);
                for (int j = 0; j < battleField.GetLength(1); j++)
                {
                    Console.Write(" {0} ", battleField[i, j]);
                }

                Console.WriteLine();
            }
        }

        private static void InitializeField()
        {
            if (!int.TryParse(Console.ReadLine(), out battleFieldSize) ||
                    battleFieldSize > 10 ||
                    battleFieldSize < 1)
            {
                throw new ArgumentException("The battle field size must be an integer between 1 and 10.");
            }

            Random randomGenerator = new Random();
            battleField = new string[battleFieldSize, battleFieldSize];

            // filling the filed with dashes
            for (int row = 0; row < battleFieldSize; row++)
            {
                for (int col = 0; col < battleFieldSize; col++)
                {
                    battleField[row, col] = "-";
                }
            }
            
            // setting the mines in the field
            int minMines = Convert.ToInt32(0.15 * battleFieldSize * battleFieldSize);
            int maxMines = Convert.ToInt32(0.3 * battleFieldSize * battleFieldSize);
            int numberOfMines = randomGenerator.Next(minMines, maxMines + 1);
            for (int i = 0; i < numberOfMines; i++)
            {
                int row = randomGenerator.Next(0, battleFieldSize);
                int col = randomGenerator.Next(0, battleFieldSize);
                if (battleField[row, col] == "-")
                {
                    battleField[row, col] = randomGenerator.Next(1, 6).ToString();
                }
                else
                {
                    numberOfMines--;
                }
            }
        }

        private static void ProcessMineDetonation(int row, int col)
        {
            int mineSize = int.Parse(battleField[row, col]);
            MarkDetonatedCell(row, col);

            if (mineSize >= 1)
            {
                MarkDetonatedCell(row - 1, col - 1);
                MarkDetonatedCell(row - 1, col + 1);
                MarkDetonatedCell(row + 1, col - 1);
                MarkDetonatedCell(row + 1, col + 1);
            }

            if (mineSize >= 2)
            {
                MarkDetonatedCell(row - 1, col);
                MarkDetonatedCell(row, col - 1);
                MarkDetonatedCell(row + 1, col);
                MarkDetonatedCell(row, col + 1);
            }

            if (mineSize >= 3)
            {
                MarkDetonatedCell(row - 2, col);
                MarkDetonatedCell(row, col - 2);
                MarkDetonatedCell(row + 2, col);
                MarkDetonatedCell(row, col + 2);
            }

            if (mineSize >= 4)
            {
                MarkDetonatedCell(row - 2, col - 1);
                MarkDetonatedCell(row - 2, col + 1);
                MarkDetonatedCell(row - 1, col - 2);
                MarkDetonatedCell(row - 1, col + 2);
                MarkDetonatedCell(row + 2, col - 1);
                MarkDetonatedCell(row + 2, col + 1);
                MarkDetonatedCell(row + 1, col - 2);
                MarkDetonatedCell(row + 1, col + 2);
            }

            if (mineSize >= 5)
            {
                MarkDetonatedCell(row - 2, col - 2);
                MarkDetonatedCell(row - 2, col + 2);
                MarkDetonatedCell(row + 2, col - 2);
                MarkDetonatedCell(row + 2, col + 2);
            }
        }

        private static void MarkDetonatedCell(int row, int col)
        {
            if (row >= 0 && 
                row < battleField.GetLength(0) && 
                col >= 0 &&
                col < battleField.GetLength(1))
            {
                battleField[row, col] = "X";
            }
        }

        private static int[] ProcessUserInput()
        {
            string input = Console.ReadLine();
            if (input == null)
            {
                throw new ArgumentException("You must enter coordinates.");
            }

            int row;
            int col;
            string[] inputCoordinates = Regex.Split(input, "\\s+");
            if (inputCoordinates.Length != 2 ||
                !int.TryParse(inputCoordinates[0], out row) ||
                !int.TryParse(inputCoordinates[1], out col))
            {
                throw new ArgumentException("You must enter at least two integer coordinates.");
            }

            if (row >= battleFieldSize || col >= battleFieldSize || row < 0 || col < 0)
            {
                throw new ArgumentException("The coordinate must be in the range of the field.");
            }

            return new[] { row, col };
        }

        private static int CountDetonatedFields()
        {
            int count = 0;
            for (int row = 0; row < battleFieldSize; row++)
            {
                for (int col = 0; col < battleFieldSize; col++)
                {
                    if (battleField[row, col] == "-" || battleField[row, col] == "X")
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
