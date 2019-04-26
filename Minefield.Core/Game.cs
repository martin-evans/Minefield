namespace Minefield.Core
{
    public class Game {
            public Board Board { get; set; }
            public Player Player { get; set; }

            public Game () {
                Board = new Board();
                Player = new Player(Board.Position ("A", "1"));
            }

        }

        }