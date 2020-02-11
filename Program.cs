using System;
using System.Linq;
using System.Collections.Generic;

// Assumptions/ Decisions
// -----------------
// I assumed that if I used .net that I could not change the data structure.
// The location of the Cap letters in the name does not matter in the serial
// The way I decided to parse this data was to use a dictionary to find the duplicates
// and then null duplicate indexes in the array. Then I used the linq libary to parse the valid data.
// ---------------------
// Time/space efficiency
// ---------------------
// The duplicate check/nulling method is O(n) but I am currently unaware of the time efficieny of the Linq query so I will asume if O(n) as well.
namespace getJob
{
    class Program
    {

        static void Main(string[] args)
        {
            var soldItem = new[]
            {
                new Store(1, "Rock Salt", "RS1", 10, 50, "Andy Ghadban"),

                new Store(2, "Planter's Nuts", "XO28-V", 4, 23, "Reginald VelJohnson"),
                
                new Store(3, "Bulk Pack SuperWash Fire Hoses", "BPSW-FH3", 122, 122, "Harry Lewis"),
               
                new Store(4, "BlackBOX carnival sticks", "BBOX4", 215, 460, "Jean-Luc Picard"),
               
                new Store(5, "ARMY surplus Canned Beef", "5-ARMYCB", 34, 513, "Jean-Luc Picard"),
               
                new Store(6, "Compressed Air", "CA6", 80, 900, "Frank Castle"),
               
                new Store(7, "Rock Salt", "RS1", 10, 2, "Reginald VelJohnson"),
               
                new Store(8, "Werther's Original", "WO-8", 12, 75, "Andy Ghadban"),
               
                new Store(9, "tonka truck passenger door", "TT-PD-9", 336, 275, "Jean-Luc Picard"),
               
                new Store(10, "ARMY surplus Canned Beef", "5-ARMYCB", 12, 6000, "Frank Castle"),
               
                new Store(11, "SwashBuckler's Buckled Swashes", "SBBS11", 122, 160, "Harry Lewis"),

                new Store(12, "Rock Salt", "RS1", 10, 50, "Andy Ghadban")
            };

            var query = parseInfo(soldItem);
            foreach(var t in query)
            {
                System.Console.WriteLine(t.salesPerson);
            }

        }

        public static Store[] checkForDupilcates(Store[] input)
        {
             Dictionary<string, bool> dic = new Dictionary<string, bool>();
               for (int i = 0; i < input.Length; i++)
               {
                   
                  if(dic.ContainsKey(input[i].name))
                  {
                    dic[input[i].name] = false;
                  }else{
                      dic.Add(input[i].name, true);
                  }
               } 

               for(int t = 0; t < input.Length; t++)
               {
                   bool output;
                   if(dic.TryGetValue(input[t].name, out output))
                   {
                       if(!output)
                       {
                           input[t] = null;
                       }
                   }
               }

               return input;
        }

        public static Store[] parseInfo(Store[] input)
        {
            input = checkForDupilcates(input);
            var query = (from x in input
                            where x != null && x.serialNumber.Contains(getInitials(x.name)) &&
                            x.salePrice > x.cost 
                            orderby (x.salePrice - x.cost) descending
                            select x).ToArray();
            return query;
        }

        private static string getInitials(string input)
        {
            string output = "";
            foreach(char t in input)
            {
                if(Char.IsUpper(t))
                {
                    output += t.ToString();
                }
            }
            return output;
        }
    }

    class Store
    {
        public int id, cost, salePrice;
        public string name, serialNumber, salesPerson;
        
        public Store(int id, string name, string serialNumber, int cost, int salePrice, string salesPerson)
        {
            this.id = id;
            this.name = name;
            this.serialNumber = serialNumber;
            this.cost = cost;
            this.salePrice = salePrice;
            this.salesPerson = salesPerson;
        }    
        public override string ToString()
        {
           return $"id: {id}, name: {name}, serialNumber: {serialNumber}, cost: {cost}, salesPrice: {salePrice}, salesPerson: {salesPerson}"; 
        }
    }

   

}
