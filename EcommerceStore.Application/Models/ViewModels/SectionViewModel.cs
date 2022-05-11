using System.Collections.Generic;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class SectionViewModel
    {
        public string Name { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
    }
}