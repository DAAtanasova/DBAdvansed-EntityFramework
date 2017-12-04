using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductsShop.Data;
using ProductsShop.Models;

namespace ProductsShop.App
{
    class Program
    {
        static void Main()
        {
            //ResetDatabase();

            ////JSON:
            //Console.WriteLine(ImportUsersInDB());

            //Console.WriteLine(ImportCategoriesInDB());

            //Console.WriteLine(ImportProductsInDB());

            //Console.WriteLine(SetCategoriesToProducts());

            //Console.WriteLine(ProductsInRange()); 

            //Console.WriteLine(SoldProducts());

            //Console.WriteLine(CategoriesByProducts()); 

            //Console.WriteLine(UsersAndProducts()); 

            ////XML:
            //Console.WriteLine(ImportXmlUsers());

            //Console.WriteLine(ImportXmlCategories());

            //Console.WriteLine(ImportXmlProducts());

            //Console.WriteLine(SetCategoriesToProductsXml());

            //Console.WriteLine(XmlProductsInRande());

            //Console.WriteLine(XmlSoldproducts());

            //Console.WriteLine(XmlCategoriesByProducts());

            //Console.WriteLine(XmlUsersAndProducts());
        }

        private static string XmlUsersAndProducts()
        {
            using (var db = new ProductShopContext())
            {
                var usersProducts = db.Users.Where(u => u.SellProducts.Count >= 1)
                    .OrderByDescending(u => u.SellProducts.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = u.SellProducts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                XDeclaration decl = new XDeclaration("1.0", "utf-8", null);
                XDocument xUsersDoc = new XDocument(decl);
                xUsersDoc.Add(new XElement("users",
                    new XAttribute("count", usersProducts.Count())));

                foreach (var user in usersProducts)
                {
                    var userElement = new XElement("user");

                    if(user.FirstName != null)
                    {
                        userElement.Add(new XAttribute("firstName", user.FirstName));
                    }

                    userElement.Add(new XAttribute("lastName", user.LastName));

                    if(user.Age != null)
                    {
                        userElement.Add(new XAttribute("age", user.Age));
                    }

                    XElement soldProducts = new XElement("soldProducts",
                        new XAttribute("count", user.SoldProducts.Count()));

                    foreach (var product in user.SoldProducts)
                    {
                        soldProducts
                            .Add(new XElement("product",
                            new XAttribute("name", product.Name),
                            new XAttribute("price", product.Price)));
                    }

                    userElement.Add(soldProducts);
                    xUsersDoc.Root.Add(userElement);
                }

                string result = xUsersDoc.Declaration + Environment.NewLine + xUsersDoc;
                File.WriteAllText("users-and-products.xml", result);

                return "Successfully created file 'users-and-products.xml'";
            }
        }

        private static string XmlCategoriesByProducts()
        {
            using (var db = new ProductShopContext())
            {
                var categoriesProducts = db.Categories
                    .OrderBy(c => c.CategoryProducts.Count)
                    .Select(c => new
                    {
                        Category = c.Name,
                        ProductCount = c.CategoryProducts.Count,
                        AvaragePrice = c.CategoryProducts
                                        .Select(p => p.Product.Price)
                                        .Average(),
                        TotalRevenue = (c.CategoryProducts
                                         .Where(u => u.Product.Buyer != null)
                                         .Select(p => p.Product.Price))
                                         .Sum()
                    });

                XDeclaration decl = new XDeclaration("1.0", "utf-8", null);
                XDocument xCategoryDoc = new XDocument(decl);
                xCategoryDoc.Add(new XElement("categories"));

                foreach (var category in categoriesProducts)
                {
                    xCategoryDoc.Element("categories")
                        .Add(new XElement("category",
                        new XAttribute("name", category.Category),
                        new XElement("products-count", category.ProductCount),
                        new XElement("avarage-price", category.AvaragePrice),
                        new XElement("total-revenue", category.TotalRevenue)));
                }

                string result = xCategoryDoc.Declaration + Environment.NewLine + xCategoryDoc;
                File.WriteAllText("categories-by-products.xml", result);

                return "Successfully created file 'categories-by-products.xml'";
            }
        }

        private static string XmlSoldproducts()
        {
            using(var db = new ProductShopContext())
            {
                var users = db.Users
                    .Where(p => p.SellProducts.Count >= 1)
                    .OrderBy(n => n.LastName).ThenBy(n => n.FirstName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName?? "",
                        LastName = u.LastName,
                        SoldProducts = u.SellProducts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                XDeclaration declaration = new XDeclaration("1.0", "utf-8", null);
                XDocument xUsersDoc = new XDocument(declaration);
                xUsersDoc.Add(new XElement("users"));

                foreach (var user in users)
                {
                    XElement soldProducts = new XElement("soldProducts");
                    foreach (var sp in user.SoldProducts)
                    {
                        soldProducts
                            .Add(new XElement("product",
                            new XElement("name", sp.Name),
                            new XElement("price", sp.Price)));
                    }
                    xUsersDoc.Element("users")
                        .Add(new XElement("user",
                        new XAttribute("firstName", user.FirstName),
                        new XAttribute("lastName", user.LastName),
                        new XElement(soldProducts)));
                }

                string result = xUsersDoc.Declaration + Environment.NewLine + xUsersDoc;
               File.WriteAllText("users-sold-products.xml", result);

                return "Successfully created file 'users-sold-products.xml'";
            }
            
        }

        private static string XmlProductsInRande()
        {
            using (var db = new ProductShopContext())
            {
                var products = db.Products.Where(p => (p.Price >= 1000 && p.Price <= 2000) && p.Buyer !=null)
                     .Select(p => new
                     {
                         Name = p.Name,
                         Price = p.Price,
                         Buyer = $"{p.Buyer.FirstName ?? ""} {p.Buyer.LastName}"
                     })
                     .OrderBy(p => p.Price).ToArray();

                XDeclaration declaration = new XDeclaration("1.0", "utf-8",null);
                var productDoc = new XDocument(declaration);
                productDoc.Add(new XElement("products"));

                foreach (var product in products)
                {
                    productDoc.Element("products")
                        .Add(new XElement("product",
                        new XAttribute("name", product.Name),
                        new XAttribute("price", product.Price),
                        new XAttribute("buyer", product.Buyer)));
                }

                string result = productDoc.Declaration + Environment.NewLine + productDoc;
                File.WriteAllText("products-in-range.xml", result);
                
                return "Successfully created file 'products-in-range.xml'";
            }

        }

        private static string SetCategoriesToProductsXml()
        {
            using (var db = new ProductShopContext())
            {
                int[] categoryIds = db.Categories.Select(c => c.CategoryId).ToArray();
                int[] productIds = db.Products.Select(p => p.ProductId).ToArray();

                var random = new Random();

                foreach (var productId in productIds)
                {
                    var categoriesCount = random.Next(1, 5);
                    var usedIndexes = new List<int>();

                    for (int i = 0; i < categoriesCount; i++)
                    {
                        var categoryIdIndex = random.Next(0, categoryIds.Length);

                        while(usedIndexes.Any(index => index == categoryIdIndex))
                        {
                            categoryIdIndex = random.Next(0, categoryIds.Length);
                        }
                        usedIndexes.Add(categoryIdIndex);

                        var categoryProduct = new CategoryProduct
                        {
                            ProductId = productId,
                            CategoryId = categoryIds[categoryIdIndex]
                        };
                        
                        db.CategoriesProducts.Add(categoryProduct);
                        db.SaveChanges();
                    }
                }
            }
            return "Successfully set categories to each product";
        }

        private static string ImportXmlProducts()
        {
            var productsPath = "Resources\\products.xml";
            var xmlStringProducts = File.ReadAllText(productsPath);

            var xDocProducts = XDocument.Parse(xmlStringProducts);
            var elements = xDocProducts.Root.Elements().ToArray();

            var products = new List<Product>();
            var random = new Random();
            var productsCounter = 0;

            using(var db = new ProductShopContext())
            {
                int[] userIds = db.Users.Select(u => u.UserId).ToArray();

                foreach (var e in elements)
                {
                    int sellerIdIndex = random.Next(0, userIds.Length);
                    
                    int sellerId = userIds[sellerIdIndex];
                    
                    var product = new Product
                    {
                        Name = e.Element("name").Value,
                        Price = decimal.Parse(e.Element("price").Value),
                        SellerId = sellerId
                    };

                    if(productsCounter % 7 != 0)
                    {
                        int buyerIdIndex = random.Next(0, userIds.Length);
                        while (sellerIdIndex == buyerIdIndex)
                        {
                            buyerIdIndex = random.Next(0, userIds.Length);
                        }

                        int buyerId = userIds[buyerIdIndex];

                        product.BuyerId = buyerId;
                    }

                    productsCounter++;
                    products.Add(product);
                }

                db.Products.AddRange(products);
                db.SaveChanges();
            }

            return $"Successfully added {productsCounter} products to the Database";
        }

        private static string ImportXmlCategories()
        {
            var categoriesPath = "Resources\\categories.xml";
            var xmlString = File.ReadAllText(categoriesPath);
            var xDocCategories = XDocument.Parse(xmlString);

            var categories = new List<Category>();

            var elements = xDocCategories.Root.Elements().ToArray();

            foreach (var e in elements)
            {
                var category = new Category
                {
                    Name = e.Element("name").Value
                };
                categories.Add(category);
            }

            using(var db = new ProductShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }

            return $"Successfully added {categories.Count} categories to the Database";
        }

        private static string ImportXmlUsers()
        {
            var usersPath = "Resources\\users.xml";
            var xmlString = File.ReadAllText(usersPath);
            var xDocUsers = XDocument.Parse(xmlString);

            var users = new List<User>();

            var elements = xDocUsers.Root.Elements().ToArray();

            foreach (var e in elements)
            {
                string firstName = e.Attribute("firstName")?.Value;
                string lastName = e.Attribute("lastName").Value;

                int? age = null;
                if(e.Attribute("age") != null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                users.Add(user);
            }
             using(var db = new ProductShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }
            return $"Successfully added {users.Count} users to the Database";
        }

        private static string UsersAndProducts()
        {
            using (var db = new ProductShopContext())
            {
                var usersCount = db.Users.Where(u => u.SellProducts.Count >= 1).Count();
                var usersProducts = db.Users
                    .Where(u => u.SellProducts.Count >= 1)
                    .OrderByDescending(u => u.SellProducts.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = new
                        {
                            Count = u.SellProducts.Count,
                            Products = u.SellProducts.Select(p => new
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                        }
                    });

                var usersFinal = new
                {
                    UsersCount = usersProducts.Count(),
                    Users = usersProducts
                };

                var jsonString = JsonConvert.SerializeObject(usersFinal,Formatting.Indented);
                File.WriteAllText("users-and-products.json", jsonString);
                
                return "Successfully created file 'users-and-products.json'";
            }
        }

        private static string CategoriesByProducts()
        {
            using (var db = new ProductShopContext())
            {
                var categoriesProducts = db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        Category = c.Name,
                        ProductCount = c.CategoryProducts.Count,
                        AvaragePrice = c.CategoryProducts
                                        .Select(p => p.Product.Price)
                                        .Average(),
                        TotalRevenue = (c.CategoryProducts
                                         .Where(u => u.Product.Buyer != null)
                                         .Select(p => p.Product.Price))
                                         .Sum()
                    });

                var jsonString = JsonConvert.SerializeObject(categoriesProducts,Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText("categories-by-products.json", jsonString);

                return "Successfully created file 'categories-by-products.json'";
            }
    }

        private static string SoldProducts()
        {
            using (var db = new ProductShopContext())
            {
                var users = db.Users
                    .Where(p => p.SellProducts.Count >= 1)
                    .OrderBy(n => n.LastName).ThenBy(n => n.FirstName)
                    .Select(u => new
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        SoldProducts = u.SellProducts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price,
                            BuyerFirstName = p.Buyer.FirstName,
                            BuyerLastName = p.Buyer.LastName
                        }) });

                var jsonString = JsonConvert.SerializeObject(users,Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                File.WriteAllText("user-sold-products.json", jsonString);

                return "Successfully create file 'user-sold-products.json'";
            }
        }

