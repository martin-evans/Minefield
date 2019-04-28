using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit {

    public abstract class GameTests {

        protected Game _theGame;

        [TestInitialize]
        public void SetUp () {

            _theGame = new Core.Game ();
        }
    }

}