using System.Collections.Generic;

namespace EcommerceStore.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int BrandId { get; set; }
        public int ProductCategoryId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}