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

            using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            {
                bus.Subscribe<OrderDTO>(warehouse.Country, HandleRequest);

                Console.WriteLine("Listening for messages. Hit <return> to quit.");

                Console.ReadLine();
            }
        }

        static void HandleRequest(OrderDTO orderDTO)
        {


            Warehouse currentWH = warehouse;

            Console.WriteLine("Got message from server!");
            WarehouseResponse response = new WarehouseResponse();
            foreach (Product prod in currentWH.Products)
            {
                Console.WriteLine("1");
                if (prod.ProdID == orderDTO.order.ProdID)
                {
                    Console.WriteLine("2");
                    Console.WriteLine(orderDTO.order.country);
                    Console.WriteLine(currentWH.Country);
                    if (orderDTO.order.country.Equals(currentWH.Country))
                    {
                        Console.WriteLine("3");
                        response.DeliveryTime = 2;
                        response.ShippingPrice = 5;
                    }
                    else
                    {
                        Console.WriteLine("4");
                        response.DeliveryTime = 10;
                        response.ShippingPrice = 10;
                    }
                    response.Stock = 1;
                }
            }
            Console.WriteLine("I GET HERE!");
            using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            {
                Console.WriteLine(response.DeliveryTime);
                bus.Publish<WarehouseResponse>(response, orderDTO.WarehouseToken.ToString());
                Console.WriteLine("Response sent to the server");
            }




        }
    }
}