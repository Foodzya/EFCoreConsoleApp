using System.Collections.Generic;

namespace EFCoreConsoleApp.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
}
