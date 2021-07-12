using System.Linq;
using System.Text;

namespace Minefield.Core
{
    public static class GameUiRenderer
    {

        private const int GRID_LEFT_MARGIN = 3;
        private const int LINE_SPACING = 1;

        private static Game _theGame;

        internal static string NewLine(int count)
        {
            return CharacterSequence(count, "\n");
        }

        internal static string Tabs(int count)
        {
            return CharacterSequence(count, "\t");
        }

        private static string CharacterSequence(int count, string characters)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < count; i++)
            {
                sb.Append(characters);
            }

            return sb.ToString();

        }

        public static string Render(Game theGame)
        {

            _theGame = theGame;

            var op = @" "
            .WithHeader()
            .WithGridTopRow()
                .WithGridBody()
                .WithBottomRow()
                .WithInstruction();

            return op;

        }

        private static string WithHeader(this string current)
        {
            var s = new StringBuilder();
            s.AppendLine($"{Tabs(4)}!!! Mine Field !!!{NewLine(LINE_SPACING)}");
            s.AppendLine($"{Tabs(1)}Use the arrow keys to move your player (x) across the board to column H");
            s.AppendLine($"{Tabs(1)}Watch out for mines!{NewLine(LINE_SPACING)}");
            s.AppendLine($"{Tabs(1)}Moves taken : {_theGame.Player.Score}{Tabs(4)}Lives Remaining : {_theGame.Player.Lives}{NewLine(LINE_SPACING)}");
            s.AppendLine($"{Tabs(3)}{_theGame.State.AsGamePlayMessage()}{NewLine(LINE_SPACING)}");

            return $"{current}{s}";
        }


        private static string WithGridTopRow(this string current)
        {

            var s = new StringBuilder();

            s.Append($"{NewLine(LINE_SPACING)}{Tabs(GRID_LEFT_MARGIN)}  ");

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

                lineStringBuilder.Append($"{Tabs(GRID_LEFT_MARGIN)}{row}|");

                for (var col = 0; col < _theGame.Board.Columns; col++)
                {

                    var colName = _theGame.Board.ColumnNames[col];

                    var currentSquare = _theGame.Board.Squares.SingleOrDefault(x => x.Column == colName && x.Row == row);


                    lineStringBuilder.Append(GetRenderingCharacters(currentSquare));

                }

                gridStringBuilder.AppendLine(lineStringBuilder.ToString());

            }

            return $"{current}{NewLine(1)}{gridStringBuilder.ToString()}";

        }

        private static string GetRenderingCharacters(Position currentSquare)
        {
            if (_theGame.Player.IsAt(currentSquare))
            {
                return "x|";
            }

            var positionIsLocationOfDetonatedMine = _theGame.Board.Mines.Any(x => x.IsAt(currentSquare) && x.State == MineState.Detonated);

            if (positionIsLocationOfDetonatedMine)
            {
                return "*|";
            }

            if (_theGame.IsFinished())
            {
                var positionIsLocationOfUndetonatedDetonatedMine = _theGame.Board.Mines.Any(x => x.IsAt(currentSquare) && x.State == MineState.Primed);

                if (positionIsLocationOfUndetonatedDetonatedMine)
                    return "!|";
            }

            return "_|";

        }

        private static string WithBottomRow(this string current)
        {

            var s = new StringBuilder();

            s.Append($"{Tabs(GRID_LEFT_MARGIN)}  ");

            foreach (var columnName in _theGame.Board.ColumnNames)
                s.Append($"{columnName} ");

            return $"{current}{s}";

        }


        private static string WithInstruction(this string current)
        {
            var s = new StringBuilder();
            s.AppendLine(NewLine(LINE_SPACING));

            s.Append($"{Tabs(GRID_LEFT_MARGIN)}(R)estart");

            return $"{current}{s}";

        }
    }
}