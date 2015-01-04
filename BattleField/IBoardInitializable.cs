namespace BattleField
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IBoardInitializable
    {
        string[,] InitializeBoard(int size, string emptyFieldSymbol);
    }
}
