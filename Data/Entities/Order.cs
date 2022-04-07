using System;
using System.Collections.Generic;

namespace EFCoreConsoleApp.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}