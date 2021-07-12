using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class GameUiRendering : GameTests {

        [TestMethod]
        public void RenderingGameState_DisplaysBoardOnScreen () {

            var op = GameUiRenderer.Render (TheGame);

            Console.WriteLine (op);

        }

    }
}