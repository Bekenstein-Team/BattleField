﻿namespace BattleField
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
                Console.Write("Please enter coordinates with space between: ");
                string input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("You must enter coordinates.");
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
