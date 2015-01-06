namespace BattleField
{
    using System;

    public class BoardInitializer : IBoardInitializable
    {
        private readonly Random randomGenerator = new Random();
        private const double MinNumberOfMinesMultiplier = 0.15;
        private const double MaxNumberOfMinesMultiplier = 0.3;

        public string[,] InitializeBoard(int size, string emptyFieldSymbol)
        {
            var board = new string[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    board[row, col] = emptyFieldSymbol;
                }
            }

            int minMines = Convert.ToInt32(MinNumberOfMinesMultiplier * size * size);
            int maxMines = Convert.ToInt32(MaxNumberOfMinesMultiplier * size * size);
            int numberOfMines = this.randomGenerator.Next(minMines, maxMines + 1);
            for (int i = 0; i < numberOfMines; i++)
            {
                int row = this.randomGenerator.Next(0, size);
                int col = this.randomGenerator.Next(0, size);
                if (board[row, col] == emptyFieldSymbol)
                {
                    board[row, col] = this.randomGenerator.Next(1, 6).ToString();
                }
                else
                {
                    numberOfMines--;
                }
            }

            return board;
        }
    }
}
