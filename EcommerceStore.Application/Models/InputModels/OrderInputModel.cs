using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class OrderInputModel
    {
        [Required]
        public List<ProductDetailsForOrderInputModel> ProductsDetails { get; set; }
    }
}