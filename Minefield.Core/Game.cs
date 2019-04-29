using System;
using System.Linq;

namespace Minefield.Core
{
    public class Game {

        public Board Board { get; set; }

        public Player Player { get; set; }

        private GameSettings _settings;

        public Game (GameSettings settings = null) {
            
            if(settings == null) {
                settings = GameSettings.Default();
            }
            
            _settings = settings;

            InitialiseTheGame();
            
        }
        
        private void InitialiseTheGame(){

            Board = new Board (strategy: _settings.MineLayingStrategy);
            
            Player = new Player(lives:_settings.NumberOfLives, at: Board.StartPosition());

        }

        public void MovePlayer(Direction direction){            

            if(IsOver())
            return;

            var position =  Board.NewPositionRelativeTo(direction, from: Player);

            Player.MoveTo(position);
                                    
            Player.IncrementMoveCount();

            var suspectLocation = SuspectLocation();

            if(suspectLocation != null && suspectLocation.IsHidden()){
                suspectLocation.Boom();
                Player.LoseALife();
            }

        }

        private Mine SuspectLocation()
        {
            return Board.Mines.SingleOrDefault(x => x.ToString() == Player.Position.ToString());            
        }

        public bool GameHasBeenWon()
        {
            return Player.Column == Board.ColumnNames.ToArray()[Board.Columns-1];
        }

        public bool IsOver()
        {
            return Player.Lives == 0;
        }

        public bool PlayerIsAtStart()
        {
            return Player.IsAt(Board.StartPosition());
        }

        public void Restart()
        {
            InitialiseTheGame();
        }
    }      
}