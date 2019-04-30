using System;
using System.Threading;
using Minefield.Core;
using static System.Console;

namespace Minefield.Console
{
    public class GameController
    {

        private ConsoleColor _defaultColor;
        private readonly Game _theGame;

        public GameController(Game theGame)
        {
            _theGame = theGame;
            _defaultColor = ForegroundColor;
            CursorVisible = false;

            _theGame.RaiseGameStateChangedEvent += (sender, e) =>
            {
                ReinitialiseGameUi();
            };

            _theGame.RaiseMineExplodedEvent += (sender, e) =>
            {
                ExplodeUi();

            };

            _theGame.Ready();


        }

        private void ExplodeUi()
        {

            Clear();

            ForegroundColor = ColorBasedOn(_theGame.State);
            Write(GameUiRenderer.Render(_theGame));

            Thread.Sleep(800);

            _theGame.ExplosionEnded();
        }

        private void ReinitialiseGameUi()
        {
            Clear();

            ForegroundColor = ColorBasedOn(_theGame.State);

            Write(GameUiRenderer.Render(_theGame));
        
            Control(ReadKey(true));

            Read();
        }

        private ConsoleColor ColorBasedOn(GameState state)
        {

            switch (state)
            {
                case GameState.ExplosionInProgress:
                    return ConsoleColor.DarkYellow;
                case GameState.Over:
                    return ConsoleColor.Red;
                case GameState.Won:
                    return ConsoleColor.DarkCyan;
                case GameState.Ready:
                default:
                    return _defaultColor;
            }

        }

        public void Control(ConsoleKeyInfo keyInfo)
        {
           
            if (keyInfo.Key == ConsoleKey.R)
            {
                _theGame.Restart();
            }
                  
            if(_theGame.IsOver() || _theGame.IsWon())
            {
                ReinitialiseGameUi();
            }

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                _theGame.MovePlayer(Direction.Up);
            }

            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                _theGame.MovePlayer(Direction.Down);
            }

            if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                _theGame.MovePlayer(Direction.Left);
            }

            if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                _theGame.MovePlayer(Direction.Right);
            }


            _theGame.MovePlayer(null);

        }

    }

}
