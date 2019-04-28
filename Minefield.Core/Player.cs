using System;

namespace Minefield.Core {
    public class Player {

        public Position Position { get; set; }
        public int Lives { get; private set; }
        public int Score { get; private set; }

        public Player(int lives)
        {
            Lives = lives;
        }

        internal void IncrementMoveCount()
        {
           Score++;
        }

        internal void LoseALife()
        {
            Lives = Lives-1;
        }
    }

}