using System;
using FastFood.Data;
using System.Linq;
using System.Text;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
        public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
        {
            var item = context.Items.FirstOrDefault(i => i.Name == itemName);

            if (item == null)
            {
                var msg = ($"Item {itemName} not found!");
                return msg;
            }

            var oldPrice = item.Price;
            item.Price = newPrice;
            context.SaveChanges();

           var msg1 = $"{item.Name} Price updated from ${oldPrice:f2} to ${newPrice:f2}";
            return msg1;
        }
    }
}
