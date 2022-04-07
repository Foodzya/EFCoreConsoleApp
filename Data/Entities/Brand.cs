namespace EFCoreConsoleApp.Data.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoundationYear { get; set; }

        public virtual Product Product { get; set; }
    }
}
