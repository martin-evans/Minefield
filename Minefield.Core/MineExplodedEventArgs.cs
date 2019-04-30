using System;

namespace Minefield.Core
{
    public class MineExplodedEventArgs : EventArgs
    {
        public Position MinePosition { get; set; }

    }
}