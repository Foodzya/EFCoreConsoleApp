namespace EcommerceStore.Application.Models.ViewModels
{
    public class TokenResponseModel
    {
        public string UserEmail { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
