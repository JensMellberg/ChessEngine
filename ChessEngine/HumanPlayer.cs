using ChessPlayerDLL;
using ChessPlayerDLL.Exceptions;
using System;
using System.Linq;
using System.Threading;

namespace ChessEngine
{
    public class HumanPlayer : IPlayer
    {
       public bool IsWhite { get; set; }
       public HumanPlayer()
       {
       }

        public void Initialize(bool isWhite)
        {
            this.IsWhite = isWhite;
        }
    }
}