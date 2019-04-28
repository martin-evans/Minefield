using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{    
    [TestClass]
    public class OnPlayingTheGame : GameTests
    {
        
         [TestMethod]
        public void The_Player_MayMovePosition_OnTheBoard()
        {    

            var currentPlayerPosition = _theGame.Player.Position;

            _theGame.MovePlayer(Direction.Right);

            var newPlayerPosition = _theGame.Player.Position;

            Assert.AreNotEqual(currentPlayerPosition, newPlayerPosition);

        }

        [TestMethod]
        public void Attempting_AnInvalidMove_WillBeIgnored()
        {

            var currentPlayerPosition = _theGame.Player.Position;

            Assert.IsTrue(currentPlayerPosition == _theGame.Board.StartPosition());

            // illegal move when at A1
            _theGame.MovePlayer(Direction.Down);

            var newPlayerPosition = _theGame.Player.Position;

            Assert.AreSame(currentPlayerPosition, newPlayerPosition);

        }         

    }


    
}