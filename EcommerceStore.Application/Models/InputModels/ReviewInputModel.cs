using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class ReviewInputModel
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Comment length must be between 10 and 500 characters")]
        public string Comment { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product id must be positive integer number")]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "User id must be positive integer number")]
        public int UserId { get; set; }
    }
}