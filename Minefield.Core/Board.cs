using System;
using System.Collections.Generic;
using System.Linq;

namespace Minefield.Core {
    public class Board {

        public int Rows { get { return Squares.GroupBy (X => X.Row).ToList ().Count; } }
        public int Columns { get { return Squares.GroupBy (X => X.Column).ToList ().Count; } }

        public Position[] Squares { get; set; }

        public Position[] Mines { get; private set; }

        private Position Position (string col, int row) {

            return Squares.ToList ().First (x => x.Column == col && x.Row == row);
        }

        public Position StartPosition () {
            return Position ("A", 1);
        }

        private List<string> ColumnNames;

        public Board () {

            ColumnNames = "A,B,C,D,E,F,G,H"
                .Split (new [] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList ();

            var lst = new List<Position> ();

            ColumnNames
                .ForEach (x => {
                    for (var i = 1; i <= ColumnNames.Count; i++) {
                        lst.Add (new Core.Position () { Column = x.ToUpper (), Row = i });
                    }
                });

            Squares = lst.ToArray ();

            LayMinesOnTheBoard ();

        }

        /*
            Directions:-
                Backwards   := same letter(col), reduce number(row)
                Forward     := same letter(col), increase number(row)
                Left        := decrement letter(col), same number(row)
                Right       := increment letter(col), same number(row)
         */

        internal Position NewPositionRelativeTo (Direction direction, Position from) {

            var currentRow = from.Row;
            var currentColumn = from.Column;
            var boardWidth = ColumnNames.Count;

            var newRow = from.Row;
            var newColumn = from.Column;

            var index = ColumnNames.IndexOf (currentColumn);

            switch (direction) {
                case Direction.Up:
                    if (currentRow <= boardWidth) {
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

        private void LayMinesOnTheBoard () {

            var rnd = new System.Random();

            Mines = new Position[10];
            
            int columnIndex;
            int rowIndex;
            

            for(var i = 0; i < Mines.Length; i++){
                                              
                do {
                    
                    columnIndex = rnd.Next(1, ColumnNames.Count);
                    rowIndex = rnd.Next(1, ColumnNames.Count);

                    var position = Position(ColumnNames[columnIndex], rowIndex);

                    if(!Mines.Contains(position) && position != StartPosition()){
                       Mines[i] = position;    
                    }

                } while (Mines[i] == null);

            }

        }

    }

}