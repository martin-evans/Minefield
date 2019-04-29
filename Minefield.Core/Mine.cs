namespace Minefield.Core
{
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
}