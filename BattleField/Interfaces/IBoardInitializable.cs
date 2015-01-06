namespace BattleField.Interfaces
{
    public interface IBoardInitializable
    {
        string[,] InitializeBoard(int size, string emptyFieldSymbol);
    }
}
