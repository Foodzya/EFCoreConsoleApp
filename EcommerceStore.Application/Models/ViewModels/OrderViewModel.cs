using System;

namespace EcommerceStore.Application.Models.ViewModels
{
    public class OrderViewModel
    {
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }
        public string CustomerFullName { get; set; }
    }
}