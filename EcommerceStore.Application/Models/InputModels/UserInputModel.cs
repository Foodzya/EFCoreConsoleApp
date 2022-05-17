using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class UserInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "User's first name must be between 1 and 300 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "User's last name must be between 1 and 300 characters")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Email must be between 1 and 300 characters")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Phone number must consist of 11 digits")]
        public string PhoneNumber { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Role id must be between 1 and 5")]
        public int RoleId { get; set; }
    }
}