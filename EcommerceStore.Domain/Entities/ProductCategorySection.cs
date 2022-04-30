namespace EcommerceStore.Domain.Entities
{
    public class ProductCategorySection
    {
        public int SectionId { get; set; }
        public int ProductCategoryId { get; set; }

        public virtual Section Section { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}