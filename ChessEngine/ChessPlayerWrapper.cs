using ChessPlayerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    internal class ChessPlayerWrapper : IChessPlayer
    {
        public bool IsChessPlayer => true;

        public bool IsValid { get; set; }

        private object instance;

        private Type type;

        public ChessPlayerWrapper(Type type)
        {
            this.instance = 0;
            this.type = type;
            if (this.Validate(type))
            {
                this.IsValid = true;
            }
        }

        private bool Validate(Type type)
        {
            var methods = type.GetMethods();
            //TODO validate
            var instance = Activator.CreateInstance(type);
            if (instance == null)
            {
               return false;
            }

            this.instance = instance;
            return true;
        }

        public IEnumerable<(int, string)> GetDifficulties()
        {
            var difficulties = type.InvokeMember("GetDifficulties", BindingFlags.InvokeMethod, null, this.instance, null);
            return difficulties != null ? (IEnumerable<(int, string)>)difficulties : Enumerable.Empty<(int, string)>();
        }

        public Move GetMove()
        {
            var move = type.InvokeMember("GetMove", BindingFlags.InvokeMethod, null, this.instance, null);
            return move != null ? (Move)move : throw new Exception("Failed to retreive move.");
        }

        public void MoveMade(Move move)
        {
            type.InvokeMember("MoveMade", BindingFlags.InvokeMethod, null, this.instance, new[] { move });
        }

        public void Initialize(bool playAsWhite, int difficulty, int timePerMove)
        {
            type.InvokeMember("Initialize", BindingFlags.InvokeMethod, null, this.instance, new object [] { playAsWhite, difficulty, timePerMove });
        }
    }
}
