namespace Minefield.Core
{
    public static class ExtensionMethods
    {

        public static bool IsAt(this Position position, Position other)
        {

            return position.Column.Equals(other.Column)
                && position.Row.Equals(other.Row);

        }

    }
}