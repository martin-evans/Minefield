using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class OnStartingTheGame
    {
        private Game _theGame;

        [TestInitialize]
        public void SetUp(){
            
            _theGame = new Core.Game();
        }

        [TestMethod]
        public void A_Board_Exists_Of_8x8()
        {            
            Assert.AreEqual(8, _theGame.Board.Rows);
            Assert.AreEqual(8, _theGame.Board.Columns);
        }


        [TestMethod]
        public void A_PlayerExists()
        {

            Assert.IsNotNull(_theGame.Player);            
        }

        [TestMethod]
        public void The_Player_IsLocatedAtTheStartPosition_OnTheBoard()
        {
            Assert.IsTrue(_theGame.Player.Position == _theGame.Board.StartPosition());

        }

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

        [TestMethod]
        public void The_Player_HasThreeLives() {

            Assert.AreEqual(3, _theGame.Player.Lives);

        }

        /* Game starts with score of 0 (AKA "Moves Taken") */

        /* Each time a player moves the score is incremented */        

        /* Player getting to the end of the board means the player wins */

        /* Player hitting a mine detonates the mine */          
        
        /* Player hitting a mine looses a life */

        /* Player looses all lives leads to Game Over */

        /* Game Over allows player to restart */

    }
    
}