using System.Linq;
using System.Text;

namespace Minefield.Core.Tests.Unit
{
    public static class GameUiRenderer
    {

        /*

         return @"
          _ _ _ _ _ _ _ _
        8|_|_|_|_|_|_|_|_|            
        7|_|_|_|_|_|_|_|_|
        6|_|_|_|_|_|_|_|_|
        5|_|_|_|_|_|_|_|_|
        4|_|_|_|_|_|_|_|_|
        3|_|X|_|_|_|_|_|_|            
        2|_|_|_|_|_|_|_|s|
        1|_|_|_|_|_|_|_|s|
          a b c d e f g h
        ";

         */

        private static Game _theGame;

        internal static string Render(Game theGame)
        {

            _theGame = theGame;

            var op = @" ".WithTopRow()
                .WithActiveGrid()
                .WithBottomRow();

            return op;

        }

        private static string WithTopRow(this string current)
        {

            var s = new StringBuilder();

            s.Append("\n  ");

            for (var i = 0; i < _theGame.Board.Columns; i++)
            {
                s.Append("_ ");
            }

            return $"{current}{s.ToString()}";

        }

        private static string WithActiveGrid(this string current)
        {

            var gridStringBuilder = new StringBuilder();

            for (var row = _theGame.Board.Rows; row >= 1; row--)
            {

                var lineStringBuilder = new StringBuilder();

                lineStringBuilder.Append($"{row}|");

                for (var col = 0; col < _theGame.Board.Columns; col++)
                {

                    var colName = _theGame.Board.ColumnNames[col];

                    var currentSquare = _theGame.Board.Squares.SingleOrDefault(x => x.Column == colName && x.Row == row);

                    if(currentSquare == null)
                    {
                        System.Console.WriteLine($"{colName}{row} is invalid");
                    }

                    if( currentSquare != null &&  _theGame.Player == currentSquare)
                    {
                        lineStringBuilder.Append("x|");
                    } 
                    else
                    {
                        lineStringBuilder.Append("_|");
                    }

                }

                gridStringBuilder.AppendLine(lineStringBuilder.ToString());

            }

            return $"{current}\n{gridStringBuilder.ToString()}";

        }

        private static string WithBottomRow(this string current)
        {

            var s = new StringBuilder();

            s.Append("  ");

            foreach (var columnName in _theGame.Board.ColumnNames)
                s.Append($"{columnName} ");

            return $"{current}{s.ToString()}";

        }

    }
}