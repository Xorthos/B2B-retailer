using System;
using EasyNetQ;
using BE;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                OrderDTO orderDTO = new OrderDTO();

                Order order;
                Guid token;

                bus.Receive<OrderDTO>("ordering", message => { order = message.order; token = message.QueueToken; });


            }
        }
    }
}