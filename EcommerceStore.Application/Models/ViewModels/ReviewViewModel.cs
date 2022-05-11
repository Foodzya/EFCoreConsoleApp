namespace EcommerceStore.Application.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string ProductName { get; set; }
        public string CustomerFullName { get; set; }
    }
}