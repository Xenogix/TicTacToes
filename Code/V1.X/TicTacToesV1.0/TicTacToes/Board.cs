using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToes
{
    public class Board
    {
        public readonly int height;
        public readonly int width;

        public List<Token>[] playerTokens { get; private set; }
        public Token[,] boardTokens { get; private set; }

        public Board(int height, int width)
        {
            this.height = height;
            this.width = width;

            boardTokens = new Token[width,height];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    boardTokens[x, y] = new Token(Properties.Resources.voidSpot);

            playerTokens = new List<Token>[width];

            for (int x = 0; x < width; x++)
                playerTokens[x] = new List<Token>();
        }

        public bool AddToken(int range, Token t)
        {
            if (range < width)
            {
                if (playerTokens[range].Count < height)
                {
                    playerTokens[range].Add(t);
                    return true;
                }
            }

            return false;
        }
    }
}
