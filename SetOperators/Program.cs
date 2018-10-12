using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;

namespace SetOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqSamples samples = new LinqSamples();

            //Comment or uncomment the method calls below to run or not

            samples.Linq46(); // This sample uses Distinct to remove  duplicate  elements in a sequence of factors of 300

            samples.Linq47(); // This sample uses Distinct to find the unique Category names

            samples.Linq48(); // This sample uses Union to create  one sequence that contains the unique values from both 
                              // arrays

            samples.Linq49(); // This sample uses the Union method to create  one sequence that contains the unique first 
                              // letter from both product and customer names. Union is only available through method 
                              // syntax

            samples.Linq50(); // This sample uses Intersect to create one sequence that contains the common values shared 
                              // by both arrays

            samples.Linq51(); // This sample uses Intersect  to create one sequence that contains the common first letter 
                              // from both product and customer names

            samples.Linq52(); // This sample uses Except to create a sequence that contains the values from numbersA that 
                              // are not also in numbersB

            samples.Linq53(); // This sample uses Except to create one  sequence that contains the 1st letters of product 
                              // names that are not also first letters of customer names
        }

        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
        }

        public class Order
        {
            public int OrderID { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal Total { get; set; }
        }

        public class Customer
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public Order[] Orders { get; set; }
        }

        class LinqSamples
        {
            private List<Product> productList;
            private List<Customer> customerList;

            [Category("Set Operators")]
            [Description("This sample uses Distinct to remove duplicate elements in a sequence of " +
                        "factors of 300.")]
            public void Linq46()
            {
                int[] factorsOf300 = { 2, 2, 3, 5, 5 };

                var uniqueFactors = factorsOf300.Distinct();

                Console.WriteLine("Prime factors of 300:");
                foreach (var f in uniqueFactors)
                {
                    Console.WriteLine(f);
                }
            }

            [Category("Set Operators")]
            [Description("This sample uses Distinct to find the unique Category names.")]
            public void Linq47()
            {
                List<Product> products = GetProductList();

                var categoryNames = (
                    from prod in products
                    select prod.Category)
                    .Distinct();

                Console.WriteLine("Category names:");
                foreach (var n in categoryNames)
                {
                    Console.WriteLine(n);
                }
            }

            [Category("Set Operators")]
            [Description("This sample uses Union to create one sequence that contains the unique values " +
                         "from both arrays.")]
            public void Linq48()
            {
                int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
                int[] numbersB = { 1, 3, 5, 7, 8 };

                var uniqueNumbers = numbersA.Union(numbersB);

                Console.WriteLine("Unique numbers from both arrays:");
                foreach (var n in uniqueNumbers)
                {
                    Console.WriteLine(n);
                }
            }

            [Category("Set Operators")]
            [Description("This sample uses the Union method to create one sequence that contains the unique first letter " +
                         "from both product and customer names. Union is only available through method syntax.")]
            public void Linq49()
            {
                List<Product> products = GetProductList();
                List<Customer> customers = GetCustomerList();

                var productFirstChars =
                    from prod in products
                    select prod.ProductName[0];
                var customerFirstChars =
                    from cust in customers
                    select cust.CompanyName[0];

                var uniqueFirstChars = productFirstChars.Union(customerFirstChars);

                Console.WriteLine("Unique first letters from Product names and Customer names:");
                foreach (var ch in uniqueFirstChars)
                {
                    Console.WriteLine(ch);
                }
            }

            [Category("Set Operators")]
            [Description("This sample uses Intersect to create one sequence that contains the common values " +
                        "shared by both arrays.")]
            public void Linq50()
            {
                int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
                int[] numbersB = { 1, 3, 5, 7, 8 };

                var commonNumbers = numbersA.Intersect(numbersB);

                Console.WriteLine("Common numbers shared by both arrays:");
                foreach (var n in commonNumbers)
                {
                    Console.WriteLine(n);
                }
            }

            [Category("Set Operators")]
            [Description("This sample uses Intersect to create one sequence that contains the common first letter " +
                         "from both product and customer names.")]
            public void Linq51()
            {
                List<Product> products = GetProductList();
                List<Customer> customers = GetCustomerList();

                var productFirstChars =
                    from prod in products
                    select prod.ProductName[0];
                var customerFirstChars =
                    from cust in customers
                    select cust.CompanyName[0];

                var commonFirstChars = productFirstChars.Intersect(customerFirstChars);

                Console.WriteLine("Common first letters from Product names and Customer names:");
                foreach (var ch in commonFirstChars)
                {
                    Console.WriteLine(ch);
                }
            }

            [Category("Set Operators")]
            [Description("This sample uses Except to create a sequence that contains the values from numbersA" +
                         "that are not also in numbersB.")]
            public void Linq52()
            {
                int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
                int[] numbersB = { 1, 3, 5, 7, 8 };

                IEnumerable<int> aOnlyNumbers = numbersA.Except(numbersB);

                Console.WriteLine("Numbers in first array but not second array:");
                foreach (var n in aOnlyNumbers)
                {
                    Console.WriteLine(n);
                }
            }

            [Category("Set Operators")]
            [Description("This sample uses Except to create one sequence that contains the first letters " +
                         "of product names that are not also first letters of customer names.")]
            public void Linq53()
            {
                List<Product> products = GetProductList();
                List<Customer> customers = GetCustomerList();

                var productFirstChars =
                    from prod in products
                    select prod.ProductName[0];
                var customerFirstChars =
                    from cust in customers
                    select cust.CompanyName[0];

                var productOnlyFirstChars = productFirstChars.Except(customerFirstChars);

                Console.WriteLine("First letters from Product names, but not from Customer names:");
                foreach (var ch in productOnlyFirstChars)
                {
                    Console.WriteLine(ch);
                }
            }

            public List<Product> GetProductList()
            {
                if (productList == null)
                    createLists();

                return productList;
            }

            public List<Customer> GetCustomerList()
            {
                if (customerList == null)
                    createLists();

                return customerList;
            }

            private void createLists()
            {
                // Product data created in-memory using collection initializer:
                productList =
                    new List<Product> {
                    new Product { ProductID = 1, ProductName = "Chai", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 39 },
                    new Product { ProductID = 2, ProductName = "Chang", Category = "Beverages", UnitPrice = 19.0000M, UnitsInStock = 17 },
                    new Product { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments", UnitPrice = 10.0000M, UnitsInStock = 13 },
                    new Product { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments", UnitPrice = 22.0000M, UnitsInStock = 53 },
                    new Product { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments", UnitPrice = 21.3500M, UnitsInStock = 0 },
                    new Product { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", Category = "Condiments", UnitPrice = 25.0000M, UnitsInStock = 120 },
                    new Product { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", Category = "Produce", UnitPrice = 30.0000M, UnitsInStock = 15 },
                    new Product { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", Category = "Condiments", UnitPrice = 40.0000M, UnitsInStock = 6 },
                    new Product { ProductID = 9, ProductName = "Mishi Kobe Niku", Category = "Meat/Poultry", UnitPrice = 97.0000M, UnitsInStock = 29 },
                    new Product { ProductID = 10, ProductName = "Ikura", Category = "Seafood", UnitPrice = 31.0000M, UnitsInStock = 31 },
                    new Product { ProductID = 11, ProductName = "Queso Cabrales", Category = "Dairy Products", UnitPrice = 21.0000M, UnitsInStock = 22 },
                    new Product { ProductID = 12, ProductName = "Queso Manchego La Pastora", Category = "Dairy Products", UnitPrice = 38.0000M, UnitsInStock = 86 },
                    new Product { ProductID = 13, ProductName = "Konbu", Category = "Seafood", UnitPrice = 6.0000M, UnitsInStock = 24 },
                    new Product { ProductID = 14, ProductName = "Tofu", Category = "Produce", UnitPrice = 23.2500M, UnitsInStock = 35 },
                    new Product { ProductID = 15, ProductName = "Genen Shouyu", Category = "Condiments", UnitPrice = 15.5000M, UnitsInStock = 39 },
                    new Product { ProductID = 16, ProductName = "Pavlova", Category = "Confections", UnitPrice = 17.4500M, UnitsInStock = 29 },
                    new Product { ProductID = 17, ProductName = "Alice Mutton", Category = "Meat/Poultry", UnitPrice = 39.0000M, UnitsInStock = 0 },
                    new Product { ProductID = 18, ProductName = "Carnarvon Tigers", Category = "Seafood", UnitPrice = 62.5000M, UnitsInStock = 42 },
                    new Product { ProductID = 19, ProductName = "Teatime Chocolate Biscuits", Category = "Confections", UnitPrice = 9.2000M, UnitsInStock = 25 },
                    new Product { ProductID = 20, ProductName = "Sir Rodney's Marmalade", Category = "Confections", UnitPrice = 81.0000M, UnitsInStock = 40 },
                    new Product { ProductID = 21, ProductName = "Sir Rodney's Scones", Category = "Confections", UnitPrice = 10.0000M, UnitsInStock = 3 },
                    new Product { ProductID = 22, ProductName = "Gustaf's Kn�ckebr�d", Category = "Grains/Cereals", UnitPrice = 21.0000M, UnitsInStock = 104 },
                    new Product { ProductID = 23, ProductName = "Tunnbr�d", Category = "Grains/Cereals", UnitPrice = 9.0000M, UnitsInStock = 61 },
                    new Product { ProductID = 24, ProductName = "Guaran� Fant�stica", Category = "Beverages", UnitPrice = 4.5000M, UnitsInStock = 20 },
                    new Product { ProductID = 25, ProductName = "NuNuCa Nu�-Nougat-Creme", Category = "Confections", UnitPrice = 14.0000M, UnitsInStock = 76 },
                    new Product { ProductID = 26, ProductName = "Gumb�r Gummib�rchen", Category = "Confections", UnitPrice = 31.2300M, UnitsInStock = 15 },
                    new Product { ProductID = 27, ProductName = "Schoggi Schokolade", Category = "Confections", UnitPrice = 43.9000M, UnitsInStock = 49 },
                    new Product { ProductID = 28, ProductName = "R�ssle Sauerkraut", Category = "Produce", UnitPrice = 45.6000M, UnitsInStock = 26 },
                    new Product { ProductID = 29, ProductName = "Th�ringer Rostbratwurst", Category = "Meat/Poultry", UnitPrice = 123.7900M, UnitsInStock = 0 },
                    new Product { ProductID = 30, ProductName = "Nord-Ost Matjeshering", Category = "Seafood", UnitPrice = 25.8900M, UnitsInStock = 10 },
                    new Product { ProductID = 31, ProductName = "Gorgonzola Telino", Category = "Dairy Products", UnitPrice = 12.5000M, UnitsInStock = 0 },
                    new Product { ProductID = 32, ProductName = "Mascarpone Fabioli", Category = "Dairy Products", UnitPrice = 32.0000M, UnitsInStock = 9 },
                    new Product { ProductID = 33, ProductName = "Geitost", Category = "Dairy Products", UnitPrice = 2.5000M, UnitsInStock = 112 },
                    new Product { ProductID = 34, ProductName = "Sasquatch Ale", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 111 },
                    new Product { ProductID = 35, ProductName = "Steeleye Stout", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 20 },
                    new Product { ProductID = 36, ProductName = "Inlagd Sill", Category = "Seafood", UnitPrice = 19.0000M, UnitsInStock = 112 },
                    new Product { ProductID = 37, ProductName = "Gravad lax", Category = "Seafood", UnitPrice = 26.0000M, UnitsInStock = 11 },
                    new Product { ProductID = 38, ProductName = "C�te de Blaye", Category = "Beverages", UnitPrice = 263.5000M, UnitsInStock = 17 },
                    new Product { ProductID = 39, ProductName = "Chartreuse verte", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 69 },
                    new Product { ProductID = 40, ProductName = "Boston Crab Meat", Category = "Seafood", UnitPrice = 18.4000M, UnitsInStock = 123 },
                    new Product { ProductID = 41, ProductName = "Jack's New England Clam Chowder", Category = "Seafood", UnitPrice = 9.6500M, UnitsInStock = 85 },
                    new Product { ProductID = 42, ProductName = "Singaporean Hokkien Fried Mee", Category = "Grains/Cereals", UnitPrice = 14.0000M, UnitsInStock = 26 },
                    new Product { ProductID = 43, ProductName = "Ipoh Coffee", Category = "Beverages", UnitPrice = 46.0000M, UnitsInStock = 17 },
                    new Product { ProductID = 44, ProductName = "Gula Malacca", Category = "Condiments", UnitPrice = 19.4500M, UnitsInStock = 27 },
                    new Product { ProductID = 45, ProductName = "Rogede sild", Category = "Seafood", UnitPrice = 9.5000M, UnitsInStock = 5 },
                    new Product { ProductID = 46, ProductName = "Spegesild", Category = "Seafood", UnitPrice = 12.0000M, UnitsInStock = 95 },
                    new Product { ProductID = 47, ProductName = "Zaanse koeken", Category = "Confections", UnitPrice = 9.5000M, UnitsInStock = 36 },
                    new Product { ProductID = 48, ProductName = "Chocolade", Category = "Confections", UnitPrice = 12.7500M, UnitsInStock = 15 },
                    new Product { ProductID = 49, ProductName = "Maxilaku", Category = "Confections", UnitPrice = 20.0000M, UnitsInStock = 10 },
                    new Product { ProductID = 50, ProductName = "Valkoinen suklaa", Category = "Confections", UnitPrice = 16.2500M, UnitsInStock = 65 },
                    new Product { ProductID = 51, ProductName = "Manjimup Dried Apples", Category = "Produce", UnitPrice = 53.0000M, UnitsInStock = 20 },
                    new Product { ProductID = 52, ProductName = "Filo Mix", Category = "Grains/Cereals", UnitPrice = 7.0000M, UnitsInStock = 38 },
                    new Product { ProductID = 53, ProductName = "Perth Pasties", Category = "Meat/Poultry", UnitPrice = 32.8000M, UnitsInStock = 0 },
                    new Product { ProductID = 54, ProductName = "Tourti�re", Category = "Meat/Poultry", UnitPrice = 7.4500M, UnitsInStock = 21 },
                    new Product { ProductID = 55, ProductName = "P�t� chinois", Category = "Meat/Poultry", UnitPrice = 24.0000M, UnitsInStock = 115 },
                    new Product { ProductID = 56, ProductName = "Gnocchi di nonna Alice", Category = "Grains/Cereals", UnitPrice = 38.0000M, UnitsInStock = 21 },
                    new Product { ProductID = 57, ProductName = "Ravioli Angelo", Category = "Grains/Cereals", UnitPrice = 19.5000M, UnitsInStock = 36 },
                    new Product { ProductID = 58, ProductName = "Escargots de Bourgogne", Category = "Seafood", UnitPrice = 13.2500M, UnitsInStock = 62 },
                    new Product { ProductID = 59, ProductName = "Raclette Courdavault", Category = "Dairy Products", UnitPrice = 55.0000M, UnitsInStock = 79 },
                    new Product { ProductID = 60, ProductName = "Camembert Pierrot", Category = "Dairy Products", UnitPrice = 34.0000M, UnitsInStock = 19 },
                    new Product { ProductID = 61, ProductName = "Sirop d'�rable", Category = "Condiments", UnitPrice = 28.5000M, UnitsInStock = 113 },
                    new Product { ProductID = 62, ProductName = "Tarte au sucre", Category = "Confections", UnitPrice = 49.3000M, UnitsInStock = 17 },
                    new Product { ProductID = 63, ProductName = "Vegie-spread", Category = "Condiments", UnitPrice = 43.9000M, UnitsInStock = 24 },
                    new Product { ProductID = 64, ProductName = "Wimmers gute Semmelkn�del", Category = "Grains/Cereals", UnitPrice = 33.2500M, UnitsInStock = 22 },
                    new Product { ProductID = 65, ProductName = "Louisiana Fiery Hot Pepper Sauce", Category = "Condiments", UnitPrice = 21.0500M, UnitsInStock = 76 },
                    new Product { ProductID = 66, ProductName = "Louisiana Hot Spiced Okra", Category = "Condiments", UnitPrice = 17.0000M, UnitsInStock = 4 },
                    new Product { ProductID = 67, ProductName = "Laughing Lumberjack Lager", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 52 },
                    new Product { ProductID = 68, ProductName = "Scottish Longbreads", Category = "Confections", UnitPrice = 12.5000M, UnitsInStock = 6 },
                    new Product { ProductID = 69, ProductName = "Gudbrandsdalsost", Category = "Dairy Products", UnitPrice = 36.0000M, UnitsInStock = 26 },
                    new Product { ProductID = 70, ProductName = "Outback Lager", Category = "Beverages", UnitPrice = 15.0000M, UnitsInStock = 15 },
                    new Product { ProductID = 71, ProductName = "Flotemysost", Category = "Dairy Products", UnitPrice = 21.5000M, UnitsInStock = 26 },
                    new Product { ProductID = 72, ProductName = "Mozzarella di Giovanni", Category = "Dairy Products", UnitPrice = 34.8000M, UnitsInStock = 14 },
                    new Product { ProductID = 73, ProductName = "R�d Kaviar", Category = "Seafood", UnitPrice = 15.0000M, UnitsInStock = 101 },
                    new Product { ProductID = 74, ProductName = "Longlife Tofu", Category = "Produce", UnitPrice = 10.0000M, UnitsInStock = 4 },
                    new Product { ProductID = 75, ProductName = "Rh�nbr�u Klosterbier", Category = "Beverages", UnitPrice = 7.7500M, UnitsInStock = 125 },
                    new Product { ProductID = 76, ProductName = "Lakkalik��ri", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 57 },
                    new Product { ProductID = 77, ProductName = "Original Frankfurter gr�ne So�e", Category = "Condiments", UnitPrice = 13.0000M, UnitsInStock = 32 }
                };

                // Customer/Order data read into memory from XML file using XLinq:
                customerList = (
                    from e in XDocument.Load(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SetOperators.customers.xml")).
                              Root.Elements("customer")
                    select new Customer
                    {
                        CustomerID = (string)e.Element("id"),
                        CompanyName = (string)e.Element("name"),
                        Address = (string)e.Element("address"),
                        City = (string)e.Element("city"),
                        Region = (string)e.Element("region"),
                        PostalCode = (string)e.Element("postalcode"),
                        Country = (string)e.Element("country"),
                        Phone = (string)e.Element("phone"),
                        Fax = (string)e.Element("fax"),
                        Orders = (
                            from o in e.Elements("orders").Elements("order")
                            select new Order
                            {
                                OrderID = (int)o.Element("id"),
                                OrderDate = (DateTime)o.Element("orderdate"),
                                Total = (decimal)o.Element("total")
                            })
                            .ToArray()
                    })
                    .ToList();
            }
        }
    }
}
