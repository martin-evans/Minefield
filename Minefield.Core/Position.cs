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
}