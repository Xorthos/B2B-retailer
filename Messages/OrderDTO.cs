using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class OrderDTO
    {
        public Order order { get; set; }

        public Guid CustomerToken { get; set; }

        public Guid WarehouseToken { get; set; }
    }

    public class OrderDTOToWarehouse : OrderDTO
    {

    }
}
