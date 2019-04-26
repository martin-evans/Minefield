namespace Minefield.Core
{
    public class Player {

            public Position Position { get; private set; }

            public Player (Position startingPosition) {
                Position = startingPosition;
            }
        }

        }