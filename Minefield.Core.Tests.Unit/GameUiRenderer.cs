using System;
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


        internal static string NewLine(int count)
        {

            var s = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                s.Append("\n");
            }


            return s.ToString();

        }


        internal static string Tabs(int count)
        {

            var s = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                s.Append("\t");
            }


            return s.ToString();

        }

        internal static string Render(Game theGame)
        {

            _theGame = theGame;

            var op = @" "
            .WithHeader()           
            .WithGridTopRow()
                .WithGridBody()
                .WithBottomRow();

            return op;

        }

        private static string WithHeader(this string current)
        {
            var s = new StringBuilder();

            s.Append("\n  ");

            s.AppendLine($"{Tabs(4)}!!! Mine Field !!!{NewLine(2)}");
            s.AppendLine($"{Tabs(1)}Use the arrow keys to move your player (x) around the board. Watch out for mines!{NewLine(3)}");
            s.AppendLine($"{Tabs(1)}Moves taken : {_theGame.Player.Score}\t\tLives Remaining : {_theGame.Player.Lives}{NewLine(3)}");
            return $"{current}{s.ToString()}";
        }



        private static string WithGridTopRow(this string current)
        {

            var s = new StringBuilder();

            s.Append($"{NewLine(1)}{Tabs(4)}  ");

            for (var i = 0; i < _theGame.Board.Columns; i++)
            {
                s.Append("_ ");
            }

            return $"{current}{s.ToString()}";

        }

        private static string WithGridBody(this string current)
        {

            var gridStringBuilder = new StringBuilder();

            for (var row = _theGame.Board.Rows; row >= 1; row--)
            {

                var lineStringBuilder = new StringBuilder();

                lineStringBuilder.Append($"{Tabs(4)}{row}|");

                for (var col = 0; col < _theGame.Board.Columns; col++)
                {

                    var colName = _theGame.Board.ColumnNames[col];

                    var currentSquare = _theGame.Board.Squares.SingleOrDefault(x => x.Column == colName && x.Row == row);


                    lineStringBuilder.Append(GetRenderingCharcters(currentSquare));

                }

                gridStringBuilder.AppendLine(lineStringBuilder.ToString());

            }

            return $"{current}{NewLine(1)}{gridStringBuilder.ToString()}";

        }

        private static string GetRenderingCharcters(Position currentSquare)
        {
            if (_theGame.Player.IsAt(currentSquare))
            {
                return "x|";
            }

            var positionIsLocationOfDetonatedMine = _theGame.Board.Mines.Any(x => { return x.IsAt(currentSquare) && (x.State == MineState.Detonated); });

            if (positionIsLocationOfDetonatedMine)
            {
                return "! |";
            }

            return "_|";

        }

        private static string WithBottomRow(this string current)
        {

            var s = new StringBuilder();

            s.Append($"{Tabs(4)}  ");

            foreach (var columnName in _theGame.Board.ColumnNames)
                s.Append($"{columnName} ");

            return $"{current}{s.ToString()}";

        }

    }
}