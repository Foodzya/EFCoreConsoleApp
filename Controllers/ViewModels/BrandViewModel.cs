using EcommerceStore.Data.Entities;
using System.Collections.Generic;

namespace EcommerceStore.Controllers.ViewModels
{
    public class BrandViewModel
    {
        public string Name { get; set; }
        public int FoundationYear { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
