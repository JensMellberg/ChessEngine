using ChessEngine;

namespace ChessEngineTests
{
    public class ChessEngineTests
    {
        [Fact]
        public void CanLoadFile()
        {
            var player = Loader.LoadFromFile(@"C:\Users\Camilla\source\repos\TestChessPlayer\TestChessPlayer\bin\Debug\net6.0\TestChessPlayer.dll");
            var difficulties = player.GetDifficulties().First();
            Assert.Equal(1, difficulties.Item1);
            Assert.Equal("easy", difficulties.Item2);
        }
    }
}