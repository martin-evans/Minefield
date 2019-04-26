using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class BoardTests{

        [TestMethod]
        public void BoardConstructed_With_8x8_Grid(){

            var board = new Board();
            Assert.AreEqual(8, board.Rows);
            Assert.AreEqual(8, board.Columns);


        }

    }
    
}