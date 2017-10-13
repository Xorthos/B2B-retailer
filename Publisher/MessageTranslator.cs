using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace Server
{
    public class MessageTranslator
    {
        public CustomerResponse translate(WarehouseResponse message)
        {
            CustomerResponse custResponse = new CustomerResponse();
            if (message.Stock >= 1)
            {
                custResponse.Available = true;
                custResponse.DeliveryTime = message.DeliveryTime;
                custResponse.ShippingPrice = message.ShippingPrice;
            }
            return custResponse;
        }

            
}
}
