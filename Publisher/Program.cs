using System;
using EasyNetQ;
using BE;
using System.Collections.Generic;
using System.Threading;

namespace Server
{
    class Program
    {
        static List<Guid> queues = new List<Guid>();
        private static int timeoutInterval = 5000;

        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            {
                
                    bus.Respond<OrderDTO, CustomerResponse>(request => HandleOrder(request));
                

                Console.ReadLine();

            }
        }

        static CustomerResponse HandleOrder(OrderDTO orderDTO)
        {

            CustomerResponse response = new CustomerResponse();
            response.ShippingPrice = 10000;

                Console.WriteLine("Got order!!!!");

                using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
                {
                    orderDTO.WarehouseToken = Guid.NewGuid();

                    bus.Publish<OrderDTO>(orderDTO, "WarehouseRequest");

                    Console.WriteLine("Sent for warehouses, waiting for response.");
                    


                    bus.Subscribe<WarehouseResponse>(orderDTO.WarehouseToken.ToString(), message =>
                    {
                        CustomerResponse custResponse = new CustomerResponse();
                        if (message.Stock >= 1)
                        {
                            custResponse.Available = true;
                            custResponse.DeliveryTime = message.DeliveryTime;
                            custResponse.ShippingPrice = message.ShippingPrice;
                        }
                        response = custResponse;
                        Console.WriteLine(response.Available);
                        Console.WriteLine(response.DeliveryTime);
                        Console.WriteLine(response.ShippingPrice);

                    });

                Timer timer = new Timer(writeSomething, null, timeoutInterval, Timeout.Infinite);
            }

            Console.WriteLine("Sent response to customer.");
            return response;
            
        }

        public static void writeSomething(object message)
        {
            Console.WriteLine("Stuff broke....");
        }
    }
}