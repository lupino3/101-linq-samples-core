﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;

namespace OrderingOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqSamples samples = new LinqSamples();

            //Comment or uncomment the method calls below to run or not

            samples.Linq28(); // This sample uses orderby to sort a list of words alphabetically

            samples.Linq29(); // This sample uses orderby to sort a list of words by length

            samples.Linq30(); // This sample uses orderby to sort a list of products by name. Use the \"descending\" 
                              // keyword at the end of the clause to perform a reverse ordering

            samples.Linq31(); // This sample uses an  OrderBy clause with a custom comparer to do a case-insensitive 
                              // sort of the words in an array

            samples.Linq32(); // This sample uses  orderby and  descending to sort a list of doubles from highest to 
                              // lowest

            samples.Linq33(); // This sample uses  orderby to sort a list of products by units in stock from highest 
                              // to lowest

            samples.Linq34(); // This sample uses method syntax to call OrderByDescending  because it enables you to 
                              // use a custom comparer

            samples.Linq35(); // This sample uses a compound  orderby to  sort a list of digits,  first by length of 
                              // their name, and then alphabetically by the name itself

            samples.Linq36(); // The first query in this sample uses method syntax to call OrderBy and ThenBy with a 
                              // custom comparer to sort first by word length and then by a case-insensitive sort of 
                              // the words in an array.  The second two queries show another way to perform the same 
                              // task

            samples.Linq37(); // This sample uses a compound  orderby to sort a list of products,  first by category, 
                              // and then by unit price, from highest to lowest

            samples.Linq38(); // This sample uses an OrderBy and a ThenBy clause with a custom comparer to sort first 
                              // by word length and  then by a case-insensitive  descending  sort of  the words in an 
                              // array

            samples.Linq39(); // This sample uses Reverse to  create a list of  all digits in the  array whose second 
                              // letter is 'i' that is reversed from the order in the original array
        }

        public class Product
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
        }

        class LinqSamples
        {
            private List<Product> productList;

            [Category("Ordering Operators")]
            [Description("This sample uses orderby to sort a list of words alphabetically.")]
            public void Linq28()
            {
                string[] words = { "cherry", "apple", "blueberry" };

                var sortedWords =
                    from word in words
                    orderby word
                    select word;

                Console.WriteLine("The sorted list of words:");
                foreach (var w in sortedWords)
                {
                    Console.WriteLine(w);
                }
            }

            [Category("Ordering Operators")]
            [Description("This sample uses orderby to sort a list of words by length.")]
            public void Linq29()
            {
                string[] words = { "cherry", "apple", "blueberry" };

                var sortedWords =
                    from word in words
                    orderby word.Length
                    select word;

                Console.WriteLine("The sorted list of words (by length):");
                foreach (var w in sortedWords)
                {
                    Console.WriteLine(w);
                }
            }

            [Category("Ordering Operators")]
            [Description("This sample uses orderby to sort a list of products by name. " +
                        "Use the \"descending\" keyword at the end of the clause to perform a reverse ordering.")]
            public void Linq30()
            {
                List<Product> products = GetProductList();

                var sortedProducts =
                    from prod in products
                    orderby prod.ProductName
                    select prod;

                ObjectDumper.Write(sortedProducts);
            }

            // Custom comparer for use with ordering operators
            public class CaseInsensitiveComparer : IComparer<string>
            {
                public int Compare(string x, string y)
                {
                    return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
                }
            }

            [Category("Ordering Operators")]
            [Description("This sample uses an OrderBy clause with a custom comparer to " +
                         "do a case-insensitive sort of the words in an array.")]
            public void Linq31()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());

                ObjectDumper.Write(sortedWords);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses orderby and descending to sort a list of " +
                         "doubles from highest to lowest.")]
            public void Linq32()
            {
                double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

                var sortedDoubles =
                    from d in doubles
                    orderby d descending
                    select d;

                Console.WriteLine("The doubles from highest to lowest:");
                foreach (var d in sortedDoubles)
                {
                    Console.WriteLine(d);
                }
            }

            [Category("Ordering Operators")]
            [Description("This sample uses orderby to sort a list of products by units in stock " +
                         "from highest to lowest.")]
            public void Linq33()
            {
                List<Product> products = GetProductList();

                var sortedProducts =
                    from prod in products
                    orderby prod.UnitsInStock descending
                    select prod;

                ObjectDumper.Write(sortedProducts);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses method syntax to call OrderByDescending because it " +
                        " enables you to use a custom comparer.")]
            public void Linq34()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

                ObjectDumper.Write(sortedWords);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses a compound orderby to sort a list of digits, " +
                         "first by length of their name, and then alphabetically by the name itself.")]
            public void Linq35()
            {
                string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                var sortedDigits =
                    from digit in digits
                    orderby digit.Length, digit
                    select digit;

                Console.WriteLine("Sorted digits:");
                foreach (var d in sortedDigits)
                {
                    Console.WriteLine(d);
                }
            }

            [Category("Ordering Operators")]
            [Description("The first query in this sample uses method syntax to call OrderBy and ThenBy with a custom comparer to " +
                         "sort first by word length and then by a case-insensitive sort of the words in an array. " +
                         "The second two queries show another way to perform the same task.")]
            public void Linq36()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords =
                    words.OrderBy(a => a.Length)
                         .ThenBy(a => a, new CaseInsensitiveComparer());

                // Another way. TODO is this use of ThenBy correct? It seems to work on this sample array.
                var sortedWords2 =
                    from word in words
                    orderby word.Length
                    select word;

                var sortedWords3 = sortedWords2.ThenBy(a => a, new CaseInsensitiveComparer());

                ObjectDumper.Write(sortedWords);

                ObjectDumper.Write(sortedWords3);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses a compound orderby to sort a list of products, " +
                         "first by category, and then by unit price, from highest to lowest.")]
            public void Linq37()
            {
                List<Product> products = GetProductList();

                var sortedProducts =
                    from prod in products
                    orderby prod.Category, prod.UnitPrice descending
                    select prod;

                ObjectDumper.Write(sortedProducts);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses an OrderBy and a ThenBy clause with a custom comparer to " +
                         "sort first by word length and then by a case-insensitive descending sort " +
                         "of the words in an array.")]
            public void Linq38()
            {
                string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

                var sortedWords =
                    words.OrderBy(a => a.Length)
                         .ThenByDescending(a => a, new CaseInsensitiveComparer());

                ObjectDumper.Write(sortedWords);
            }

            [Category("Ordering Operators")]
            [Description("This sample uses Reverse to create a list of all digits in the array whose " +
                         "second letter is 'i' that is reversed from the order in the original array.")]
            public void Linq39()
            {
                string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                var reversedIDigits = (
                    from digit in digits
                    where digit[1] == 'i'
                    select digit)
                    .Reverse();

                Console.WriteLine("A backwards list of the digits with a second character of 'i':");
                foreach (var d in reversedIDigits)
                {
                    Console.WriteLine(d);
                }
            }

            public List<Product> GetProductList()
            {
                if (productList == null)
                    createLists();

                return productList;
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
                    new Product { ProductID = 22, ProductName = "Gustaf's Knäckebröd", Category = "Grains/Cereals", UnitPrice = 21.0000M, UnitsInStock = 104 },
                    new Product { ProductID = 23, ProductName = "Tunnbröd", Category = "Grains/Cereals", UnitPrice = 9.0000M, UnitsInStock = 61 },
                    new Product { ProductID = 24, ProductName = "Guaraná Fantástica", Category = "Beverages", UnitPrice = 4.5000M, UnitsInStock = 20 },
                    new Product { ProductID = 25, ProductName = "NuNuCa Nuß-Nougat-Creme", Category = "Confections", UnitPrice = 14.0000M, UnitsInStock = 76 },
                    new Product { ProductID = 26, ProductName = "Gumbär Gummibärchen", Category = "Confections", UnitPrice = 31.2300M, UnitsInStock = 15 },
                    new Product { ProductID = 27, ProductName = "Schoggi Schokolade", Category = "Confections", UnitPrice = 43.9000M, UnitsInStock = 49 },
                    new Product { ProductID = 28, ProductName = "Rössle Sauerkraut", Category = "Produce", UnitPrice = 45.6000M, UnitsInStock = 26 },
                    new Product { ProductID = 29, ProductName = "Thüringer Rostbratwurst", Category = "Meat/Poultry", UnitPrice = 123.7900M, UnitsInStock = 0 },
                    new Product { ProductID = 30, ProductName = "Nord-Ost Matjeshering", Category = "Seafood", UnitPrice = 25.8900M, UnitsInStock = 10 },
                    new Product { ProductID = 31, ProductName = "Gorgonzola Telino", Category = "Dairy Products", UnitPrice = 12.5000M, UnitsInStock = 0 },
                    new Product { ProductID = 32, ProductName = "Mascarpone Fabioli", Category = "Dairy Products", UnitPrice = 32.0000M, UnitsInStock = 9 },
                    new Product { ProductID = 33, ProductName = "Geitost", Category = "Dairy Products", UnitPrice = 2.5000M, UnitsInStock = 112 },
                    new Product { ProductID = 34, ProductName = "Sasquatch Ale", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 111 },
                    new Product { ProductID = 35, ProductName = "Steeleye Stout", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 20 },
                    new Product { ProductID = 36, ProductName = "Inlagd Sill", Category = "Seafood", UnitPrice = 19.0000M, UnitsInStock = 112 },
                    new Product { ProductID = 37, ProductName = "Gravad lax", Category = "Seafood", UnitPrice = 26.0000M, UnitsInStock = 11 },
                    new Product { ProductID = 38, ProductName = "Côte de Blaye", Category = "Beverages", UnitPrice = 263.5000M, UnitsInStock = 17 },
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
                    new Product { ProductID = 54, ProductName = "Tourtière", Category = "Meat/Poultry", UnitPrice = 7.4500M, UnitsInStock = 21 },
                    new Product { ProductID = 55, ProductName = "Pâté chinois", Category = "Meat/Poultry", UnitPrice = 24.0000M, UnitsInStock = 115 },
                    new Product { ProductID = 56, ProductName = "Gnocchi di nonna Alice", Category = "Grains/Cereals", UnitPrice = 38.0000M, UnitsInStock = 21 },
                    new Product { ProductID = 57, ProductName = "Ravioli Angelo", Category = "Grains/Cereals", UnitPrice = 19.5000M, UnitsInStock = 36 },
                    new Product { ProductID = 58, ProductName = "Escargots de Bourgogne", Category = "Seafood", UnitPrice = 13.2500M, UnitsInStock = 62 },
                    new Product { ProductID = 59, ProductName = "Raclette Courdavault", Category = "Dairy Products", UnitPrice = 55.0000M, UnitsInStock = 79 },
                    new Product { ProductID = 60, ProductName = "Camembert Pierrot", Category = "Dairy Products", UnitPrice = 34.0000M, UnitsInStock = 19 },
                    new Product { ProductID = 61, ProductName = "Sirop d'érable", Category = "Condiments", UnitPrice = 28.5000M, UnitsInStock = 113 },
                    new Product { ProductID = 62, ProductName = "Tarte au sucre", Category = "Confections", UnitPrice = 49.3000M, UnitsInStock = 17 },
                    new Product { ProductID = 63, ProductName = "Vegie-spread", Category = "Condiments", UnitPrice = 43.9000M, UnitsInStock = 24 },
                    new Product { ProductID = 64, ProductName = "Wimmers gute Semmelknödel", Category = "Grains/Cereals", UnitPrice = 33.2500M, UnitsInStock = 22 },
                    new Product { ProductID = 65, ProductName = "Louisiana Fiery Hot Pepper Sauce", Category = "Condiments", UnitPrice = 21.0500M, UnitsInStock = 76 },
                    new Product { ProductID = 66, ProductName = "Louisiana Hot Spiced Okra", Category = "Condiments", UnitPrice = 17.0000M, UnitsInStock = 4 },
                    new Product { ProductID = 67, ProductName = "Laughing Lumberjack Lager", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 52 },
                    new Product { ProductID = 68, ProductName = "Scottish Longbreads", Category = "Confections", UnitPrice = 12.5000M, UnitsInStock = 6 },
                    new Product { ProductID = 69, ProductName = "Gudbrandsdalsost", Category = "Dairy Products", UnitPrice = 36.0000M, UnitsInStock = 26 },
                    new Product { ProductID = 70, ProductName = "Outback Lager", Category = "Beverages", UnitPrice = 15.0000M, UnitsInStock = 15 },
                    new Product { ProductID = 71, ProductName = "Flotemysost", Category = "Dairy Products", UnitPrice = 21.5000M, UnitsInStock = 26 },
                    new Product { ProductID = 72, ProductName = "Mozzarella di Giovanni", Category = "Dairy Products", UnitPrice = 34.8000M, UnitsInStock = 14 },
                    new Product { ProductID = 73, ProductName = "Röd Kaviar", Category = "Seafood", UnitPrice = 15.0000M, UnitsInStock = 101 },
                    new Product { ProductID = 74, ProductName = "Longlife Tofu", Category = "Produce", UnitPrice = 10.0000M, UnitsInStock = 4 },
                    new Product { ProductID = 75, ProductName = "Rhönbräu Klosterbier", Category = "Beverages", UnitPrice = 7.7500M, UnitsInStock = 125 },
                    new Product { ProductID = 76, ProductName = "Lakkalikööri", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 57 },
                    new Product { ProductID = 77, ProductName = "Original Frankfurter grüne Soße", Category = "Condiments", UnitPrice = 13.0000M, UnitsInStock = 32 }
                };
            }
        }
    }
}
