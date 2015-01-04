namespace BattleField
{
    public interface IBoardInitializable
    {
        string[,] InitializeBoard(int size, string emptyFieldSymbol);
    }
}
