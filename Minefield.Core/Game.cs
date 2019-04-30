using System;
using System.Linq;
using System.Threading;

namespace Minefield.Core
{
    public class Game
    {        

        public Board Board { get; set; }

        public Player Player { get; set; }


        private GameState _state;

        public GameState State
        {
            get { return _state; }
            private set
            {
                _state = value;
                OnGameStateChanged(new GameStateChangedEventArgs { NewState = value });
            }
        }

        private GameSettings _settings;

        public Game(GameSettings settings = null)
        {

            if (settings == null)
            {
                settings = GameSettings.Default();
            }

            _settings = settings;



            InitialiseTheGame();

        }

        private void InitialiseTheGame()
        {

            Board = new Board(strategy: _settings.MineLayingStrategy);

            Player = new Player(lives: _settings.NumberOfLives, at: Board.StartPosition());
            RaiseGameStateChangedEvent += Game_RaiseGameStateChangedEvent;;


        }

        void Game_RaiseGameStateChangedEvent(object sender, GameStateChangedEventArgs e)
        {
            Console.WriteLine(e.NewState);

            if (e.NewState == GameState.Boom)
            {
                Thread.Sleep(_settings.ExplosionLengthMs);

                Player.LoseALife();

                if (Player.Lives == 0)
                {
                    State = GameState.Over;
                }
                else
                {
                    State = GameState.Ready;
                }
            }
        }


        public void MovePlayer(Direction direction)
        {

            if (IsOver())
                return;

            var position = Board.NewPositionRelativeTo(direction, from: Player);

            Player.MoveTo(position);

            Player.IncrementMoveCount();

            var suspectLocation = SuspectLocation();

            if (suspectLocation != null && suspectLocation.IsHidden())
            {
                State = GameState.Boom;
                suspectLocation.Boom();      
            }

            // If player reached the other side.. handle here.
        }


        private Mine SuspectLocation()
        {
            return Board.Mines.SingleOrDefault(x => x.ToString() == Player.Position.ToString());
        }

        public bool GameHasBeenWon()
        {
            return Player.Column == Board.ColumnNames.ToArray()[Board.Columns - 1];
        }

        public bool IsOver()
        {
            return State == GameState.Over;
        }

        public bool PlayerIsAtStart()
        {
            return Player.IsAt(Board.StartPosition());
        }

        public void Restart()
        {
            InitialiseTheGame();
        }

        public delegate void GameStateChangedEventArgsEventHandler(object sender, GameStateChangedEventArgs args);

        public event EventHandler<GameStateChangedEventArgs> RaiseGameStateChangedEvent;

        protected virtual void OnGameStateChanged(GameStateChangedEventArgs e)
        {

            EventHandler<GameStateChangedEventArgs> handler = RaiseGameStateChangedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

      


    }

    public class GameStateChangedEventArgs : EventArgs
    {
        public GameState NewState { get; set; }
    }


    public enum GameState
    {
        Ready,
        Over,
        Won,
        Boom
    }

}