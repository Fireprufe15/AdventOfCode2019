using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4A {
   class Program {
      static void Main(string[] args) {
         Console.WriteLine("enter number 1");
         int lowerRange = int.Parse(Console.ReadLine());
         Console.WriteLine("enter number 2");
         int higherRange = int.Parse(Console.ReadLine());

         int count = 0;
         for (int i = lowerRange; i < higherRange+1; i++) {

            var intString = i.ToString();
            var followsDecreaseRule = true;
            var followDupeRule = false;
            Dictionary<char, int> groups = new Dictionary<char, int>();
            for (int x = 1; x < intString.Length; x++) {

               if (int.Parse(intString[x - 1].ToString()) > int.Parse(intString[x].ToString())) {
                  followsDecreaseRule = false;
                  break;
               }

               if (intString[x - 1] == intString[x]) {                  
                  if (groups.ContainsKey(intString[x])) {
                     groups[intString[x]]++;
                  } else {
                     groups.Add(intString[x],2);
                  }
               }

            }

            if (groups.Where(x => x.Value == 2).Count() > 0) {
               followDupeRule = true;
            }

            if (followsDecreaseRule && followDupeRule) {
               count++;
            }

         }

         Console.WriteLine(count);
         Console.ReadKey();
      }
   }
}
