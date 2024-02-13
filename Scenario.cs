using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;


namespace Matthew_Abramowicz_Unit6_IT481
{
    class Scenario
    {
        static Customer cust;
        static int items = 0;
        static int numberOfItems;
        static int controlItemNumber;

        public Scenario(int r, int c)
        {
            Console.WriteLine(r + " dressing rooms " + " for " + c + " customers.");
            controlItemNumber = 0;
            numberOfItems = 0;
        }
        static void Main(string[] args)
        {
            Console.Write("What ClothingItems value would you like? (0 = random)");
            controlItemNumber = Int32.Parse(Console.ReadLine());

            Console.Write("How many customers would you like? ");
            int numberOfCustomers = Int32.Parse(Console.ReadLine());
            Console.WriteLine("There are " + numberOfCustomers + " total customers!");

            Console.Write("How many dressing rooms would you like? ");
            int totalRooms = Int32.Parse(Console.ReadLine());

            Scenario s = new Scenario(totalRooms, numberOfCustomers);
            DressingRooms dr = new DressingRooms(totalRooms);

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < numberOfCustomers; i++)
            {
                cust = new Customer(controlItemNumber);
                numberOfItems = cust.getNumberOfItems();
                items += numberOfItems;
                tasks.Add(Task.Factory.StartNew(async () =>
                {
                    await dr.RequestRoom(cust);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Average runtime in miliseconds {0} ", dr.getRunTime() / numberOfCustomers);
            Console.WriteLine("Average wait time in miliseconds {0} ", dr.getWaitTime() / numberOfCustomers);
            Console.WriteLine("Total customers is {0}", numberOfCustomers);
            int averageItemsPerCustomer = items / numberOfCustomers;
            Console.WriteLine("Average number of items was " + averageItemsPerCustomer);
            Console.Read();
        }
    }
}

