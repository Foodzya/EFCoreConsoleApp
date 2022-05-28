namespace EcommerceStore.Queries.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string SectionName { get; set; }
        public string CategoryName { get; set; }
    }
}
