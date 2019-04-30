namespace Minefield.Core
{
    public static class ExtensionMethods
    {

        public static bool IsAt(this Position position, Position other)
        {
            return position.Column.Equals(other.Column)
                && position.Row.Equals(other.Row);
        }

        public static string AsGamePlayMessage (this GameState state)
        {
            switch (state)
            {
                case GameState.Ready:
                    return "Ready to move at your peril!!";
                case GameState.Over:
                    return "Game Over. Better luck next time!!";
                case GameState.Won:
                    return "Yay you won!";
                case GameState.ExplosionInProgress:
                    return "!!!BOOM!!!";
                default:
                    return "";
            }
        }

    }
}