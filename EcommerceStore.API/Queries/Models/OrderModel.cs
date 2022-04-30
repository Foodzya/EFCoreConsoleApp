using System;

namespace EcommerceStore.Queries.Models
{
    public class OrderModel
    {
        public string Status { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserId { get; set; }
    }
}
