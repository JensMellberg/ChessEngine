using ChessPlayerDLL;
using ChessPlayerDLL.Exceptions;
using System;
using System.Linq;
using System.Threading;

namespace ChessEngine
{
    public class ChessGame
    {
        ChessBoard CurrentBoard { get; set; }

        public IPlayer Player1 { get; set; }

        public IPlayer Player2 { get; set; }

        private Action<ChessBoard, Move> boardChangedCallback;

        private int playerTurn;

        public ChessGame(IPlayer player1, IPlayer player2, Action<ChessBoard, Move> boardChangedCallback, int timePerMove)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this.boardChangedCallback = boardChangedCallback;
            this.playerTurn = 1;
        }

        public void Start()
        {
            this.CurrentBoard = new ChessBoard();
            this.CurrentBoard.SetupBoard();
            this.Player1.Initialize(true);
            this.Player2.Initialize(false);
            this.GetFollowUpMove(null, this.Player1);
        }

        public void OnMoveMade(Move move)
        {
            this.CurrentBoard.DoMove(move);
            this.boardChangedCallback(this.CurrentBoard, move);
            this.ChangeTurn();
            this.GetFollowUpMove(move, this.CurrentPlayer);
        }

        public void GetFollowUpMove(Move move, IPlayer player)
        {
            if (player is ChessBot botPlayer)
            {
                if (move != null)
                {
                    botPlayer.Player.MoveMade(move);
                }

                this.MakeBotMove(botPlayer.Player);
            }
            else if (player is HumanPlayer humanPlayer)
            {
                if (move != null)
                {
                    //TODO
                }

                //TODO
            }
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

        private IPlayer CurrentPlayer => this.playerTurn == 1 ? this.Player1 : this.Player2;

        private void ChangeTurn()
        {
            this.playerTurn = this.playerTurn == 1 ? 2 : 1;
        }
    }
}