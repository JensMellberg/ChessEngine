using ChessPlayerDLL;
using ChessPlayerDLL.Exceptions;
using System;
using System.Linq;
using System.Threading;

namespace ChessEngine
{
    public class ChessBot : IPlayer
    {
       public IChessPlayer Player { get; set; }

        private int difficulty;

        private int timePerMove;
       public ChessBot(IChessPlayer player, int difficulty, int timePerMove)
       {
            this.Player = player;
            this.difficulty = difficulty;
            this.timePerMove = timePerMove;
       }

        public void Initialize(bool isWhite)
        {
            this.Player.Initialize(isWhite, this.difficulty, this.timePerMove);
        }
    }
}