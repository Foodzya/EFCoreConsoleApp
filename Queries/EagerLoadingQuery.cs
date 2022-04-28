using EcommerceStore.Core.Context;
using EcommerceStore.Queries.Models;
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
                    .Select(p => new ProductModel
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Quantity = p.Quantity
                    });

                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name} + {product.Price} + {product.Quantity}");
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
                    .Select(g => new BrandModel
                    {
                        Name = g.Key.Name,
                        ProductCount = g.Count()
                    });
            }
        }

        public void ProductsBySectionAndCategory()
        {
            string productCategoryName = "Sneakers";
            string sectionName = "Man";

            using (EcommerceContext context = new EcommerceContext())
            {
                var products = context.Products
                            .Include(p => p.ProductCategory)
                                .ThenInclude(pc => pc.ProductCategorySections)
                                    .ThenInclude(pcs => pcs.Section)
                            .Where(p => p.ProductCategory.ProductCategorySections.Any(pcs => pcs.Section.Name == sectionName))
                            .Where(p => p.ProductCategory.Name == productCategoryName)
                            .Select(p => new ProductModel
                            {
                                Name = p.Name,
                                Quantity = p.Quantity,
                                Description = p.Description,
                                Price = p.Price,
                                SectionName = p.ProductCategory.ProductCategorySections
                                    .FirstOrDefault(pcs => pcs.ProductCategoryId == p.ProductCategoryId).Section.Name
                            });

                var productsWithoutInclude = context.Products
                            .Where(p => p.ProductCategory.ProductCategorySections.Any(pcs => pcs.Section.Name == sectionName))
                            .Where(p => p.ProductCategory.Name == productCategoryName)
                            .Select(p => new ProductModel
                            {
                                Name = p.Name,
                                Quantity = p.Quantity,
                                Description = p.Description,
                                Price = p.Price,
                                SectionName = p.ProductCategory.ProductCategorySections
                                    .FirstOrDefault(pcs => pcs.ProductCategoryId == p.ProductCategoryId).Section.Name
                            });

                foreach (var product in products)
                {
                    Console.WriteLine($"Product is {product.Name};\n" +
                        $"Quantity is {product.Quantity}\n" +
                        $"Section is {product.SectionName}\n");
                }
            }

            using (EcommerceContext context = new EcommerceContext())
            {
                var products = (from s in context.Sections
                                join pcs in context.ProductCategorySections on s.Id equals pcs.SectionId
                                join pc in context.ProductCategories on pcs.ProductCategoryId equals pc.Id
                                join p in context.Products on pc.Id equals p.ProductCategoryId
                                where s.Name == sectionName && pc.Name == productCategoryName
                                select new
                                {
                                    ProductName = p.Name,
                                    ProductQuantity = p.Quantity,
                                    ProductCategoryName = pc.Name,
                                    SectionName = s.Name
                                }).ToList();
            }
        }
        public void CompletedOrdersWithProduct()
        {
            using (EcommerceContext context = new EcommerceContext())
            {
                string productName = "Adidas Ozweego";
                string status = "Completed";

                var orders = context.Orders
                    .Include(o => o.ProductOrders)
                        .ThenInclude(p => p.Product)
                    .Where(o => o.Status == status)
                    .Where(o => o.ProductOrders.Any(p => p.Product.Name == productName))
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
                    .Include(r => r.Product)
                    .Include(r => r.User)
                    .Where(r => r.Product.Name == "Adidas Superstar")
                    .Select(r => new ReviewModel
                    {
                        Rating = r.Rating,
                        Comment = r.Comment,
                        UserName = $"{r.User.FirstName} {r.User.LastName}",
                        UserEmail = r.User.Email,
                        ProductName = r.Product.Name
                    });

                foreach (var review in reviews)
                {
                    Console.WriteLine($"{review.UserName}, {review.UserEmail}:" +
                        $"\n Product \"{review.ProductName}\": {review.Rating}, {review.Comment}");
                }
            }
        }
    }
}