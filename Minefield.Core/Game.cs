using System;
using System.Linq;

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

                if(value != GameState.ExplosionInProgress)
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

            RaiseGameStateChangedEvent += Game_RaiseGameStateChangedEvent;
            RaiseMineExplodedEvent += Game_RaiseMineExplodedEvent;

            if (_settings.State.HasValue)
            {
                State = _settings.State.Value;
            }

        }

        public void Ready()
        {
            State = GameState.Ready;
        }

        private void InitialiseTheGame()
        {

            Board = new Board(strategy: _settings.MineLayingStrategy);

            Player = new Player(lives: _settings.NumberOfLives, at: Board.StartPosition());
        }

        public void ExplosionEnded()
        {
            ProcessNewLocation();
        }

        public void MovePlayer(Direction? direction)
        {

            if (IsOver() || IsWon())
            {
                return;
            }


            var position = Board.NewPositionRelativeTo(direction, from: Player);

            if (direction.HasValue)
            {

                Player.MoveTo(position);

                Player.IncrementMoveCount();

            }

            ProcessNewLocation();
        }

        private void ProcessNewLocation()
        {
            var minedLocation = Board.Mines?.FirstOrDefault(x => x.ToString() == Player.Position.ToString());

            if (minedLocation != null && minedLocation.IsHidden())
            {
                minedLocation.Boom();
                OnMineExploded(new MineExplodedEventArgs { MinePosition = minedLocation });
                return;
            }

            var playerIsAtEndOfBoard = (Player.Column == Board.ColumnNames.ToArray()[Board.Columns - 1]);

            if (playerIsAtEndOfBoard)
            {
                State = GameState.Won;
            }
            else
            {
                State = GameState.Ready;
            }
        }

        public bool IsWon()
        {
            return State == GameState.Won;;
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
            Ready();
        }

        public delegate void GameStateChangedEventArgsEventHandler(object sender, GameStateChangedEventArgs args);

        public event EventHandler<GameStateChangedEventArgs> RaiseGameStateChangedEvent;

        public event EventHandler<MineExplodedEventArgs> RaiseMineExplodedEvent;

        protected virtual void OnGameStateChanged(GameStateChangedEventArgs e)
        {

            EventHandler<GameStateChangedEventArgs> handler = RaiseGameStateChangedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnMineExploded(MineExplodedEventArgs e)
        {
            EventHandler<MineExplodedEventArgs> handler = RaiseMineExplodedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void Game_RaiseGameStateChangedEvent(object sender, GameStateChangedEventArgs e)
        {
            if (e.NewState == GameState.Over)
            {
                return;
            }

            if (e.NewState == GameState.Ready && Player.Lives == 0)
            {
                State = GameState.Over;
            }
                 
        }

        private void Game_RaiseMineExplodedEvent(object sender, MineExplodedEventArgs e)
        {
            State = GameState.ExplosionInProgress;
            Player.LoseALife();                       
        }
    }

}