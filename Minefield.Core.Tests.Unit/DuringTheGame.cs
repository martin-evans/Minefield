using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minefield.Core.Tests.Unit
{
    [TestClass]
    public class DuringTheGame : GameTests
    {

        [TestMethod]
        public void WhenThePlayerMoves_TheMovesScore_IsIncremented()
        {

            _theGame.MovePlayer(Direction.Right);
            Assert.AreEqual(1, _theGame.Player.Score);

            _theGame.MovePlayer(Direction.Up);
            Assert.AreEqual(2, _theGame.Player.Score);

            _theGame.MovePlayer(Direction.Left);
            Assert.AreEqual(3, _theGame.Player.Score);

            _theGame.MovePlayer(Direction.Down);
            Assert.AreEqual(4, _theGame.Player.Score);

        }

        [TestMethod]
        public void WhenThePlayerReachesTheEndOfTheBoard_TheGameIsWon()
        {

            var furthermostColumnIndex = _theGame.Board.Columns - 1;

            for (var i = 0; i <= furthermostColumnIndex; i++)
            {

                var shouldBeWon = (i == furthermostColumnIndex);

                System.Console.WriteLine(_theGame.Player);

                System.Console.WriteLine($"Should be won yet? {shouldBeWon}");

                Assert.AreEqual(shouldBeWon, _theGame.GameHasBeenWon());

                if (!shouldBeWon)
                {
                    _theGame.MovePlayer(Direction.Right);
                }
            }
        }

        [TestMethod]
        public void WhenThePlayerWalksOnAnUnexplodedMine_TheMineDetonates_And_ThePlayerLoosesALife()
        {

            // player moving anywhere will stand on a mine       
            _theGame = new Game(GameSettings.UnexplodedMinesEverywhere());

            var currentLives = _theGame.Player.Lives;

            _theGame.MovePlayer(Direction.Right);

            var theMine = _theGame.Board.Mines.Single(x => x.IsAt(_theGame.Player));

            Assert.AreEqual(MineState.Detonated, theMine.State);

            Assert.IsFalse(theMine.IsHidden());

            Assert.AreEqual(currentLives - 1, _theGame.Player.Lives);

        }


        [TestMethod]
        public void WhenThePlayerWalksOnAnExplodedMine_TheMineDoesNothing_And_ThePlayerDoesNotLosesAnyMoreLives()
        {

            // player moving anywhere will stand on a mine       
            _theGame = new Game(GameSettings.UnexplodedMinesEverywhere());

            var currentLives = _theGame.Player.Lives;

            _theGame.MovePlayer(Direction.Right);

            Assert.AreEqual(currentLives - 1, _theGame.Player.Lives);

        }



        [TestMethod]
        public void WhenThePlayerLoosesAllLives_TheGameIsOver()
        {

            // player moving anywhere will stand on a mine            
            _theGame = new Game(GameSettings.UnexplodedMinesEverywhere(lives: 1));

            _theGame.MovePlayer(Direction.Right);

            Assert.AreEqual(0, _theGame.Player.Lives);

            Assert.IsTrue(_theGame.IsOver());

        }


        [TestMethod]
        public void WhenTheGameIsOver_AllPlayerMovesAreIgnored()
        {


            // starting a game with 0 lives defines the game as being already over
            _theGame = new Game(new GameSettings() { NumberOfLives = 0 });


            var currentPlayerPosition = _theGame.Player;
            _theGame.MovePlayer(Direction.Right);

            var newPlayerPosition = _theGame.Player;

            Assert.AreEqual(currentPlayerPosition, newPlayerPosition);


        }

        [TestMethod]
        public void RestartingGame_ResetsGame()
        {

            // player moving anywhere will stand on a mine            
            _theGame = new Game(GameSettings.UnexplodedMinesEverywhere());

            _theGame.MovePlayer(Direction.Right);

            Assert.IsFalse(_theGame.PlayerIsAtStart());

            Assert.AreEqual(1, _theGame.Player.Score);


            _theGame.Restart();


            Assert.IsTrue(_theGame.PlayerIsAtStart());

            Assert.AreEqual(0, _theGame.Player.Score);


        }

    }

}