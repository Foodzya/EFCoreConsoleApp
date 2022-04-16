using EcommerceStore.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EcommerceStore.Queries
{
    public class EagerLoadingQuery
    {
        public void ProductsByBrandName()
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                string brandName = "Adidas";

                var products = context.Products
                    .Include(product => product.Brand)
                    .Where(prod => prod.Brand.Name == brandName)
                    .Select(p => new { p.Name, p.Price, p.Quantity });

                foreach (var product in products)
                {
                    Console.WriteLine(product.Name);
                }
            }
        }

        public void BrandsWithProductsByDesc()
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                var brandsWithProducts = context.Products
                    .Include(p => p.Brand)
                    .GroupBy(p => new { p.Brand.Name, p.Brand.Id })
                    .OrderByDescending(g => g.Count())
                    .Select(g => new
                    {
                        BrandName = g.Key.Name,
                        BrandCount = g.Count()
                    });
            }
        }

        public void ProductsBySectionAndCategory()
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                string productCategoryName = "Sneakers";
                string sectionName = "Man";

                var products = context.Products
                        .Include(p => p.ProductCategory)
                            .ThenInclude(pc => pc.ProductCategorySections
                                    .Where(pcs => pcs.Section.Name == sectionName))
                                .ThenInclude(pcs => pcs.Section)
                        .Where(p => p.ProductCategory.ProductCategorySections.Any(pcs => pcs.Section.Name == sectionName && pcs.ProductCategory.Name == productCategoryName))
                        .Select(p => new { ProductName = p.Name, ProductQuantity = p.Quantity });

                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductName} {product.ProductQuantity}");
                }
            }
        }
        public void CompletedOrdersWithProduct()
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                string productName = "Adidas Ozweego";
                string status = "Completed";

                var orders = context.Orders
                    .Where(o => o.ProductOrders.Any(p => p.Product.Name == productName))
                    .Include(o => o.ProductOrders
                            .Where(p => p.Product.Name == productName))
                        .ThenInclude(p => p.Product)
                    .Where(o => o.Status == status)
                    .OrderByDescending(o => o.ModifiedDate);

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id}, {order.ModifiedDate}, {order.Status}");
                }
            }
        }

        public void ReviewsForProduct()
        {
            using (EcommerceContext context = new())
            {
                var reviews = context.Reviews
                    .Where(r => r.Product.Name == "Adidas Ozweego")
                    .Include(r => r.Product)
                    .Include(r => r.User)
                    .Select(r => new { Rating = r.Rating, Comment = r.Comment, UserName = $"{r.User.FirstName} {r.User.LastName}", UserEmail = r.User.Email });

                foreach (var review in reviews)
                {
                    Console.WriteLine($"{review.UserName}, {review.UserEmail}: {review.Rating}, {review.Comment}");
                }
            }
        }
    }
}