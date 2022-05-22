using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class RoleInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Role name must be between 1 and 300 characters")]
        public string Name { get; set; }
    }
}