
namespace FastFood.DataProcessor
{
    using System;
    using FastFood.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
    using Newtonsoft.Json;
    using FastFood.DataProcessor.Dto;
    using System.Text;
    using System.Linq;
    using FastFood.Models;
    using FastFood.DataProcessor.Dto.Import;
    using System.IO;
    using System.Xml.Serialization;
    using System.Globalization;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Xml.Linq;
    using FastFood.Models.Enums;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var dObjs = JsonConvert.DeserializeAnonymousType(jsonString, new[]
            {
                new
                {
                    Name = String.Empty,
                    Age = 0,
                    Position = String.Empty
                }

            });

            var employees = new List<Employee>();
            var sb = new StringBuilder();

            foreach (var e in dObjs)
            {
                if (String.IsNullOrEmpty(e.Name) || e.Name.Length < 3 || e.Name.Length > 30 || e.Age < 15 || e.Age > 80 || e.Position.Length < 3 || e.Position.Length > 30)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var positionExist = context.Positions.Any(p => p.Name == e.Position);

                if (!positionExist)
                {
                    context.Positions.Add(new Position() { Name = e.Position });
                    context.SaveChanges();
                }

                var position = context.Positions.FirstOrDefault(p => p.Name == e.Position);

                var newEmployee = new Employee()
                {
                    Name = e.Name,
                    Age = e.Age,
                    Position = position
                };

                employees.Add(newEmployee);
                sb.AppendLine(String.Format(SuccessMessage, e.Name));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().Trim();

            //------------------------------------------------------------------------------------
            //var result = new List<string>();

            //var objects = JsonConvert.DeserializeAnonymousType(jsonString, new[] { new { Name = String.Empty, Age = 0, Position = String.Empty } });

            //foreach (var obj in objects)
            //{
            //    if (obj.Name == null || obj.Position == null || obj.Name.Length < 3 || obj.Name.Length > 30 || obj.Age < 15 || obj.Age > 80 || obj.Position.Length < 3 || obj.Position.Length > 30)
            //    {
            //        result.Add(FailureMessage);
            //        continue;
            //    }


            //    bool positionExists = context.Positions
            //        .Any(p => p.Name == obj.Position);

            //    if (!positionExists)
            //    {
            //        context.Positions.Add(new Position { Name = obj.Position });
            //        context.SaveChanges();
            //    }

            //    var position = context.Positions.FirstOrDefault(p => p.Name == obj.Position);

            //    context.Employees.Add(new Employee { Age = obj.Age, Name = obj.Name, Position = position });
            //    context.SaveChanges();
            //    result.Add(String.Format(SuccessMessage, obj.Name));
            //}

            //return String.Join(Environment.NewLine, result);        
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            //var result = new List<string>();

            //var objects = JsonConvert.DeserializeAnonymousType(jsonString, new[] { new { Name = String.Empty, Price = 0.00m, Category = String.Empty } });

            //foreach (var obj in objects)
            //{
            //    bool itemExists = context.Items.Any(i => i.Name == obj.Name);

            //    if (itemExists || obj.Name.Length < 3 || obj.Name.Length > 30 || obj.Price < 0.01m || obj.Category.Length < 3 || obj.Category.Length > 30)
            //    {
            //        result.Add(FailureMessage);
            //        continue;
            //    }

            //    bool categoryExists = context.Categories.Any(c => c.Name == obj.Category);

            //    if (!categoryExists)
            //    {
            //        context.Categories.Add(new Category { Name = obj.Category });
            //        context.SaveChanges();
            //    }

            //    var category = context.Categories.SingleOrDefault(c => c.Name == obj.Category);

            //    context.Items.Add(new Item { Category = category, Name = obj.Name, Price = obj.Price });
            //    context.SaveChanges();

            //    result.Add(String.Format(SuccessMessage, obj.Name));
            //}

            //return String.Join(Environment.NewLine, result);



            //-----------------------------------------------------------------------------------------------

            var deserializeItems = JsonConvert.DeserializeAnonymousType(jsonString, new[]
            {
                new
                {
                    Name = String.Empty,
                    Price = 0.00m,
                    Category = String.Empty
                }
            });

            var itemsList = new List<Item>();
            var sb = new StringBuilder();

            foreach (var obj in deserializeItems)
            {
                var itemExist = context.Items.Any(it => it.Name == obj.Name);

                if (itemExist || obj.Name.Length < 3 || obj.Name.Length > 30 || obj.Price < 0.01m || obj.Category.Length < 3 || obj.Category.Length > 30)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var categoryExist = context.Categories.Any(c => c.Name == obj.Category);

                if (!categoryExist)
                {
                    context.Categories.Add(new Category() { Name = obj.Category });
                    context.SaveChanges();
                }

                var category = context.Categories.SingleOrDefault(c => c.Name == obj.Category);

                var newItem = new Item()
                {
                    Category = category,
                    Name = obj.Name,
                    Price = obj.Price,
                };

                context.Items.Add(newItem);
                context.SaveChanges();

                sb.AppendLine(String.Format(SuccessMessage, obj.Name));
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            foreach (var e in elements)
            {
                var customer = e.Element("Customer").Value;
                var employee = e.Element("Employee").Value;
                var dateTime = e.Element("DateTime").Value;
                var type = e.Element("Type").Value;

                if(customer == null || employee == null || dateTime == null || type == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var currentEmployee = context.Employees.FirstOrDefault(o => o.Name == employee);

                if (currentEmployee == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var currentDateTime = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                Object typeObj;
                var IsValidType = Enum.TryParse(typeof(OrderType), type, out typeObj);

                if (!IsValidType)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var currentType = (OrderType)typeObj;

                var isValidItems = true;
                var listItems = new List<ItemDto>();

                foreach (var itemElement in e.Element("Items").Elements())
                {
                    var itemName = itemElement.Element("Name")?.Value;
                    var quantity = itemElement.Element("Quantity")?.Value;

                    if(itemName == null || quantity == null)
                    {
                        sb.AppendLine(FailureMessage);
                        isValidItems = false;
                    }

                    var currentQuantity = int.Parse(quantity);
                    var itemExist = context.Items.Any(it => it.Name == itemName);

                    if(!itemExist || currentQuantity <= 0)
                    {
                        sb.AppendLine(FailureMessage);
                        isValidItems = false;
                    }

                    listItems.Add(new ItemDto() { Name = itemName, Quantity = currentQuantity });
                }

                if (!isValidItems)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var order = new Order() { Customer = customer, Employee = currentEmployee, DateTime = currentDateTime, Type = currentType };
                context.Orders.Add(order);

                foreach (var dto in listItems)
                {
                    var item = context.Items.FirstOrDefault(it => it.Name == dto.Name);
                    context.OrderItems.Add( new OrderItem() { Item = item, Order = order, Quantity = dto.Quantity });
                }

                context.SaveChanges();

                sb.AppendLine($"Order for {customer} on {dateTime} added");
            }

            return sb.ToString().Trim();
        }
    }
}