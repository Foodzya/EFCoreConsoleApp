using EcommerceStore.Domain.Entities;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class ProductCategoryViewModel
    {
        public string Name { get; set; }
        public string ParentCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
