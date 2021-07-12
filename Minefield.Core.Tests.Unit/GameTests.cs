using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit {

    public abstract class GameTests {

        protected Game TheGame;

        [TestInitialize]
        public void SetUp () {

            TheGame = new Game ();
        }
    }

}