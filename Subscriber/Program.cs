using System;
using EasyNetQ;
using BE;

namespace Subscriber
{
    public class Program
    {
        public static Warehouse warehouse;


        static void Main(string[] args)
        {

            DummyData dummy = new DummyData();

            Console.WriteLine("Enter warehouse country.");
            warehouse = dummy.getDummyWarehouses(Console.ReadLine());

            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Subscribe<Order>("WarehouseRequest", HandleRequest);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleRequest(Order order)
        {
            int time;
            int price;
            bool available = false;
            foreach (Product prod in warehouse.Products)
            {
                if(prod.ProdID == order.ProdID)
                {
                    if (order.country.Equals(warehouse.Country))
                    {
                        time = 2;
                        price = 5;
                    } else
                    {
                        time = 10;
                        price = 10;
                    }
                    available = true;
                }
            }

            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Subscribe<Order>("WarehouseRequest", HandleRequest);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }




        }
    }
}