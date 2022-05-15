using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class ProductCategoryInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Product category name must be between 1 and 300 characters")]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Parent category ID must be integer starting from 1")]
        public int? ParentCategoryId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Section id must be integer starting from 1")]
        public int SectionId { get; set; }
    }
}