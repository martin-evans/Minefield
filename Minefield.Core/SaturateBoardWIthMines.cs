using System.Linq;

namespace Minefield.Core

{
    public class SaturateBoardWithMinesStrategy : IMineLayingStrategy
    {
        public void Lay(Board board)
        {
            board.Mines = board.Squares.ToList().Select(pos => Mine.Lay(at: pos)).ToArray();
        }
    }

}