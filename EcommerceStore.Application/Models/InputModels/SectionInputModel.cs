using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class SectionInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Section name must be between 1 and 300 characters")]
        public string Name { get; set; }
    }
}