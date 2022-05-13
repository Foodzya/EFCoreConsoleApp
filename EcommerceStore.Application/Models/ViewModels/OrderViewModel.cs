using System;
using System.Collections.Generic;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string CustomerFullName { get; set; }
        public List<string> ProductNames { get; set; }
        public double TotalPrice { get; set; }
    }
}