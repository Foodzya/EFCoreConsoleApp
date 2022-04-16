namespace EcommerceStore.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}