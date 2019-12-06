using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6 {
   class Program {
      static void Main(string[] args) {
         using (var fs = new FileStream("input.txt", FileMode.Open)) {
            using (TextReader tr = new StreamReader(fs)) {

               var input = tr.ReadToEnd().Split('\n');
               var orbit = GetOrbits("COM", input, null);
               var youRef = FindReference("YOU", orbit);
               var sanRef = FindReference("SAN", orbit);
               var dist = FindDistance(youRef, sanRef);
               Console.WriteLine(orbit.returnCount());
               Console.WriteLine(dist.ToString());

            }
         }
      }

      public static OrbitalBody GetOrbits(string name, string[] input, OrbitalBody parent) {

         var items = input.Where(x => x.StartsWith(name)).ToList();

         if (items.Count == 0) {
            return new OrbitalBody(name, parent);
         }

         var returnval = new OrbitalBody(items[0], parent);
         foreach (var item in items) {
            returnval.OrbitingBodies.Add(GetOrbits(item.Split(')')[1], input.Where(x => !x.StartsWith(name)).ToArray(), returnval));
         }
         return returnval;

      }

      public static OrbitalBody FindReference(string name, OrbitalBody baseOrbit) {
         
         var match = baseOrbit.OrbitingBodies.FirstOrDefault(x => x.Name == name);
         if (match != null) {
            return match;
         }

         foreach (var body in baseOrbit.OrbitingBodies) {
            match = FindReference(name, body);
            if (match != null) {
               return match;
            }
         }

         return null;

      }

      public static int FindDistance(OrbitalBody first, OrbitalBody second) {
         int dist = 0;
         var foundCommonAncestor = false;
         var currentBody = first.Parent;
         while (!foundCommonAncestor) {
            if (currentBody.hasChild(second.Name)) {
               foundCommonAncestor = true;
            } else {
               currentBody = currentBody.Parent;
               dist++;
            }
         }
         foundCommonAncestor = false;
         currentBody = second.Parent;
         while (!foundCommonAncestor) {
            if (currentBody.hasChild(first.Name)) {
               foundCommonAncestor = true;
            } else {
               currentBody = currentBody.Parent;
               dist++;
            }
         }

         return dist;
      }


   }

   class OrbitalBody {
      public string Name { get; set; }    
      public OrbitalBody Parent { get; set; }
      public List<OrbitalBody> OrbitingBodies { get; set; }

      public OrbitalBody() {
         OrbitingBodies = new List<OrbitalBody>();
      }

      public OrbitalBody(string input, OrbitalBody parent = null) {
         var values = input.Split(')');
         Name = values[0];
         OrbitingBodies = new List<OrbitalBody>();
         Parent = parent;
      }

      public int returnCount(int treeCount = -1) {

         treeCount += 1;
         var newCount = 0;
         foreach (var item in OrbitingBodies) {
            newCount += item.returnCount(treeCount);
         }
         treeCount += newCount;
         return treeCount;

      }

      public bool hasChild(string name) {

         return FindReference(name, this) != null;

      }

      public OrbitalBody FindReference(string name, OrbitalBody baseOrbit) {

         var match = baseOrbit.OrbitingBodies.FirstOrDefault(x => x.Name == name);
         if (match != null) {
            return match;
         }

         foreach (var body in baseOrbit.OrbitingBodies) {
            match = FindReference(name, body);
            if (match != null) {
               return match;
            }
         }

         return null;

      }

   }


}
