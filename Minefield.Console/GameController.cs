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

            _theGame.RaiseGameStateChangedEvent += (sender, e) =>
            {
                Clear();
                ForegroundColor = ColorBasedOn(_theGame.State);

                Write(GameUiRenderer.Render(_theGame));
                Control(ReadKey());
                Read();
            };


            _theGame.RaiseMineExplodedEvent += (sender, e) =>
            {
                Clear();
                ForegroundColor = ColorBasedOn(_theGame.State);
                Write(GameUiRenderer.Render(_theGame));

                Thread.Sleep(800);

                _theGame.ExplosionEnded();

            };

            _theGame.Ready();


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

        }

    }

}
