using EcommerceStore.Infrastucture.Persistence.Context;
using EcommerceStore.Queries.Models;
using System;
using System.Linq;

namespace EcommerceStore.Queries
{
    // Need to use UseLazyLoadingProxies method in OnConfiguring method
    // in EcommerceContext to launch lazy loading queries.
    public class LazyLoadingQuery
    {
        public void ProductsByBrandName()
        {
            using (EcommerceContext context = new())
            {
                var products = context.Products.ToList();

                foreach (var product in products)
                {
                    if (product.Brand.Name == "Adidas")
                        Console.WriteLine($"{product.Name} {product.Price} {product.Quantity}");
                }
            }
        }

        public void BrandsWithProductsByDesc()
        {
            using (EcommerceContext context = new())
            {
                var brandsWithProducts = context.Products
                    .GroupBy(p => new { p.Brand.Name, p.Brand.Id })
                    .OrderByDescending(g => g.Count())
                    .Select(g => new BrandModel
                    {
                        Name = g.Key.Name,
                        ProductCount = g.Count()
                    });

                foreach (var brand in brandsWithProducts)
                {
                    Console.WriteLine($"{brand.Name} {brand.ProductCount}");
                }
            }
        }

        public void ProductsBySectionAndCategory()
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                string productCategoryName = "Sneakers";
                string sectionName = "Man";

                var products = context.Products.ToList();

                var productsBySectionAndCategory = products
                    .Where(p => p.ProductCategory.Name == productCategoryName)
                    .Where(p => p.ProductCategory.ProductCategorySections.Any(pcs => pcs.Section.Name == sectionName))
                    .Select(p => new ProductModel
                    {
                        Name = p.Name,
                        Quantity = p.Quantity,
                        CategoryName = p.ProductCategory.Name
                    });

                foreach (var product in productsBySectionAndCategory)
                {
                    Console.WriteLine($"{product.Name} {product.Quantity} {product.CategoryName}");
                }
            }
        }

        public void CompletedOrdersWithProduct()
        {
            using (EcommerceContext context = new())
            {
                var orders = context.Orders.ToList();

                var completedOrdersWithProduct = orders
                    .Where(o => o.Status == "Completed")
                    .Where(o => o.ProductOrders.Any(p => p.Product.Name == "Adidas Ozweego"))
                    .OrderByDescending(o => o.ModifiedDate)
                    .Select(o => new OrderModel
                    {
                        Status = o.Status,
                        ModifiedDate = o.ModifiedDate,
                        UserId = o.UserId
                    });

                foreach (var order in completedOrdersWithProduct)
                {
                    Console.WriteLine($"{order.Status} {order.ModifiedDate} {order.UserId}");
                }
            }
        }

        public void ReviewsForProduct()
        {
            using (EcommerceContext context = new())
            {
                var reviews = context.Reviews.ToList();

                var reviewsForProduct = reviews
                    .Where(r => r.Product.Name == "Adidas Ozweego")
                    .Select(r => new ReviewModel
                    {
                        Rating = r.Rating,
                        Comment = r.Comment,
                        UserName = $"{r.User.FirstName} {r.User.LastName}",
                        UserEmail = r.User.Email,
                        ProductName = r.Product.Name
                    });

                foreach (var review in reviewsForProduct)
                {
                    Console.WriteLine($"{review.UserName}, {review.UserEmail}: {review.Rating}, {review.Comment}, {review.ProductName}");
                }
            }
        }
    }
}