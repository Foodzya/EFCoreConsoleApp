using System.Collections.Generic;

namespace EcommerceStore.Domain.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual ProductCategory ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductCategory> ChildrenCategory { get; set; } 
        public virtual List<ProductCategorySection> ProductCategorySections { get; set; }
    }
}