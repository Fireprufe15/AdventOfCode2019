using System;
using System.IO;

namespace Day2A {
   class Program {
      static void Main(string[] args) {
         using (var fs = new FileStream("in.txt", FileMode.Open)) {

            using (TextReader textReader = new StreamReader(fs)) {

               var codeArray = textReader.ReadToEnd().Split(',', StringSplitOptions.RemoveEmptyEntries);
               var codeIntArray = new int[codeArray.Length];
               for (int i = 0; i < codeArray.Length; i++) {
                  codeIntArray[i] = int.Parse(codeArray[i]);
               }

               for (int noun = 0; noun < 100; noun++) {
                  for (int verb = 0; verb < 100; verb++) {
                     for (int i = 0; i < codeArray.Length; i++) {
                        codeIntArray[i] = int.Parse(codeArray[i]);
                     }
                     var output = Compute(codeIntArray, verb, noun);
                     if (output == 19690720) {
                        Console.WriteLine((100*noun)+verb);
                        Console.ReadKey();
                     }
                  }
               }
               
               Console.WriteLine(codeIntArray[0]);
               Console.ReadKey();
            }
         }
      }

      private static int Compute(int[] memory, int verb, int noun) {

         memory[1] = noun;
         memory[2] = verb;

         for (int i = 0; i < memory.Length; i++) {
            switch (memory[i]) {
               case 1:
                  memory[memory[i + 3]] = memory[memory[i + 1]] + memory[memory[i + 2]];
                  break;

               case 2:
                  memory[memory[i + 3]] = memory[memory[i + 1]] * memory[memory[i + 2]];
                  break;

               case 99:
                  i = memory.Length;
                  break;

               default:
                  break;
            }
            i += 3;
         }

         return memory[0];

      }
   }
}
