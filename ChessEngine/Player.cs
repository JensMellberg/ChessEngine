using ChessPlayerDLL;
using ChessPlayerDLL.Exceptions;
using System;
using System.Linq;
using System.Threading;

namespace ChessEngine
{
    public interface IPlayer
    {
        public void Initialize(bool isWhite);
    }
}