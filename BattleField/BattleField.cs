namespace BattleFiled
{
    using System;

    public class BattleField
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to \"Battle Field\" game.");
            Console.Write("Enter the size of the battle field between 1 and 10: ");
            int battleFieldSize;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out battleFieldSize) ||
                    battleFieldSize > 10 ||
                    battleFieldSize < 1)
            {
                Console.Write("Please enter an integer between 1 and 10: ");
                input = Console.ReadLine();
            }

            string[,] battleField = InitializeField(battleFieldSize);

            Console.WriteLine();
            PrintField(battleField);
            Console.WriteLine();
            int moveCounter = 0;

            // main loop
            while (true)
            {
                // reading user input
                Console.Write("Please enter coordinates: ");
                string[] inputCoordinates = Console.ReadLine().Split(new [] {' '});
                int row = int.Parse(inputCoordinates[0]);
                int col = int.Parse(inputCoordinates[1]);

                // checking the field
                if (battleField[row, col] == "-" ||
                    battleField[row, col] == "X")
                {
                    Console.WriteLine("Invalid move!");
                }
                else
                {
                    battleField = HodNaIgracha(row, col, battleFieldSize, battleField);
                    moveCounter++;
                }
                
                PrintField(battleField);
                int count = 0;
                bool gameEnded = false;
                for (int rowCheck = 0; rowCheck < battleFieldSize; rowCheck++)
                {
                    for (int colCheck = 0; colCheck < battleFieldSize; colCheck++)
                    {
                        if (battleField[rowCheck, colCheck] == "-" ||
                            battleField[rowCheck, colCheck] == "X")
                        {
                            count++;
                        }

                        if (count == battleFieldSize * battleFieldSize)
                        {
                            gameEnded = true; 
                        }
                    }
                }
                
                if (gameEnded)
                {
                    PrintField(battleField);
                    Console.WriteLine("Game over!");
                    Console.WriteLine("Detonated mines {0}", moveCounter);
                    break;
                }
            }
        }

        private static string[,] InitializeField(int battleFieldSize)
        {
            //tuka si pravq poleto
            string[,] battleField = new string[battleFieldSize, battleFieldSize];
            Random randomPosition = new Random();

            //celta na tova e da se zapylni matricata default s cherti
            for (int row = 0; row < battleFieldSize; row++)
            {
                for (int col = 0; col < battleFieldSize; col++)
                {
                    battleField[row, col] = "-";
                }
            }

            string[] minesArray = { "1", "2", "3", "4", "5" };
            double fifteenPercentNSquared = 0.15 * battleFieldSize * battleFieldSize;
            double thirtyPercenNSquared = 0.3 * battleFieldSize * battleFieldSize;
            int fifteenPercent = Convert.ToInt16(fifteenPercentNSquared);
            int thirtyPercent = Convert.ToInt16(thirtyPercenNSquared);
            int numberOfMines = randomPosition.Next(fifteenPercent, thirtyPercent + 1);

            for (int i = 0; i < numberOfMines; i++)
            {
                int newRow = randomPosition.Next(0, battleFieldSize);
                int newCol = randomPosition.Next(0, battleFieldSize);
                if (battleField[newRow, newCol] == "-")
                {
                    battleField[newRow, newCol] = minesArray[randomPosition.Next(0, 5)];
                }
                else
                {
                    numberOfMines--;
                }
            }

            return battleField;
        }

        private static string[,] HodNaIgracha(int row, int col, int n, string[,] battleField)
        {
            if (Convert.ToInt16(battleField[row, col]) >= 1)
            {
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    battleField[row - 1, col - 1] = "X";
                }

                if (row - 1 >= 0 && col < n - 1)
                {
                    battleField[row - 1, col + 1] = "X";
                }

                if (row < n - 1 && col - 1 > 0)
                {
                    battleField[row + 1, col - 1] = "X";
                }

                if (row < n - 1 && col < n - 1)
                {
                    battleField[row + 1, col + 1] = "X";
                }

                if (Convert.ToInt16(battleField[row, col]) >= 2)
                {
                    if (row - 1 >= 0)
                    {
                        battleField[row - 1, col] = "X";
                    }

                    if (col - 1 >= 0)
                    {
                        battleField[row, col - 1] = "X";
                    }

                    if (col < n - 1)
                    {
                        battleField[row, col + 1] = "X";
                    }

                    if (row < n - 1)
                    {
                        battleField[row + 1, col] = "X";
                    }

                    if (Convert.ToInt16(battleField[row, col]) >= 3)
                    {
                        if (row - 2 >= 0)
                        {
                            battleField[row - 2, col] = "X";
                        }

                        if (col - 2 >= 0)
                        {
                            battleField[row, col - 2] = "X";
                        }

                        if (col < n - 2)
                        {
                            battleField[row, col + 2] = "X";
                        }

                        if (row < n - 2)
                        {
                            battleField[row + 2, col] = "X";
                        }

                        if (Convert.ToInt16(battleField[row, col]) >= 4)
                        {
                            if (row - 2 >= 0 && col - 1 >= 0)
                            {
                                battleField[row - 2, col - 1] = "X";
                            }

                            if (row - 2 >= 0 && col < n - 1)
                            {
                                battleField[row - 2, col + 1] = "X";
                            }

                            if (row - 1 >= 0 && col - 2 >= 0)
                            {
                                battleField[row - 1, col - 2] = "X";
                            }

                            if (row - 1 >= 0 && col < n - 2)
                            {
                                battleField[row - 1, col + 2] = "X";
                            }

                            if (row < n - 1 && col - 2 >= 0)
                            {
                                battleField[row + 1, col - 2] = "X";
                            }

                            if (row < n - 1 && col < n - 2)
                            {
                                battleField[row + 1, col + 2] = "X";
                            }

                            if (row < n - 2 && col - 1 > 0)
                            {
                                battleField[row + 2, col - 1] = "X";
                            }

                            if (row < n - 2 && col < n - 1)
                            {
                                battleField[row + 2, col + 1] = "X";
                            }

                            if (Convert.ToInt16(battleField[row, col]) == 5)
                            {
                                if (row - 2 >= 0 && col - 2 >= 0)
                                {
                                    battleField[row - 2, col - 2] = "X";
                                }

                                if (row - 2 >= 0 && col < n - 2)
                                {
                                    battleField[row - 2, col + 2] = "X";
                                }

                                if (row < n - 2 && col - 2 > 0)
                                {
                                    battleField[row + 2, col - 2] = "X";
                                }

                                if (row < n - 2 && col < n - 2)
                                {
                                    battleField[row + 2, col + 2] = "X";
                                }
                            }
                        }
                    }
                }
            }

            battleField[row, col] = "X";
            return battleField;
        }

        private static void PrintField(string[,] battleField)
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
    }
}
