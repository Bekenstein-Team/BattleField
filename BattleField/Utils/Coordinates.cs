namespace BattleField.Utils
{
    public struct Coordinates
    {
        public Coordinates(int row, int col) : this()
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; private set; }

        public int Col { get; private set; }
    }
}
