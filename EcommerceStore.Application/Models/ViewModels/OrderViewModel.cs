using System;
using System.Collections.Generic;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string CustomerFullName { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}