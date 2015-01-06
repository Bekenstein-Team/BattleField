namespace BattleField.Models
{
    using System;
    using System.Text;
    using Interfaces;

    public class BattleField 
    {
        public const int MinBoardSize = 2;
        public const int MaxBoardSize = 10;
        public const string EmptyFieldSymbol = "-";
        public const string DetonatedMineSymbol = "X";

        private int boardSize;

        public BattleField(int boardSize, IBoardInitializable boardInitializer)
        {
            this.BoardSize = boardSize;
            this.Board = boardInitializer.InitializeBoard(this.BoardSize, BattleField.EmptyFieldSymbol);
            this.DetonatedMinesCount = 0;
        }

        public int BoardSize
        {
            get
            {
                return this.boardSize;
            }

            private set
            {
                if (value < BattleField.MinBoardSize || value > BattleField.MaxBoardSize)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The board size must be between 2 and 10.");
                }

                this.boardSize = value;
            }
        }

        public int RemainingMines
        {
            get
            {
                int mines = 0;
                for (int row = 0; row < this.BoardSize; row++)
                {
                    for (int col = 0; col < this.BoardSize; col++)
                    {
                        if (this.Board[row, col] != BattleField.EmptyFieldSymbol && 
                            this.Board[row, col] != BattleField.DetonatedMineSymbol)
                        {
                            mines++;
                        }
                    }
                }

                return mines;
            }
        }

        public string[,] Board { get; private set; }

        public int DetonatedMinesCount { get; private set; }

        public bool CoordinatesAreValid(int row, int col)
        {
            bool rowIsValid = row >= 0 && row < this.BoardSize;
            bool colIsValid = col >= 0 && col < this.BoardSize;
            bool result = rowIsValid &&
                colIsValid &&
                this.Board[row, col] != BattleField.EmptyFieldSymbol &&
                this.Board[row, col] != BattleField.DetonatedMineSymbol;

            return result;
        }
            
        public void ProccessMove(int row, int col)
        {
            if (row >= this.BoardSize || row < 0)
            {
                throw new ArgumentOutOfRangeException("row", row, "Invalid value for row. The coordinates must be within the board.");
            }

            if (col >= this.BoardSize || col < 0)
            {
                throw new ArgumentOutOfRangeException("col", col, "Invalid value for column. The coordinates must be within the board.");
            }

            if (this.Board[row, col] == EmptyFieldSymbol || this.Board[row, col] == DetonatedMineSymbol)
            {
                throw new ArgumentException("There is no mine on that field.");
            }

            this.ProcessMineDetonation(row, col);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("   0  ");
            for (int i = 1; i < this.BoardSize; i++)
            {
                result.AppendFormat("{0}  ", i);
            }

            result.AppendLine();
            result.Append("   -");
            for (int i = 0; i < this.BoardSize; i++)
            {
                result.Append("---");
            }

            result.AppendLine();
            for (int i = 0; i < this.BoardSize; i++)
            {
                result.AppendFormat("{0}|", i);
                for (int j = 0; j < this.BoardSize; j++)
                {
                    result.AppendFormat(" {0} ", this.Board[i, j]);
                }

                result.AppendLine();
            }

            return result.ToString();
        }

        private void ProcessMineDetonation(int row, int col)
        {
            this.DetonatedMinesCount++;
            int mineSize = int.Parse(this.Board[row, col]);
            this.MarkDetonatedCell(row, col);

            if (mineSize >= 1)
            {
                this.MarkDetonatedCell(row - 1, col - 1);
                this.MarkDetonatedCell(row - 1, col + 1);
                this.MarkDetonatedCell(row + 1, col - 1);
                this.MarkDetonatedCell(row + 1, col + 1);
            }

            if (mineSize >= 2)
            {
                this.MarkDetonatedCell(row - 1, col);
                this.MarkDetonatedCell(row, col - 1);
                this.MarkDetonatedCell(row + 1, col);
                this.MarkDetonatedCell(row, col + 1);
            }

            if (mineSize >= 3)
            {
                this.MarkDetonatedCell(row - 2, col);
                this.MarkDetonatedCell(row, col - 2);
                this.MarkDetonatedCell(row + 2, col);
                this.MarkDetonatedCell(row, col + 2);
            }

            if (mineSize >= 4)
            {
                this.MarkDetonatedCell(row - 2, col - 1);
                this.MarkDetonatedCell(row - 2, col + 1);
                this.MarkDetonatedCell(row - 1, col - 2);
                this.MarkDetonatedCell(row - 1, col + 2);
                this.MarkDetonatedCell(row + 2, col - 1);
                this.MarkDetonatedCell(row + 2, col + 1);
                this.MarkDetonatedCell(row + 1, col - 2);
                this.MarkDetonatedCell(row + 1, col + 2);
            }

            if (mineSize == 5)
            {
                this.MarkDetonatedCell(row - 2, col - 2);
                this.MarkDetonatedCell(row - 2, col + 2);
                this.MarkDetonatedCell(row + 2, col - 2);
                this.MarkDetonatedCell(row + 2, col + 2);
            }
        }

        private void MarkDetonatedCell(int row, int col)
        {
            if (row >= 0 && row < this.BoardSize && col >= 0 && col < this.BoardSize)
            {
                this.Board[row, col] = BattleField.DetonatedMineSymbol;
            }
        }
    }
}
