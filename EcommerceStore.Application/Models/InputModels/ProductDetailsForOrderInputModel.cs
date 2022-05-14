using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class ProductDetailsForOrderInputModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product id must be an integer starting from 1")]
        public int ProductId { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Product amount must be positive integer")]
        public int ProductAmount { get; set; }
    }
}