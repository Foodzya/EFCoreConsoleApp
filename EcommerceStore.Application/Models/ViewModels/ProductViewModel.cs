namespace EcommerceStore.Application.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string BrandName { get; set; }
        public string ProductCategoryName { get; set; }
    }
}