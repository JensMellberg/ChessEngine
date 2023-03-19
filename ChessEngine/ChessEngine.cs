using ChessPlayerDLL;
using ChessPlayerDLL.Exceptions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace ChessEngine
{
    public class ChessEngine
    {

        public ChessEngine()
        {
        }

        public IChessPlayer CreateChessPlayer(string path) => Loader.LoadFromFile(path);

        public IPlayer CreateBotPlayerRepresentation(IChessPlayer bot, int difficulty) => new ChessBot(bot, difficulty, 30);

        public IPlayer CreateHumanPlayerRepresentation() => new HumanPlayer();

        public ChessGame CreateGame(IPlayer player1, IPlayer player2, Action<ChessBoard, Move> boardChangedCallback, int whitePlayer = -1)
        {
            if (whitePlayer == -1)
            {
                whitePlayer = new Random().Next(1, 3);
            }

            var game = new ChessGame(
                whitePlayer == 1 ? player1 : player2,
                whitePlayer == 1 ? player2 : player1,
                boardChangedCallback,
                30);
            return game;
        }
    }
}