using EcommerceStore.Controllers.InputModels;
using EcommerceStore.Controllers.ViewModels;
using EcommerceStore.Data.Entities;
using System;

namespace EcommerceStore.Controllers.ExtensionMappers
{
    public static class ExtensionMappers
    {
        public static Brand MapToBrand(this BrandInputModel inputModel)
        {
            if (inputModel != null)
            {
                var brand = new Brand
                {
                    Name = inputModel.Name,
                    FoundationYear = inputModel.FoundationYear
                };

                return brand;
            }

            throw new NullReferenceException("Brand input model was null");
        }

        public static BrandViewModel MapToBrandView(this Brand brand)
        {
            if (brand != null)
            {
                var brandView = new BrandViewModel
                {
                    Name = brand.Name,
                    FoundationYear = brand.FoundationYear,
                    Products = brand.Products
                };

                return brandView;
            }

            throw new NullReferenceException("Brand was null");
        }
    }
}