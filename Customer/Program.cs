﻿using BE;
using EasyNetQ;
using MessagingGateway;
using System;
using System.Collections.Generic;

namespace Customer
{
    class Program
    {
        static List<Guid> queues = new List<Guid>();
        static SynchronousMessagingGateway gateway = new SynchronousMessagingGateway();

        static void Main(string[] args)
        {
            while (true)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO.order = new Order();

                Console.WriteLine("Enter product id.");
                orderDTO.order.ProdID = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Enter customer id.");
                orderDTO.order.CustID = Console.ReadLine();
                Console.WriteLine("Enter customer country code.");
                orderDTO.order.country = Console.ReadLine();
                orderDTO.CustomerToken = Guid.NewGuid();
                //bus.Respond<OrderDTO, CustomerResponse>(request => { return new CustomerResponse() { ShippingPrice = 100, DeliveryTime = 20, Available = false }; });
                CustomerResponse response = gateway.SendRequest(orderDTO);
                Console.WriteLine("Available: {0}, DeliveryTime: {1}, ShippingPrice: {2}", response.Available, response.DeliveryTime, response.ShippingPrice);

                Console.ReadLine();
            }
        }
    }
}
