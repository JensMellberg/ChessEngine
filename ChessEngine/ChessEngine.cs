using ChessPlayerDLL;
using ChessPlayerDLL.Exceptions;
using System;
using System.Linq;
using System.Threading;

namespace ChessEngine
{
    public class ChessEngine
    {
        ChessBoard currentBoard;

        public IChessPlayer Player1 { get; set; }

        public IChessPlayer Player2 { get; set; }

        private Action<ChessBoard> boardChangedCallback;

        private int playerTurn;

        public ChessEngine()
        {
        }

        public void StartBotGame(string firstPath, string secondPath, Action<ChessBoard> boardChangedCallback)
        {
            this.currentBoard = new ChessBoard();
            this.currentBoard.SetupBoard();
            var white = new Random().Next(1, 3);
            this.playerTurn = 1;
            this.Player1 = Loader.LoadFromFile(white == 1 ? firstPath : secondPath);
            this.Player2 = Loader.LoadFromFile(white == 1 ? secondPath : firstPath);
            var p1Difficulty = this.Player1.GetDifficulties().First();
            var p2Difficulty = this.Player2.GetDifficulties().First();
            this.Player1.Initialize(true, p1Difficulty.Item1, 500);
            this.Player2.Initialize(false, p2Difficulty.Item1, 500);
            this.boardChangedCallback = boardChangedCallback;
            this.MakeBotMove(Player1);
        }

        public void OnMoveMade(Move move)
        {
            this.currentBoard.DoMove(move);
            this.boardChangedCallback(this.currentBoard);
            this.ChangeTurn();
            this.CurrentPlayer.MoveMade(move);
            this.MakeBotMove(this.CurrentPlayer);
        }

        public void MakeBotMove(IChessPlayer player)
        {
            var thread = new Thread(() =>
            {
                var move = player.GetMove();
                Thread.Sleep(1500);
                this.OnMoveMade(move);
            });

            thread.Start();
        }

        private IChessPlayer CurrentPlayer => this.playerTurn == 1 ? this.Player1 : this.Player2;

        private void ChangeTurn()
        {
            this.playerTurn = this.playerTurn == 1 ? 2 : 1;
        }
    }
}