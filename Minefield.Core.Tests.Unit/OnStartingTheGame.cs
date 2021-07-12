using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class OnStartingTheGame : GameTests
{

        [TestMethod]
        public void A_Board_Exists_Of_8x8()
        {            
            Assert.AreEqual(8, TheGame.Board.Rows);
            Assert.AreEqual(8, TheGame.Board.Columns);
        }

        [TestMethod]
        public void MinesHave_Been_HiddenOnTheBoard()
        {
            TheGame = new Game(GameSettings.Default());

            foreach (var mine in TheGame.Board.Mines)
            {
                Assert.IsTrue(mine.IsHidden());
            }

        }


        [TestMethod]
        public void A_PlayerExists()
        {

            Assert.IsNotNull(TheGame.Player);            
        }

        [TestMethod]
        public void The_Player_IsLocatedAtTheStartPosition_OnTheBoard()
        {
            Assert.IsTrue(TheGame.Player.IsAt( TheGame.Board.StartPosition()));

        }

       
        [TestMethod]
        public void The_Player_HasThreeLives() {

            Assert.AreEqual(3, TheGame.Player.Lives);

        }

        [TestMethod]
        public void The_Player_HasAScoreOf_Zero() {

            Assert.AreEqual(0, TheGame.Player.Score);

        }


        [TestMethod]
        public void The_GameIsReady()
        {

            Assert.AreEqual(GameState.Ready, TheGame.State);

        }


    }

}
