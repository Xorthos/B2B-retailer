using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class CustomerResponse
    {
        public bool Available { get; set; }

        public int? OrderID { get; set; }

        public int DeliveryTime { get; set; }

        public float ShippingPrice { get; set; }
    }
}
