using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{    
    [TestClass]
    public class OnPlayingTheGame : GameTests
    {
        
         [TestMethod]
        public void The_Player_MayMoveAround_TheBoard()
        {    

            var currentPlayerPosition = TheGame.Player.Position.ToString();

            TheGame.MovePlayer(Direction.Right);

            var newPlayerPosition = TheGame.Player.Position.ToString();

            Assert.AreNotEqual(currentPlayerPosition, newPlayerPosition);

        }

        [TestMethod]
        public void Attempting_AnInvalidMove_WillBeIgnored()
        {

            var currentPlayerPosition = TheGame.Player.Position;

            Assert.IsTrue(currentPlayerPosition.IsAt(TheGame.Board.StartPosition()));

            // illegal move when at A1
            TheGame.MovePlayer(Direction.Down);

            var newPlayerPosition = TheGame.Player.Position;

            Assert.AreSame(currentPlayerPosition, newPlayerPosition);

        }         

    }
}