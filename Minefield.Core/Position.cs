namespace Minefield.Core
{
    public class Position {
                public string Column { get; set; }
                public int Row { get; set; }

                public override string ToString(){
                    
                    return $"{Column}{Row}";
                }
            }

        }