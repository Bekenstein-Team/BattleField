namespace BattleField.Tests.Mock
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BoardInitialezerMock : IBoardInitializable
    {
        public string[,] InitializeBoard(int size, string emptyFieldSymbol)
        {
            return new string[10, 10]
            {
                { "-", "5", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "3", "-", "2", "-", "-", "4", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "3", "-", "5", "-", "4" },
                { "-", "4", "-", "-", "-", "-", "2", "-", "2", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "-", "-", "3", "-" },
                { "-", "-", "-", "-", "-", "-", "2", "-", "1", "-" },
                { "-", "-", "-", "-", "-", "2", "-", "4", "-", "-" },
                { "-", "-", "-", "-", "-", "-", "3", "-", "2", "5" },
            };
        }
    }
}
