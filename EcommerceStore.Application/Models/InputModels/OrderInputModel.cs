using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class OrderInputModel
    {
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "Status cannot exceed 11 characters")]
        [RegularExpression("In Review|In Delivery|Completed", ErrorMessage = "Order status must be either 'In Review', 'In Delivery' or 'Completed' only")]
        public string Status { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}