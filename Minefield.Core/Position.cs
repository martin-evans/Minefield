namespace Minefield.Core
{
    public class Position
    {
        public string Column { get; set; }
        public int Row { get; set; }

        public void MoveTo(Position position)
        {
            Column = position.Column;
            Row = position.Row;
        }

        public override string ToString()
        {

            return $"{Column}{Row}";
        }
    }

    public class Mine : Position
    {
        public MineState State { get; internal set; }

        public bool IsHidden()
        {
            return State == MineState.Primed;
        }

        public void Boom()
        {
            State = MineState.Detonated;
        }

        public static Mine Lay(Position at)
        {
            return new Mine
            {
                Column = at.Column,
                Row = at.Row,
                State = MineState.Primed,
            };
        }

    }

    public enum MineState
    {
        Primed,
        Detonated
    }

    public static class ExtensionMethods
    {

        public static bool IsAt(this Position position, Position other)
        {

            return position.Column.Equals(other.Column)
                && position.Row.Equals(other.Row);

        }

    }
}