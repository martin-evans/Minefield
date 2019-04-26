using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class Game
    {

        [TestMethod]
        public void On_Starting_The_Game_A_Board_Exists_Of_8x8()
        {
            var subject = new Core.Game();

            Assert.AreEqual(8, subject.Board.Rows);
            Assert.AreEqual(8, subject.Board.Columns);
        }


        [TestMethod]
        public void On_Starting_The_Game_A_PlayerExists()
        {
            var subject = new Core.Game();

            Assert.IsNotNull(subject.Player);            
        }

        [TestMethod]
        public void On_Starting_The_Game_The_PlayerIsLocatedAtTheStartPosition_OnTheBoard()
        {
            var subject = new Core.Game();

            Assert.IsTrue(subject.Player.Position == subject.Board.Position("A","1"));

        }

    }
    
}