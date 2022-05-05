using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class ProductInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Product's name must be between 1 and 300 characters")]
        public string Name { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be positive floating-point numeric number")]
        public double Price { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Product's description must be between 1 and 500 characters")]
        public string Description { get; set; }
        [Required]
        [Url]
        public string Image { get; set; }
    }
}