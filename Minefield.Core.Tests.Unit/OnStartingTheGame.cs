using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class OnStartingTheGame : GameTests
{

        [TestMethod]
        public void A_Board_Exists_Of_8x8()
        {            
            Assert.AreEqual(8, _theGame.Board.Rows);
            Assert.AreEqual(8, _theGame.Board.Columns);
        }

        [TestMethod]
        public void MinesHave_Been_HiddenOnTheBoard()
        {
            foreach (var mine in _theGame.Board.Mines)
            {
                Assert.IsTrue(mine.IsHidden());
            }

        }


        [TestMethod]
        public void A_PlayerExists()
        {

            Assert.IsNotNull(_theGame.Player);            
        }

        [TestMethod]
        public void The_Player_IsLocatedAtTheStartPosition_OnTheBoard()
        {
            Assert.IsTrue(_theGame.Player.IsAt( _theGame.Board.StartPosition()));

        }

       
        [TestMethod]
        public void The_Player_HasThreeLives() {

            Assert.AreEqual(3, _theGame.Player.Lives);

        }

        [TestMethod]
        public void The_Player_HasAScoreOf_Zero() {

            Assert.AreEqual(0, _theGame.Player.Score);

        }


        [TestMethod]
        public void The_GameIsReady()
        {

            Assert.AreEqual(GameState.Ready, _theGame.State);

        }


    }

}
