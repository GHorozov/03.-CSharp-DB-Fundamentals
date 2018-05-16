namespace ProductShop.App
{
    using System;
    using ProductShop.Data;

    using Newtonsoft.Json;
    using System.IO;
    using ProductShop.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            //JSON
            //Console.WriteLine(ImportUsersFromJson());
            //Console.WriteLine(ImportCategoriesFromJson());
            //Console.WriteLine(ImportProductsFromJson());
            // Console.WriteLine(SetCategories());

            //GetProductsInRange();
            //SucessfullySoldProducts();
            //CategoryByProductCount();
            //UsersAndProducts();

            //XML
            //ImportUsersFromXml();
            //ImportCategoriesFromXml();
            //ImportProductsFromXml();
            //SetCategories();
            //ProductInRangeXml();
            //SoldProductsXml();
            //CategoryByProductCountXml();
            //UsersAndProductsXml();
        }

        static void UsersAndProductsXml()
        {
            var context = new ProductShopContext();

            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    ProductSold = u.ProductsSold.Select(p => new
                        {
                            p.Name,
                            p.Price
                        })
                })
                .ToArray();


            var xDoc = new XDocument();
            xDoc.Add(new XElement("users", new XAttribute("count", users.Count())));

            foreach (var u in users)
            {
                var element = new XElement("users");

                if (u.FirstName != null)
                {
                    element.Add(new XAttribute("first-name", u.FirstName));
                }

                element.Add(new XAttribute("last-name", u.LastName));

                if(u.Age != null)
                {
                    element.Add(new XAttribute("age", u.Age));
                }
               
                var soldProductElement = new XElement("sold-products", new XAttribute("count", u.ProductSold.Count()));
                foreach (var p in u.ProductSold)
                {
                    soldProductElement.Add(new XElement("product",
                        new XAttribute("name", p.Name),
                        new XAttribute("price", p.Price)));
                }

                element.Add(soldProductElement);
                xDoc.Root.Add(element);
            }

            xDoc.Save("UsersAndProductsXml.xml");
        }

        static void CategoryByProductCountXml()
        {
            var context = new ProductShopContext();

            var categories = context.Categories
                .OrderBy(c => c.Products.Count)
                .Select(c => new
                {
                    c.Name,
                    NumberOfProducts = c.Products.Count,
                    AveragePriceOfProducts = c.Products.Average(p => p.Product.Price),
                    TotalRvenue = c.Products.Sum(p => p.Product.Price)
                })
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("categories"));

            foreach (var c in categories)
            {
                xDoc.Root.Add(new XElement("category", new XAttribute("name", c.Name),
                    new XElement("products-count", c.NumberOfProducts),
                    new XElement("average-price", c.AveragePriceOfProducts),
                    new XElement("total-revenue", c.TotalRvenue)));


            }

            xDoc.Save("CategoryByProductCount.xml");
        }

        static void SoldProductsXml()
        {
            var context = new ProductShopContext();

            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    SoldProducts = u.ProductsSold.Select(p => new
                    {
                        p.Name,
                        p.Price,
                    })
                })
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));

            foreach (var u in users)
            {
                var element = new XElement("user");

                if (u.FirstName != null)
                {
                    element.Add(new XAttribute("first-name", u.FirstName));
                }

                element.Add(new XAttribute("last-name", u.LastName));

                xDoc.Root.Add(element);

                var element2 = new XElement("sold-products");


                foreach (var p in u.SoldProducts)
                {
                    element2.Add(new XElement("product",
                    new XElement("name", p.Name),
                    new XElement("price", p.Price)));
                }

                xDoc.Root.Add(element2);
            }

            xDoc.Save("SoldProducts.xml");
        }

        static void ProductInRangeXml()
        {
            var context = new ProductShopContext();

            var products = context.Products
                .Where(p => p.BuyerId != null && p.Price >= 1000 && p.Price <= 2000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    BuyerName = $"{p.Buyer.FirstName} {p.Buyer.LastName}",
                })
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("products")); //Root

            foreach (var p in products)
            {
                var element = new XElement("product",
                    new XAttribute("name", p.Name),
                    new XAttribute("price", p.Price),
                    new XAttribute("buyer", p.BuyerName));

                xDoc.Root.Add(element);
            }

            xDoc.Save("ProductsInRange.xml");

            //File.WriteAllText("ProductsInRange.xml", xDoc.ToString());
        }

        static string ImportProductsFromXml()
        {
            var path = "products.xml";
            var xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();

            var context = new ProductShopContext();
            var products = new List<Product>();

            var userIds = context.Users.Select(u => u.UserId).ToArray();
            var categoryIds = context.Categories.Select(c => c.CategoryId).ToArray();

            var rnd = new Random();

            foreach (var e in elements)
            {
                var name = e.Element("name").Value;
                var price = decimal.Parse(e.Element("price").Value);

                var index = rnd.Next(0, userIds.Length);
                var sellerId = userIds[index];

                int? buyerId = sellerId;
                while (buyerId == sellerId)
                {
                    var byuerIndex = rnd.Next(0, userIds.Length);
                    buyerId = userIds[byuerIndex];
                }

                if (buyerId - sellerId > 5 && buyerId - sellerId > 0)
                {
                    buyerId = null;
                }

                var product = new Product()
                {
                    Name = name,
                    Price = price,
                    SellerId = sellerId,
                    BuyerId = buyerId
                };

                products.Add(product);
            }

            context.AddRange(products);
            context.SaveChanges();

            return $"{products.Count} were added sucessfully from {path}.";
        }

        static string ImportCategoriesFromXml()
        {
            var path = "categories.xml";
            var xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();

            var categories = new List<Category>();

            foreach (var e in elements)
            {
                var category = new Category()
                {
                    Name = e.Element("name").Value //Takes element not attribute
                };

                categories.Add(category);
            }

            var context = new ProductShopContext();
            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"{categories.Count} were added sucessfully from {path}.";
        }

        static string ImportUsersFromXml()
        {
            string xmlString = File.ReadAllText("users.xml");
            var xmlDoc = XDocument.Parse(xmlString); //Read xml from string
            var elements = xmlDoc.Root.Elements();

            var users = new List<User>();

            foreach (var e in elements)
            {
                var firstName = e.Attribute("FirstName")?.Value; //? operator if null dosnt take attribute.
                var lastName = e.Attribute("lastName")?.Value;

                int? age = null;
                if (e.Attribute("age") != null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                };

                users.Add(user);
            }

            var context = new ProductShopContext();
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"{users.Count} users were sucessfully added from users.xml.";
        }

        //JSON
        static void UsersAndProducts()
        {
            var context = new ProductShopContext();

            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = u.ProductsSold.Select(p => new
                    {
                        Count = u.ProductsSold.Count(),
                        p.Name,
                        p.Price
                    })
                })
                .ToArray();

            var stringJson = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            File.WriteAllText("UsersAndProducts.json", stringJson);

        }

        static void CategoryByProductCount()
        {
            var context = new ProductShopContext();

            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    ProductsCount = c.Products.Count,
                    AveragePrice = c.Products.Average(p => p.Product.Price),
                    TotalRevenue = c.Products.Sum(p => p.Product.Price)
                })
                .ToArray();

            var stringJson = JsonConvert.SerializeObject(categories, Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            File.WriteAllText("CategoryByProductCount.json", stringJson);
        }

        static void SucessfullySoldProducts()
        {
            var context = new ProductShopContext();

            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    SoldProducts = u.ProductsSold.Select(p => new
                    {
                        p.Name,
                        p.Price,
                        BuyerFirstName = p.Buyer.FirstName,
                        BuyerLastName = p.Buyer.LastName,
                    })
                })
                .ToArray();


            var stringJson = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            File.WriteAllText("SucessfullySoldProducts.json", stringJson);
        }

        static void GetProductsInRange()
        {
            var context = new ProductShopContext();

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderByDescending(p => p.Price)
                .Select(p => new { p.Name, p.Price, Seller = $"{p.Seller.FirstName} {p.Seller.LastName}" })
                .ToArray();

            var stringJson = JsonConvert.SerializeObject(products, Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            File.WriteAllText("PricesInRange.json", stringJson);
        }

        static string SetCategories()
        {
            var context = new ProductShopContext();

            var productsIds = context.Products.Select(p => p.ProductId).ToArray();
            var categoriesIds = context.Categories.Select(c => c.CategoryId).ToArray();

            var rnd = new Random();
            var listCategoryProducts = new List<CategoryProduct>();

            foreach (var p in productsIds)
            {
                //Puts on each productId 3 categoryId
                for (int i = 0; i < 3; i++)
                {
                    var randomCategoryIdIndex = rnd.Next(0, categoriesIds.Length);
                    while (listCategoryProducts.Any(cp => cp.ProductId == p && cp.CategoryId == categoriesIds[randomCategoryIdIndex]))
                    {
                        randomCategoryIdIndex = rnd.Next(0, categoriesIds.Length);
                    }

                    var catProd = new CategoryProduct
                    {
                        ProductId = p, //p is index from productsIds
                        CategoryId = categoriesIds[randomCategoryIdIndex],
                    };

                    listCategoryProducts.Add(catProd);
                }
            }

            context.CategoryProducts.AddRange(listCategoryProducts);
            context.SaveChanges();

            return $"{listCategoryProducts.Count} categoryProducts were added.";
        }

        static string ImportCategoriesFromJson()
        {
            var path = "categories.json";
            Category[] categories = ImportJson<Category>(path);
            var context = new ProductShopContext();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"{categories.Length} categoties were imported from {path}.";
        }

        static string ImportProductsFromJson()
        {
            var path = "products.json";
            Product[] products = ImportJson<Product>(path);

            using (var context = new ProductShopContext())
            {
                Random rnd = new Random();

                foreach (var p in products)
                {
                    var usersId = context.Users.Select(u => u.UserId).ToArray();
                    var index = rnd.Next(0, usersId.Length);
                    var sellerId = usersId[index];

                    //Randomly set id to buyerId
                    int? buyerId = sellerId;
                    while (buyerId == sellerId)
                    {
                        int buyerIndex = rnd.Next(0, usersId.Length);
                        buyerId = usersId[buyerIndex];
                    }

                    if (buyerId - sellerId > 5 && buyerId - sellerId > 0)
                    {
                        buyerId = null;
                    }

                    //Add id to sellerId and id to buyerId
                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            return $"{products.Length} products were imported from {path}.";
        }

        static string ImportUsersFromJson()
        {
            var path = "users.json";
            User[] users = ImportJson<User>(path);
            var context = new ProductShopContext();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"{users.Length} users were imported from {path}.";
        }

        static T[] ImportJson<T>(string path)
        {
            var stringJson = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(stringJson);

            return objects;
        }
    }
}
