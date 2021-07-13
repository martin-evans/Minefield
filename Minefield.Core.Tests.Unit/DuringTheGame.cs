using System;
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

            TheGame.MovePlayer(Direction.Right);
            Assert.AreEqual(1, TheGame.Player.Score);

            TheGame.MovePlayer(Direction.Up);
            Assert.AreEqual(2, TheGame.Player.Score);

            TheGame.MovePlayer(Direction.Left);
            Assert.AreEqual(3, TheGame.Player.Score);

            TheGame.MovePlayer(Direction.Down);
            Assert.AreEqual(4, TheGame.Player.Score);

        }

        [TestMethod]
        public void WhenThePlayerReachesTheEndOfTheBoard_TheGameIsWon()
        {

            TheGame = new Game(GameSettings.TestSettings_NoMines());

            var furthermostColumnIndex = TheGame.Board.Columns - 1;

            for (var i = 0; i <= furthermostColumnIndex; i++)
            {

                var shouldBeWon = (i == furthermostColumnIndex);

                Console.WriteLine(TheGame.Player);

                Console.WriteLine($"Should be won yet? {shouldBeWon}");

                Assert.AreEqual(shouldBeWon, TheGame.IsWon());

                if (!shouldBeWon)
                {
                    TheGame.MovePlayer(Direction.Right);
                }
            }
        }

        [TestMethod] 
        public void WhenTheGameIsOver_PlayerCannotMoveAgain()
        {

            var statesThatsHouldNotAllowFurtherMovement = new[] { GameState.Over, GameState.Won };

            foreach(var testState in statesThatsHouldNotAllowFurtherMovement)
            {

                TheGame = new Game(GameSettings.TestSettings_NoMines(state: testState));

                TheGame.MovePlayer(Direction.Right);
                TheGame.MovePlayer(Direction.Right);
                TheGame.MovePlayer(Direction.Right);

                Assert.IsTrue(TheGame.PlayerIsAtStart(), $"Additional moves allowed  during Gamestate {testState}");

            }

        }


        [TestMethod]
        public void WhenThePlayerWalksOnAnUnexplodedMine_TheMineDetonates_And_ThePlayerLoosesALife()
        {

            // player moving anywhere will stand on a mine       
            TheGame = new Game(GameSettings.TestSettings_UnexplodedMinesEverywhere());

            var currentLives = TheGame.Player.Lives;

            TheGame.MovePlayer(Direction.Right);

            var theMine = TheGame.Board.Mines.Single(x => x.IsAt(TheGame.Player));


            Assert.AreEqual(MineState.Detonated, theMine.State);

            Assert.IsFalse(theMine.IsHidden());

            Assert.AreEqual(currentLives - 1, TheGame.Player.Lives);

        }


        [TestMethod]
        public void WhenThePlayerWalksOnAnUnexplodedMine_TheMineExplodes()
        {

            TheGame = new Game(GameSettings.TestSettings_UnexplodedMinesEverywhere());

            var explosionOccurred = false;


            EventHandler<MineExplodedEventArgs> handleStateChange = (sender, args) =>
            {
                explosionOccurred = true;
                TheGame.ExplosionEnded();
            };


            TheGame.RaiseMineExplodedEvent += handleStateChange;


            TheGame.MovePlayer(Direction.Right);

            Assert.IsTrue(explosionOccurred);


            TheGame.RaiseMineExplodedEvent -= handleStateChange;


        }


        [TestMethod]
        public void AfterThePlayerMoves_IfTheGameHasntEnded_ThePlayerMayMoveAgain()
        {

            TheGame = new Game(GameSettings.TestSettings_NoMines());
                       
            var gameSetToReady = false;

            EventHandler<GameStateChangedEventArgs> handleStateChange = (sender, args) =>
            {
                if (args.NewState == GameState.Ready)
                {
                    gameSetToReady = true;
                }

            };


            TheGame.RaiseGameStateChangedEvent += handleStateChange;

            TheGame.MovePlayer(Direction.Right);

            Assert.IsTrue(gameSetToReady);

            TheGame.RaiseGameStateChangedEvent -= handleStateChange;


        }

        [TestMethod]
        public void WhenThePlayerWalksOnAnExplodedMine_TheMineDoesNothing_And_ThePlayerDoesNotLosesAnyMoreLives()
        {

            // player moving anywhere will stand on a mine       
            TheGame = new Game(GameSettings.TestSettings_UnexplodedMinesEverywhere());

            var currentLives = TheGame.Player.Lives;

            TheGame.MovePlayer(Direction.Right);

            Assert.AreEqual(currentLives - 1, TheGame.Player.Lives);

        }


        [TestMethod]
        public void WhenThePlayerLoosesAllLives_TheGameIsOver()
        {

            // player moving anywhere will stand on a mine            
            TheGame = new Game(GameSettings.TestSettings_UnexplodedMinesEverywhere(lives: 1));


            EventHandler<MineExplodedEventArgs> handleStateChange = (sender, args) =>
            {
                TheGame.ExplosionEnded();
            };


            TheGame.RaiseMineExplodedEvent += handleStateChange;

            TheGame.MovePlayer(Direction.Right);

            Assert.AreEqual(0, TheGame.Player.Lives);

            Assert.IsTrue(TheGame.IsOver());

        }


        [TestMethod]
        public void WhenTheGameIsOver_AllPlayerMovesAreIgnored()
        {


            // starting a game with 0 lives defines the game as being already over
            TheGame = new Game(new GameSettings() { NumberOfLives = 0 });


            var currentPlayerPosition = TheGame.Player;
            TheGame.MovePlayer(Direction.Right);

            var newPlayerPosition = TheGame.Player;

            Assert.AreEqual(currentPlayerPosition, newPlayerPosition);


        }

        [TestMethod]
        public void RestartingGame_ResetsGame()
        {

            // player moving anywhere will stand on a mine            
            TheGame = new Game(GameSettings.TestSettings_UnexplodedMinesEverywhere());

            TheGame.MovePlayer(Direction.Right);

            Assert.IsFalse(TheGame.PlayerIsAtStart());

            Assert.AreEqual(1, TheGame.Player.Score);


            TheGame.Restart();


            Assert.IsTrue(TheGame.PlayerIsAtStart());

            Assert.AreEqual(0, TheGame.Player.Score);
            
            Assert.AreEqual(0, 0);
            
        }

    }

}