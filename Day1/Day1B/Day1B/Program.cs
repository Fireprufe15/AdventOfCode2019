using System;
using System.IO;

namespace Day1B {
   class Program {
      static void Main(string[] args) {
         using (var fs = new FileStream("in.txt", FileMode.Open)) {

            using (TextReader textReader = new StreamReader(fs)) {

               var items = textReader.ReadToEnd();
               var itemsArray = items.Split('\n');

               int fuelRequirement = 0;
               foreach (var item in itemsArray) {
                  if (!String.IsNullOrEmpty(item)) fuelRequirement += CalcModuleFuel(double.Parse(item));
               }

               Console.WriteLine(fuelRequirement.ToString());
               Console.ReadKey();
            }

         }
      }

      private static int CalcModuleFuel(double moduleMass) {

         var fuelReq = Math.Floor(moduleMass / 3) - 2;
         if (fuelReq <= 0) {
            return 0;
         }
         fuelReq += CalcModuleFuel(fuelReq);
         return (int)fuelReq;

      }
   }
}
