using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class GameUiRendering : GameTests {

        [TestMethod]
        public void RenderingGameState_DIsplaysBoardOnScreen () {

            var op = GameUiRenderer.Render (_theGame);

            Console.WriteLine (op);

        }

    }
}