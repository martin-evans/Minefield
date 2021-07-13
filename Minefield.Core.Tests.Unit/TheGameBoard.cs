using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class TheGameBoard
    {
        [TestMethod]
        public void ConsistsOf_An8x8Grid()
        {
            var board = new Board();
            Assert.AreEqual(8, board.Rows);
            Assert.AreEqual(8, board.Columns);
        }

        [TestMethod]
        public void ConsistsOf_10RandomlyPlacedMines()
        {
            var board = new Board();

            Assert.AreEqual(10, board.Mines.Length);

            foreach (var location in board.Mines)
            {
                Console.WriteLine($"{location.Column}{location.Row}");
            }
        }
    }
    
    [TestClass]
    public class BreakingTest
    {
        [TestMethod]
        public void Foo()
        {
            Assert.AreEqual(1 ==2, "This test was expected to fail");
        }
    }
}