using System;
using EasyNetQ;
using BE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static List<Guid> queues = new List<Guid>();
        static MessageTranslator mt = new MessageTranslator();
        private static int timeoutInterval = 5000;

        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            {
                bus.RespondAsync<OrderDTO, CustomerResponse>(request => HandleOrder(request));

                Console.ReadLine();

            }
        }

        static async Task<CustomerResponse> HandleOrder(OrderDTO orderDTO)
        {

            List<CustomerResponse> warehouseResponses = new List<CustomerResponse>();

            Console.WriteLine("Got order!!!!");

            using (var bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false"))
            {
                orderDTO.WarehouseToken = Guid.NewGuid();

                bus.Publish<OrderDTO>(orderDTO);

                Console.WriteLine("Sent for warehouses, waiting for response.");


                bus.Subscribe<WarehouseResponse>(orderDTO.WarehouseToken.ToString(), message =>
                {
                    var custResponse = mt.translate(message);
                    warehouseResponses.Add(custResponse);
                });

                await Task.Delay(timeoutInterval);
            }

            CustomerResponse bestResponse = warehouseResponses[0];

            foreach (CustomerResponse res in warehouseResponses)
            {
                if (res.DeliveryTime < bestResponse.DeliveryTime)
                {
                    bestResponse = res;
                }
            }

            Console.WriteLine("Sent response to customer.");
            return bestResponse;

        }

        public static void writeSomething(object message)
        {
            Console.WriteLine("Stuff broke....");
        }
    }
}