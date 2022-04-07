using System;
using System.Collections.Generic;

namespace EFCoreConsoleApp.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
