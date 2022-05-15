using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Application.Models.InputModels
{
    public class AddressInputModel
    {
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Street Address must be between 1 and 300 characters")]
        public string StreetAddress { get; set; }
        [Required]
        public int PostCode { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "City name must be between 1 and 300 characters")]
        public string City { get; set; }
    }
}