using EcommerceStore.Data.Context;
using EcommerceStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using (EcommerceContext context = new EcommerceContext())
{
    var result = context.Products
        .Include(
            product => product.Brand.Id == product.BrandId)
        .ToList();

    foreach (var item in result)
    {
        Console.WriteLine(item.Name);
    }

    Console.ReadLine();
}