        private static string ProductsInRange()
        {
            using (var db = new ProductShopContext())
            {
                var products = db.Products.Where(p => p.Price >= 500 && p.Price <= 1000)
                    .Select(p=>new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Seller =$"{p.Seller.FirstName ?? ""} {p.Seller.LastName}" 
                    })
                    .OrderBy(p => p.Price).ToArray();

                var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented);

                File.WriteAllText("products-in-range.json", jsonString);

                string result = $"Successfully create file 'products-in-range.json'";
                return result;
            }
        }

        private static string SetCategoriesToProducts()
        {
            using (var db = new ProductShopContext())
            {
                int[] productsIds = db.Products.Select(p => p.ProductId).ToArray();
                int[] categoriesIds = db.Categories.Select(c => c.CategoryId).ToArray();

                Random random = new Random();

                foreach (var productId in productsIds)
                {
                    var usedIndexes = new List<int>();
                    var categoryCount = random.Next(1, 5);
                    for (int i = 0; i < categoryCount; i++)
                    {
                        var categoryIdIndex = random.Next(0, categoriesIds.Length);

                        while (usedIndexes.Any(index => index == categoryIdIndex))
                        {
                            categoryIdIndex = random.Next(0, categoriesIds.Length);
                        }

                        var categoryProduct = new CategoryProduct
                        {
                            ProductId = productId,
                            CategoryId = categoriesIds[categoryIdIndex]
                        };

                        usedIndexes.Add(categoryIdIndex);

                        db.CategoriesProducts.Add(categoryProduct);
                        db.SaveChanges();
                    }
                }

                return "Successfully add categories to each product";
            }

        }

        private static string ImportProductsInDB()
        {
            string productsPath = "Resources\\products.json";
            Product[] products = ImportJson<Product>(productsPath);
            int currentProductNumber = 0;

            using(var db = new ProductShopContext())
            {
                int[] userIds = db.Users.Select(u => u.UserId).ToArray();
                
                Random random = new Random();

                foreach (var product in products)
                {
                    var sellerIdIndex = random.Next(0, userIds.Length);
                    product.SellerId = userIds[sellerIdIndex];

                    if (currentProductNumber % 7 != 0)
                    {
                        var buyerIdIndex = random.Next(0, userIds.Length);
                        while (sellerIdIndex == buyerIdIndex)
                        {
                            buyerIdIndex = random.Next(0, userIds.Length);
                        }
                        product.BuyerId = userIds[buyerIdIndex];
                    }

                    currentProductNumber++;
                }

                db.Products.AddRange(products);
                db.SaveChanges();
                return $"Successfully added {products.Length} products into the Database";
            }
        }

        private static string ImportCategoriesInDB()
        {
            string categoriesPath = "Resources\\categories.json";
            Category[] categories = ImportJson<Category>(categoriesPath);

            using(var db = new ProductShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
            return $"Successuflly added {categories.Length} products from {categoriesPath} into the Database";
        }

        private static string ImportUsersInDB()
        {
            var usersPath = "Resources\\users.json";
            User[] users = ImportJson<User>(usersPath);

            using(var db = new ProductShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }

            return $"Successuflly added {users.Length} users from {usersPath} into the Database";
        }

        private static T[] ImportJson<T>(string path)
        {
            var jsonSting = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonSting);

            return objects;
        }

        private static void ResetDatabase()
        {
            using(var db = new ProductShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }
        }
    }
}
