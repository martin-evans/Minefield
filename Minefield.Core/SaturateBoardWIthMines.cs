namespace Minefield.Core

{
    public class SaturateBoardWithMinesStrategy : IMineLayingStrategy
    {
        public void Lay(Board board)
        {
            
            board.Mines = board.Squares;


        }
    }
}