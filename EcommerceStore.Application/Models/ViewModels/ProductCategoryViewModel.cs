using System.Collections.Generic;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class ProductCategoryViewModel
    {
        public string Name { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}