using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3A {
   class Program {
      static void Main(string[] args) {
         using (var fs = new FileStream("in.txt", FileMode.Open)) {
            using (TextReader tr = new StreamReader(fs)) {

               var wires = tr.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
               var wirePaths = new List<List<GridCoordinate>>();               

               foreach (var wire in wires) {
                  var currentCoord = new GridCoordinate(0, 0);
                  var wirePath = new List<GridCoordinate>();
                  var wireMoves = wire.Split(',', StringSplitOptions.RemoveEmptyEntries);
                  foreach (var move in wireMoves) {                     
                     var moveAmount = int.Parse(move.Substring(1));
                     for (int i = 0; i < moveAmount; i++) {
                        switch (move[0]) {
                           case 'U':
                              currentCoord = new GridCoordinate(currentCoord.X, currentCoord.Y + 1);
                              break;
                           case 'D':
                              currentCoord = new GridCoordinate(currentCoord.X, currentCoord.Y - 1);
                              break;
                           case 'L':
                              currentCoord = new GridCoordinate(currentCoord.X - 1, currentCoord.Y);
                              break;
                           case 'R':
                              currentCoord = new GridCoordinate(currentCoord.X + 1, currentCoord.Y);
                              break;
                           default:
                              break;
                        }
                        wirePath.Add(new GridCoordinate(currentCoord.X, currentCoord.Y));
                     }
                  }
                  wirePaths.Add(wirePath);
               }

               var crossingPoints = new List<GridCoordinate>();
               foreach (var item in wirePaths[0]) {
                  var matchElement = wirePaths[1].Where(x => x.X == item.X && x.Y == item.Y).FirstOrDefault();
                  if (matchElement != null) {
                     crossingPoints.Add(new GridCoordinate(item.X, item.Y) {
                        Steps = wirePaths[0].IndexOf(item)+1 + wirePaths[1].IndexOf(matchElement)+1
                     });
                  }
               }

               GridCoordinate shortestDistance = new GridCoordinate(int.MaxValue, 0);
               foreach (var item in crossingPoints) {
                  if (shortestDistance.ManhattanDistance() > item.ManhattanDistance()) {
                     shortestDistance = item;
                  }
               }               

               Console.WriteLine(shortestDistance.ManhattanDistance());
               Console.WriteLine(crossingPoints.Min(x => x.Steps));
               Console.ReadKey();
            }
         }
      }
   }

   public class GridCoordinate {
      public int X { get; set; }
      public int Y { get; set; }
      public int Steps { get; set; }

      public GridCoordinate(int x, int y) {
         X = x;
         Y = y;
      }

      public int ManhattanDistance() {
         return Math.Abs(X) + Math.Abs(Y);
      }

      public override int GetHashCode() {
         return HashCode.Combine(X, Y);
      }
   }
}
