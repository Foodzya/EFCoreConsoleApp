using EcommerceStore.Domain.Entities;
using EcommerceStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceStore.Tests.MockData
{
    public static class SeedDatabase
    {
        public static async Task SeedDatabaseWithOrdersAsync(EcommerceContext context)
        {
            await context.Orders.AddRangeAsync(new List<Order>
            {
                new Order
                {
                    Id = 1,
                    IsDeleted = false,
                    ModifiedDate = DateTime.UtcNow,
                    Status = "Created",
                    UserId = 2,
                    User = new User
                    {
                        Id = 2,
                        FirstName = "John",
                        LastName = "Wick",
                        Email = "emailfortest@gmail.com",
                        PhoneNumber = "13371337"
                    },
                    ProductOrders = new List<ProductOrder>()
                },
                new Order
                {
                    Id = 2,
                    IsDeleted = false,
                    ModifiedDate = DateTime.UtcNow,
                    Status = "InDelivery",
                    UserId = 1,
                    User = new User()
                    {
                        Id = 1,
                        FirstName = "Alex",
                        LastName = "Bordson",
                        Email = "secondemailfortest@mail.ru",
                        PhoneNumber = "133712345"
                    },
                    ProductOrders = new List<ProductOrder>()
                }
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedDatabaseWithProductsAsync(EcommerceContext context)
        {
            await context.Products.AddRangeAsync(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Adidas Ozweego",
                    Image = "someimage",
                    Description = "Some description",
                    Price = 100,
                    Quantity = 10,
                    ProductCategoryId = 1,
                    BrandId = 1,
                    Brand = new Brand
                    {
                        Id = 1,
                        FoundationYear = 1990,
                        Name = "Some brand",
                        IsDeleted = false
                    },
                    ProductOrders = new List<ProductOrder>()
                    {
                        new ProductOrder
                        {
                            Id = 2,
                            Quantity = 20,
                            Price = 200
                        }
                    },
                    ProductCategory = new ProductCategory()
                    {
                        Id = 2,
                        Name = "Something else"
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Adidas Superstar",
                    Image = "someimageofsneakers",
                    Description = "Some description",
                    Price = 120,
                    Quantity = 10,
                    ProductCategoryId = 1,
                    BrandId = 1,
                    ProductCategory = new ProductCategory()
                    {
                        Id = 1,
                        Name = "Something"
                    },
                    ProductOrders = new List<ProductOrder>()
                    {
                        new ProductOrder
                        {
                            Id = 1,
                            Quantity = 10,
                            Price = 150
                        }
                    },
                    Brand = new Brand
                    {
                        Id = 2,
                        FoundationYear = 1995,
                        Name = "Some new brand",
                        IsDeleted = false
                    }
                }
            });

            await context.SaveChangesAsync();
        }
    }
}
