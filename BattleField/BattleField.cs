namespace BattleField
{
    using System;
    using System.Text;

    public class BattleField 
    {
        private const int MinBattleFieldSize = 2;
        private const int MaxBattleFieldSize = 10;
        private const string EmptyFieldSymbol = "-";
        private const string DetonatedMineSymbol = "X";

        private int size;

        public BattleField(int size, IBoardInitializable boardInitializer)
        {
            this.Size = size;
            this.Board = boardInitializer.InitializeBoard(this.Size, EmptyFieldSymbol);
            this.DetonatedMinesCount = 0;
        }

        public int Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (value < MinBattleFieldSize || 
                    value > MaxBattleFieldSize)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The battlefield size must be between 1 and 10.");
                }

                this.size = value;
            }
        }

        public int RemainingMines
        {
            get
            {
                int mines = 0;
                for (int row = 0; row < this.Size; row++)
                {
                    for (int col = 0; col < this.Size; col++)
                    {
                        if (this.Board[row, col] != EmptyFieldSymbol && 
                            this.Board[row, col] != DetonatedMineSymbol)
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

        public void ProccessMove(int row, int col)
        {
            if (row >= this.Size || row < 0)
            {
                throw new ArgumentOutOfRangeException("row", row, "Invalid value for row. The coordinates must be within the board.");
            }

            if (col >= this.Size || col < 0)
            {
                throw new ArgumentOutOfRangeException("col", col, "Invalid value for column. The coordinates must be within the board.");
            }

            if (this.Board[row, col] == EmptyFieldSymbol || 
                this.Board[row, col] == DetonatedMineSymbol)
            {
                throw new ArgumentException("There is no mine on that field.");
            }

            this.ProcessMineDetonation(row, col);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("   0  ");
            for (int i = 1; i < this.Size; i++)
            {
                result.AppendFormat("{0}  ", i);
            }

            result.AppendLine();
            result.Append("   -");
            for (int i = 0; i < this.Size; i++)
            {
                result.Append("---");
            }

            result.AppendLine();
            for (int i = 0; i < this.Size; i++)
            {
                result.AppendFormat("{0}|", i);
                for (int j = 0; j < this.Size; j++)
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

            if (mineSize >= 5)
            {
                this.MarkDetonatedCell(row - 2, col - 2);
                this.MarkDetonatedCell(row - 2, col + 2);
                this.MarkDetonatedCell(row + 2, col - 2);
                this.MarkDetonatedCell(row + 2, col + 2);
            }
        }

        private void MarkDetonatedCell(int row, int col)
        {
            if (row >= 0 && row < this.Size && col >= 0 && col < this.Size)
            {
                this.Board[row, col] = DetonatedMineSymbol;
            }
        }
    }
}
