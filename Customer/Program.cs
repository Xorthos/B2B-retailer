using System;

namespace Customer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                OrderDTO orderDTO = new OrderDTO();

                Console.WriteLine("Enter product id.");
                orderDTO.order.ProdID = Console.ReadLine();
                Console.WriteLine("Enter customer id.");
                orderDTO.order.CustID = Console.ReadLine();
                orderDTO.QueueToken = Guid.NewGuid();

                bus.Send("ordering", orderDTO);

                bus.Receive<CustomerResponse>(orderDTO.QueueToken.ToString(), message => Console.WriteLine("Available: {0}, DeliveryTime: {1}, ShippingPrice: {2}", message.Available, message.DeliveryTime, message.ShippingPrice));
            }
        }
    }
}
