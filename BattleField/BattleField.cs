namespace BattleField
{
    using System;
    using System.Text;

    public class BattleField 
    {
        private const int MIN_BATTLEFIELD_SIZE = 2;
        private const int MAX_BATTLEFIELD_SIZE = 10;
        private const string EMPTY_FIELD_SYMBOL = "-";
        private const string DETONATED_MINE_SYMBOL = "X";

        private int _size;

        public BattleField(int size, IBoardInitializable boardInitializer)
        {
            this.Size = size;
            this.Board = boardInitializer.InitializeBoard(this.Size, EMPTY_FIELD_SYMBOL);
            this.DetonatedMinesCount = 0;
        }

        public int Size
        {
            get
            {
                return this._size;
            }

            set
            {
                if (value < MIN_BATTLEFIELD_SIZE || value > MAX_BATTLEFIELD_SIZE)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The battlefield size must be between 1 and 10.");
                }

                this._size = value;
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
                        if (this.Board[row, col] != EMPTY_FIELD_SYMBOL && this.Board[row, col] != DETONATED_MINE_SYMBOL)
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

            if (this.Board[row, col] == EMPTY_FIELD_SYMBOL || this.Board[row, col] == DETONATED_MINE_SYMBOL)
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
            if (row >= 0 && row < this.Size && col >= 0 && col < this.Size)
            {
                this.Board[row, col] = DETONATED_MINE_SYMBOL;
            }
        }
    }
}
