using System;

namespace Minefield.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(RenderBoard());

            //System.Console.ReadLine();

        }

        private static string RenderBoard(){
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
        }

    }
}
