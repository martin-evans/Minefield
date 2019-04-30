namespace Minefield.Core

{
    public class NoMinesStrategy : IMineLayingStrategy
    {
        public void Lay(Board board)
        {
            board.Mines = null;
        }

    }

}