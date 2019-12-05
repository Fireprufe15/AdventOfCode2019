using System;
using System.Collections.Generic;
using System.IO;

namespace Day5 {
   class Program {
      static void Main(string[] args) {
         using (var fs = new FileStream("in.txt", FileMode.Open)) {

            using (TextReader textReader = new StreamReader(fs)) {

               var codeArray = textReader.ReadToEnd().Split(',', StringSplitOptions.RemoveEmptyEntries);
               var codeIntArray = new int[codeArray.Length];
               for (int i = 0; i < codeArray.Length; i++) {
                  codeIntArray[i] = int.Parse(codeArray[i]);
               }
               Compute(codeIntArray);
               Console.ReadKey();
            }
         }
      }

      private static void Compute(int[] memory) {                  

         for (int i = 0; i < memory.Length; i++) {

            var opcodeString = memory[i].ToString().PadLeft(5, '0');

            int opCode = int.Parse(opcodeString.Substring(opcodeString.Length - 2));

            int param1;
            int param2;

            switch (opCode) {
               case 1:
                  setParams(memory, i, opcodeString, out param1, out param2);
                  memory[memory[i + 3]] = param1 + param2;
                  i += 3;
                  break;
               case 2:
                  setParams(memory, i, opcodeString, out param1, out param2);
                  memory[memory[i + 3]] = param1 * param2;
                  i += 3;
                  break;
               case 3:
                  Console.WriteLine("please enter integer");
                  var input = int.Parse(Console.ReadLine());
                  memory[memory[i + 1]] = input;
                  i += 1;
                  break;
               case 4:
                  Console.WriteLine(memory[memory[i + 1]].ToString());
                  i += 1;
                  break;
               case 5:
                  setParams(memory, i, opcodeString, out param1, out param2);
                  if (param1 > 0) i = param2 - 1;
                  else i += 2;
 
                  break;
               case 6:
                  setParams(memory, i, opcodeString, out param1, out param2);
                  if (param1 == 0) i = param2 - 1;
                  else i += 2;

                  break;
               case 7:
                  setParams(memory, i, opcodeString, out param1, out param2);
                  if (param1 < param2) memory[memory[i + 3]] = 1;
                  else memory[memory[i + 3]] = 0;
                  i += 3;
                  break;
               case 8:
                  setParams(memory, i, opcodeString, out param1, out param2);
                  if (param1 == param2) memory[memory[i + 3]] = 1;
                  else memory[memory[i + 3]] = 0;
                  i += 3;
                  break;
               case 99:
                  i = memory.Length;
                  break;
               default:
                  break;
            }
            
         }         

      }

      private static void setParams(int[] memory, int currentPointer, string opcodeString, out int param1, out int param2) {

         if (opcodeString[2] == '1') {
            param1 = memory[currentPointer + 1];
         } else {
            param1 = memory[memory[currentPointer + 1]];
         }
         if (opcodeString[1] == '1') {
            param2 = memory[currentPointer + 2];
         } else {
            param2 = memory[memory[currentPointer + 2]];
         }

      }

   }
}
