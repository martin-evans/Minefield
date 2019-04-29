using System.Linq;

namespace Minefield.Core
{
      public class LayRandomMines : IMineLayingStrategy {
        public void Lay (Board board) {
            var rnd = new System.Random ();

            board.Mines = new Mine[10];

            int columnIndex;
            int rowIndex;

            for (var i = 0; i < board.Mines.Length; i++) {

                do {

                    columnIndex = rnd.Next (1, board.ColumnNames.Count);
                    rowIndex = rnd.Next (1, board.ColumnNames.Count);

                    var position = board.Position (board.ColumnNames[columnIndex], rowIndex);

                    if (!board.Mines.Contains (position) && position != board.StartPosition ()) {
                        board.Mines[i] = Mine.Lay(at: position);
                    }

                } while (board.Mines[i] == null);

            }
        }
    }
 

}