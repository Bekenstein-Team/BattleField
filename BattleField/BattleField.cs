namespace BattleField
{
    using System;
    using System.Text;

    public class BattleField
    {
        private static readonly Random RandomGenerator = new Random();
        private int size;

        public BattleField(int size)
        {
            this.Size = size;
            this.DetonatedMinesCount = 0;
            this.InitializeBoard();
        }

        public int Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The battlefield size must be between 1 and 10.");
                }

                this.size = value;
            }
        }

        public bool AllMinesAreDetonated
        {
            get
            {
                for (int row = 0; row < this.Size; row++)
                {
                    for (int col = 0; col < this.Size; col++)
                    {
                        if (this.Board[row, col] != "-" && this.Board[row, col] != "X")
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public string[,] Board { get; private set; }

        public int DetonatedMinesCount { get; private set; }

        public void ProccessMove(int row, int col)
        {
            if (row >= this.Size || row < 0)
            {
                throw new ArgumentOutOfRangeException("row", row, "Ivalid value for row. The coordinates must be within the board.");
            }

            if (col >= this.Size || col < 0)
            {
                throw new ArgumentOutOfRangeException("col", col, "Ivalid value for column. The coordinates must be within the board.");
            }

            if (this.Board[row, col] == "-" || this.Board[row, col] == "X")
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

        private void InitializeBoard()
        {
            this.Board = new string[this.Size, this.Size];

            // filling the board with dashes
            for (int row = 0; row < this.Size; row++)
            {
                for (int col = 0; col < this.Size; col++)
                {
                    this.Board[row, col] = "-";
                }
            }

            // setting the mines
            int minMines = Convert.ToInt32(0.15 * this.Size * this.Size);
            int maxMines = Convert.ToInt32(0.3 * this.Size * this.Size);
            int numberOfMines = RandomGenerator.Next(minMines, maxMines + 1);
            for (int i = 0; i < numberOfMines; i++)
            {
                int row = RandomGenerator.Next(0, this.Size);
                int col = RandomGenerator.Next(0, this.Size);
                if (this.Board[row, col] == "-")
                {
                    this.Board[row, col] = RandomGenerator.Next(1, 6).ToString();
                }
                else
                {
                    numberOfMines--;
                }
            }
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
                this.Board[row, col] = "X";
            }
        }
    }
}
