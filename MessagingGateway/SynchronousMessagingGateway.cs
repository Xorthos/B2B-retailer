using BE;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingGateway
{
    public class SynchronousMessagingGateway
    {
        IBus bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false");

        public CustomerResponse SendRequest(OrderDTO orderDTO)
        {
            return bus.Request<OrderDTO, CustomerResponse>(orderDTO);
        }
    }
}
