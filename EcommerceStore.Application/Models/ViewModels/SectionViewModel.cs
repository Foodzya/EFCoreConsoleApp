using System.Collections.Generic;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class SectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
    }
}