using System;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class OrderViewModel
    {
        public string Status { get; set; }
        public string CustomerFullName { get; set; }
        public List<string> ProductNames { get; set; }
        public double TotalPrice { get; set; }
    }
}