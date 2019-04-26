using System;
using System.Collections.Generic;
using System.Linq;

namespace Minefield.Core
{
    public class Board {

            
            public int Rows { get { return Squares.GroupBy(X=>X.Row).ToList().Count; } }
            public int Columns { get { return Squares.GroupBy(X=>X.Column).ToList().Count; } }

            public Position[] Squares { get; set; }

            public Position Position (string col, string row) {

                return Squares.ToList ().First (x => x.Column == col && x.Row == row);
            }

            public Board () {

                var lst = new List<Position> ();

                "a,b,c,d,e,f,g,h"
                .Split (new [] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList ()
                    .ForEach (x => {
                            for (var i = 1; i <= 8; i++) {
                                lst.Add (new Core.Position () { Column = x.ToUpper (), Row = i.ToString()});
                            }
                        });

                Squares = lst.ToArray();

            }
        }

        }