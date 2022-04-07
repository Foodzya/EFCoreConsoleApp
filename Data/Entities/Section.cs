using System.Collections.Generic;

namespace EFCoreConsoleApp.Data.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductCategorySection> ProductCategorySections { get; set; }
    }
}