using System.Collections.Generic;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Subcategories { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}