namespace EcommerceStore.Queries.Models
{
    public class ReviewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ProductName { get; set; }
    }
}
