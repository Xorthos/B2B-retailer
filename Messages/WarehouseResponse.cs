using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class WarehouseResponse
    {
        public int Stock { get; set; }

        public int DeliveryTime { get; set; }

        public float ShippingPrice { get; set; }
    }
}
