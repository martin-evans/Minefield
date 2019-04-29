
namespace Minefield.Core
{

    public class Player : Position
    {

        public int Lives { get; private set; }

        public int Score { get; private set; }

        public Position Position { get { return this; } }

        public Player(int lives, Position at)
        {
            Column = at.Column;
            Row = at.Row;
            Lives = lives;
        }

        internal void IncrementMoveCount()
        {
            Score++;
        }

        internal void LoseALife()
        {
            Lives = Lives - 1;
        }

    }

}