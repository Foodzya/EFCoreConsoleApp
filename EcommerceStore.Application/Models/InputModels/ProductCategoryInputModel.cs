using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class ProductCategoryInputModel
    {
        [Required]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
