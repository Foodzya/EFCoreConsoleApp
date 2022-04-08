using System.Collections.Generic;

namespace EcommerceStore.Data.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }

        public virtual ProductCategory ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public List<ProductCategorySection> ProductCategorySections { get; set; }
    }
}