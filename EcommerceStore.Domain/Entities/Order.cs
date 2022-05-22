using System;
using System.Collections.Generic;

namespace EcommerceStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}