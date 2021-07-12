using System;
using System.Collections.Generic;
using System.Linq;

namespace Minefield.Core {
    public class Board {

        public int Rows { get { return Squares.GroupBy (x => x.Row).ToList ().Count; } }
        public int Columns { get { return Squares.GroupBy (x => x.Column).ToList ().Count; } }

        public Position[] Squares { get; set; }

        public Mine[] Mines { get; internal set; }

        internal Position Position (string col, int row) {

            return Squares.ToList ().First (x => x.Column == col && x.Row == row);
        }

        public Position StartPosition () {
            return Position ("A", 1);
        }

        public List<string> ColumnNames { get; private set; }

        public Board (IMineLayingStrategy strategy = null) {

            ColumnNames = "A,B,C,D,E,F,G,H"
                .Split (new [] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList ();

            var lst = new List<Position> ();

            ColumnNames
                .ForEach (x => {
                    for (var i = 1; i <= ColumnNames.Count; i++) {
                        lst.Add (new Position () { Column = x.ToUpper (), Row = i });
                    }
                });

            Squares = lst.ToArray ();

            LayMinesOnTheBoard (strategy);

        }

        private void LayMinesOnTheBoard (IMineLayingStrategy strategy = null) {

            if (strategy == null) {
                strategy = new LayRandomMines ();
            }

            strategy.Lay (this);

        }

        /*
            Directions:-
                Backwards   := same letter(col), reduce number(row)
                Forward     := same letter(col), increase number(row)
                Left        := decrement letter(col), same number(row)
                Right       := increment letter(col), same number(row)
         */

        internal Position NewPositionRelativeTo (Direction? direction, Position from) {

            if (direction == null)
            {
                return from;
            }

            var currentRow = from.Row;
            var currentColumn = from.Column;
            var boardWidth = ColumnNames.Count;

            var newRow = from.Row;
            var newColumn = from.Column;

            var index = ColumnNames.IndexOf (currentColumn);

            switch (direction) {
                case Direction.Up:
                    if (currentRow < boardWidth) {
                        newRow = currentRow + 1;
                    }
                    break;
                case Direction.Down:
                    if (currentRow > 1) {
                        newRow = currentRow - 1;
                    }
                    break;
                case Direction.Left:
                    if (index > 0) {
                        newColumn = ColumnNames[index - 1];
                    }
                    break;
                case Direction.Right:
                    if (index < ColumnNames.Count) {
                        newColumn = ColumnNames[index + 1];
                    }
                    break;
                default:
                    return from;

            }

            return Position (newColumn, newRow);

        }

    }

}