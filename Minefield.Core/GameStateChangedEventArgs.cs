using System;

namespace Minefield.Core
{
    public class GameStateChangedEventArgs : EventArgs
    {
        public GameState NewState { get; set; }
    }
}