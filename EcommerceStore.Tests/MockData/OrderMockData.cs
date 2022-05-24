using EcommerceStore.Application.Models.InputModels;
using EcommerceStore.Application.Models.ViewModels;
using EcommerceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceStore.Tests.MockData
{
    public static class OrderMockData
    {
        public static Order GetOrderWithIsDeletedTrue()
        {
            var order = new Order
            {
                Id = 1,
                ModifiedDate = DateTime.UtcNow,
                Status = "InReview",
                IsDeleted = true,
                UserId = 1,
                User = new User
                {
                    Email = "somemail@gmail.com",
                    PhoneNumber = "123123123"
                }
            };

            return order;
        }

        public static Order GetOrderWithCanceledStatus()
        {
            var order = new Order
            {
                Id = 1,
                ModifiedDate = DateTime.UtcNow,
                Status = "Canceled",
                IsDeleted = true,
                UserId = 1,
                User = new User
                {
                    Email = "somemail@gmail.com",
                    PhoneNumber = "123123123"
                }
            };

            return order;
        }

        public static OrderInputModel GetOrderInputModelForCreating()
        {
            var orderInputModel = new OrderInputModel
            {
                ProductsDetails = new List<ProductDetailsForOrderInputModel>
                {
                    new ProductDetailsForOrderInputModel
                    {
                        ProductAmount = 1,
                        ProductId = 1
                    },
                    new ProductDetailsForOrderInputModel
                    {
                        ProductAmount = 1,
                        ProductId = 2
                    }
                }
            };

            return orderInputModel;
        }
    }
}
