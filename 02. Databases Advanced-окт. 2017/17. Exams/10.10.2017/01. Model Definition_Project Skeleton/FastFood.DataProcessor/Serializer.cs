using System;
using System.IO;
using FastFood.Data;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using FastFood.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Collections.Generic;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{
            object objOrderType;
            var isValidOrderType = Enum.TryParse(typeof(OrderType), orderType, out objOrderType);
            
            var orders = context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Item)
                .Where(o => o.Employee.Name == employeeName && o.Type == (OrderType)objOrderType)
                .Select(o => new
                {
                    Customer = o.Customer,
                    Items = o.OrderItems.Select(oi => new
                    {
                        Name = oi.Item.Name,
                        Price = oi.Item.Price,
                        Quantity = oi.Quantity,
                    }),
                    TotalPrice = o.OrderItems.Sum(oi => oi.Quantity * oi.Item.Price) 
                })
                .OrderByDescending(o => o.TotalPrice)
                .ThenByDescending(o => o.Items.Count())
                .ToArray();

            var result = new
            {
                Name = employeeName,
                Orders = orders,
                TotalMade = orders.Sum(oi => oi.TotalPrice)
            };

            var json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

            return json;

        }

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            var categotiesArray = context.Categories
                .Include(i => i.Items)
                .ThenInclude(oi => oi.OrderItems)
                .Where(c => categoriesString.Contains(c.Name)).ToArray();

            var categories = new List<CategoryDto>();
            foreach (var c in categotiesArray)
            {
                var name = c.Name;

                var itemName = c.Items.OrderByDescending(i => i.OrderItems.Sum(oi => oi.Quantity * i.Price)).First().Name;

                var totalMade = c.Items.OrderByDescending(i => i.OrderItems.Sum(oi => oi.Quantity * i.Price)).First()
                                .OrderItems
                                .Sum(oi => oi.Quantity * oi.Item.Price);

                var timesSold = c.Items.OrderByDescending(i => i.OrderItems.Sum(oi => oi.Quantity * i.Price)).First()
                                .OrderItems
                                .Sum(oi => oi.Quantity);

                categories.Add(new CategoryDto() { Name = name, ItemName = itemName, TotalMade = totalMade, TimesSold = timesSold });
            }

            categories = categories
                .OrderByDescending(c => c.TotalMade)
                .ThenByDescending(c => c.TimesSold)
                .ToList();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("Categories"));

            foreach (var c in categories)
            {
                var category = new XElement("Category");

                category.Add(new XElement("Name", c.Name));
                var mostPop = new XElement("MostPopularItem");

                mostPop.Add(new XElement("Name", c.ItemName));
                mostPop.Add(new XElement("TotalMade", c.TotalMade));
                mostPop.Add(new XElement("TimesSold", c.TimesSold));

                category.Add(mostPop);
                xDoc.Element("Categories").Add(category);
            }

            return xDoc.ToString();
		}
	}
}