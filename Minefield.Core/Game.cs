using System;

namespace Minefield.Core
{
    public class Game {
        public Board Board { get; set; }
        public Player Player { get; set; }

        public Game () {
            Board = new Board ();
            Player = new Player(){ Position = Board.StartPosition(), Lives = 3};
        }
        
        public void MovePlayer(Direction direction){            

            Player.Position = Board.NewPositionRelativeTo(direction, from: Player.Position);

        }

    }

}