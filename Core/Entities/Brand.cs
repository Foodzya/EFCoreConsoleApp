﻿using System.Collections.Generic;

namespace EcommerceStore.Core.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}