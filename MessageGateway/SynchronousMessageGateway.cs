using BE;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGateway
{
    public class SynchronousMessageGateway
    {
        IBus bus = RabbitHutch.CreateBus("host=localhost;persistentMessages=false");

        public CustomerResponse SendRequest(OrderDTO orderDTO)
        {
            return bus.Request<OrderDTO, CustomerResponse>(orderDTO);
        }
    }
}
