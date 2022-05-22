using System.Collections.Generic;

namespace EcommerceStore.Domain.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<ProductCategorySection> ProductCategorySections { get; set; }
    }
}