using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;

namespace ProjectionOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqSamples samples = new LinqSamples();

            // Comment or uncomment the method calls below to run or not
            samples.DataSetLinq6();     // This sample uses select to produce a sequence of ints one higher than those in an existing array of ints.
            samples.DataSetLinq7();     // This sample uses select to return a sequence of just the names of a list of products.
            samples.DataSetLinq8();     // This sample uses select to produce a sequence of strings representing the text version of a sequence of ints.
            samples.DataSetLinq9();     // This sample uses select to produce a sequence of the uppercase and lowercase versions of each word in the original array.
            samples.DataSetLinq10();    // This sample uses select to produce a sequence containing text representations of digits and whether their length is even or odd.
            samples.DataSetLinq11();    // This sample uses select to produce a sequence containing some properties of Products...
            samples.DataSetLinq12();    // This sample uses an indexed Select clause to determine if the value of ints in an array match their position in the array.
            samples.DataSetLinq13();    // This sample combines select and where to make a simple query that returns the text form of each digit less than 5.
            samples.DataSetLinq14();    // This sample uses a compound from clause to make a query that returns all pairs of numbers...
            samples.DataSetLinq15();    // This sample uses a compound from clause to select all orders where the order total is less than 500.00.
            samples.DataSetLinq16();    // This sample uses a compound from clause to select all orders where the order was made in 1998 or later.
            samples.DataSetLinq17();    // This sample uses a compound from clause to select all orders where order total is greater than 2000.00...
            samples.DataSetLinq18();    // This sample uses multiple from clauses so that filtering on customers can be done before selecting their orders...
            samples.DataSetLinq19();    // This sample uses an indexed SelectMany clause to select all orders...
        }

        private class LinqSamples
        {
            private DataSet testDS;

            public LinqSamples()
            {
                testDS = TestHelper.CreateTestDataset();
            }

            #region "Projection Operators"

            [Category("Projection Operators")]
            [Description("This sample uses select to produce a sequence of ints one higher than " +
                         "those in an existing array of ints.")]
            public void DataSetLinq6()
            {

                var numbers = testDS.Tables["Numbers"].AsEnumerable();

                var numsPlusOne =
                    from n in numbers
                    select n.Field<int>(0) + 1;

                Console.WriteLine("Numbers + 1:");
                foreach (var i in numsPlusOne)
                {
                    Console.WriteLine(i);
                }
            }

            [Category("Projection Operators")]
            [Description("This sample uses select to return a sequence of just the names of a list of products.")]
            public void DataSetLinq7()
            {

                var products = testDS.Tables["Products"].AsEnumerable();

                var productNames =
                    from p in products
                    select p.Field<string>("ProductName");

                Console.WriteLine("Product Names:");
                foreach (var productName in productNames)
                {
                    Console.WriteLine(productName);
                }
            }

            [Category("Projection Operators")]
            [Description("This sample uses select to produce a sequence of strings representing " +
                         "the text version of a sequence of ints.")]
            public void DataSetLinq8()
            {

                var numbers = testDS.Tables["Numbers"].AsEnumerable();
                string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                var textNums = numbers.Select(p => strings[p.Field<int>("number")]);

                Console.WriteLine("Number strings:");
                foreach (var s in textNums)
                {
                    Console.WriteLine(s);
                }
            }

            [Category("Projection Operators")]
            [Description("This sample uses select to produce a sequence of the uppercase " +
                         "and lowercase versions of each word in the original array.")]
            public void DataSetLinq9()
            {

                var words = testDS.Tables["Words"].AsEnumerable();

                var upperLowerWords = words.Select(p => new
                {
                    Upper = (p.Field<string>(0)).ToUpper(),
                    Lower = (p.Field<string>(0)).ToLower()
                });

                foreach (var ul in upperLowerWords)
                {
                    Console.WriteLine("Uppercase: " + ul.Upper + ", Lowercase: " + ul.Lower);
                }
            }

            [Category("Projection Operators")]
            [Description("This sample uses select to produce a sequence containing text " +
                         "representations of digits and whether their length is even or odd.")]
            public void DataSetLinq10()
            {

                var numbers = testDS.Tables["Numbers"].AsEnumerable();
                var digits = testDS.Tables["Digits"];
                string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                var digitOddEvens = numbers.
                    Select(n => new
                    {
                        Digit = digits.Rows[n.Field<int>("number")]["digit"],
                        Even = (n.Field<int>("number") % 2 == 0)
                    });

                foreach (var d in digitOddEvens)
                {
                    Console.WriteLine("The digit {0} is {1}.", d.Digit, d.Even ? "even" : "odd");
                }
            }

            [Category("Projection Operators")]
            [Description("This sample uses select to produce a sequence containing some properties " +
                         "of Products, including UnitPrice which is renamed to Price " +
                         "in the resulting type.")]
            public void DataSetLinq11()
            {

                var products = testDS.Tables["Products"].AsEnumerable();

                var productInfos = products.
                    Select(n => new
                    {
                        ProductName = n.Field<string>("ProductName"),
                        Category = n.Field<string>("Category"),
                        Price = n.Field<decimal>("UnitPrice")
                    });

                Console.WriteLine("Product Info:");
                foreach (var productInfo in productInfos)
                {
                    Console.WriteLine("{0} is in the category {1} and costs {2} per unit.", productInfo.ProductName, productInfo.Category, productInfo.Price);
                }
            }


            [Category("Projection Operators")]
            [Description("This sample uses an indexed Select clause to determine if the value of ints " +
                         "in an array match their position in the array.")]
            public void DataSetLinq12()
            {

                var numbers = testDS.Tables["Numbers"].AsEnumerable();

                var numsInPlace = numbers.Select((num, index) => new
                {
                    Num = num.Field<int>("number"),
                    InPlace = (num.Field<int>("number") == index)
                });

                Console.WriteLine("Number: In-place?");
                foreach (var n in numsInPlace)
                {
                    Console.WriteLine("{0}: {1}", n.Num, n.InPlace);
                }
            }

            [Category("Projection Operators")]
            [Description("This sample combines select and where to make a simple query that returns " +
                         "the text form of each digit less than 5.")]
            public void DataSetLinq13()
            {

                var numbers = testDS.Tables["Numbers"].AsEnumerable();
                var digits = testDS.Tables["Digits"];

                var lowNums =
                    from n in numbers
                    where n.Field<int>("number") < 5
                    select digits.Rows[n.Field<int>("number")].Field<string>("digit");

                Console.WriteLine("Numbers < 5:");
                foreach (var num in lowNums)
                {
                    Console.WriteLine(num);
                }
            }

            [Category("Projection Operators")]
            [Description("This sample uses a compound from clause to make a query that returns all pairs " +
                         "of numbers from both arrays such that the number from numbersA is less than the number " +
                         "from numbersB.")]
            public void DataSetLinq14()
            {

                var numbersA = testDS.Tables["NumbersA"].AsEnumerable();
                var numbersB = testDS.Tables["NumbersB"].AsEnumerable();

                var pairs =
                    from a in numbersA
                    from b in numbersB
                    where a.Field<int>("number") < b.Field<int>("number")
                    select new { a = a.Field<int>("number"), b = b.Field<int>("number") };

                Console.WriteLine("Pairs where a < b:");
                foreach (var pair in pairs)
                {
                    Console.WriteLine("{0} is less than {1}", pair.a, pair.b);
                }
            }

            [Category("Projection Operators")]
            [Description("This sample uses a compound from clause to select all orders where the " +
                         "order total is less than 500.00.")]
            public void DataSetLinq15()
            {

                var customers = testDS.Tables["Customers"].AsEnumerable();
                var orders = testDS.Tables["Orders"].AsEnumerable();

                var smallOrders =
                    from c in customers
                    from o in orders
                    where c.Field<string>("CustomerID") == o.Field<string>("CustomerID")
                        && o.Field<decimal>("Total") < 500.00M
                    select new
                    {
                        CustomerID = c.Field<string>("CustomerID"),
                        OrderID = o.Field<int>("OrderID"),
                        Total = o.Field<decimal>("Total")
                    };

                ObjectDumper.Write(smallOrders);
            }

            [Category("Projection Operators")]
            [Description("This sample uses a compound from clause to select all orders where the " +
                         "order was made in 1998 or later.")]
            public void DataSetLinq16()
            {

                var customers = testDS.Tables["Customers"].AsEnumerable();
                var orders = testDS.Tables["Orders"].AsEnumerable();

                var myOrders =
                    from c in customers
                    from o in orders
                    where c.Field<string>("CustomerID") == o.Field<string>("CustomerID") &&
                    o.Field<DateTime>("OrderDate") >= new DateTime(1998, 1, 1)
                    select new
                    {
                        CustomerID = c.Field<string>("CustomerID"),
                        OrderID = o.Field<int>("OrderID"),
                        OrderDate = o.Field<DateTime>("OrderDate")
                    };

                ObjectDumper.Write(myOrders);
            }

            [Category("Projection Operators")]
            [Description("This sample uses a compound from clause to select all orders where the " +
                         "order total is greater than 2000.00 and uses from assignment to avoid " +
                         "requesting the total twice.")]
            public void DataSetLinq17()
            {

                var customers = testDS.Tables["Customers"].AsEnumerable();
                var orders = testDS.Tables["Orders"].AsEnumerable();

                var myOrders =
                    from c in customers
                    from o in orders
                    let total = o.Field<decimal>("Total")
                    where c.Field<string>("CustomerID") == o.Field<string>("CustomerID")
                        && total >= 2000.0M
                    select new { CustomerID = c.Field<string>("CustomerID"), OrderID = o.Field<int>("OrderID"), total };

                ObjectDumper.Write(myOrders);
            }

            [Category("Projection Operators")]
            [Description("This sample uses multiple from clauses so that filtering on customers can " +
                         "be done before selecting their orders.  This makes the query more efficient by " +
                         "not selecting and then discarding orders for customers outside of Washington.")]
            public void DataSetLinq18()
            {

                var customers = testDS.Tables["Customers"].AsEnumerable();
                var orders = testDS.Tables["Orders"].AsEnumerable();
                DateTime cutoffDate = new DateTime(1997, 1, 1);

                var myOrders =
                    from c in customers
                    where c.Field<string>("Region") == "WA"
                    from o in orders
                    where c.Field<string>("CustomerID") == o.Field<string>("CustomerID")
                    && (DateTime)o["OrderDate"] >= cutoffDate
                    select new { CustomerID = c.Field<string>("CustomerID"), OrderID = o.Field<int>("OrderID") };

                ObjectDumper.Write(myOrders);
            }

            [Category("Projection Operators")]
            [Description("This sample uses an indexed SelectMany clause to select all orders, " +
                         "while referring to customers by the order in which they are returned " +
                         "from the query.")]
            public void DataSetLinq19()
            {

                var customers = testDS.Tables["Customers"].AsEnumerable();
                var orders = testDS.Tables["Orders"].AsEnumerable();

                var customerOrders =
                    customers.SelectMany(
                        (cust, custIndex) =>
                        orders.Where(o => cust.Field<string>("CustomerID") == o.Field<string>("CustomerID"))
                            .Select(o => new { CustomerIndex = custIndex + 1, OrderID = o.Field<int>("OrderID") }));

                foreach (var c in customerOrders)
                {
                    Console.WriteLine("Customer Index: " + c.CustomerIndex +
                                        " has an order with OrderID " + c.OrderID);
                }
            }

            #endregion
        }
    }

    #region "Test Helper"
    internal class TestHelper
    {
        internal static DataSet CreateTestDataset()
        {
            DataSet ds = new DataSet();

            // Customers Table
            ds.Tables.Add(CreateNumbersTable());
            ds.Tables.Add(CreateLowNumbersTable());
            ds.Tables.Add(CreateEmptyNumbersTable());
            ds.Tables.Add(CreateProductList());
            ds.Tables.Add(CreateDigitsTable());
            ds.Tables.Add(CreateWordsTable());
            ds.Tables.Add(CreateWords2Table());
            ds.Tables.Add(CreateWords3Table());
            ds.Tables.Add(CreateWords4Table());
            ds.Tables.Add(CreateAnagramsTable());
            ds.Tables.Add(CreateNumbersATable());
            ds.Tables.Add(CreateNumbersBTable());
            ds.Tables.Add(CreateFactorsOf300());
            ds.Tables.Add(CreateDoublesTable());
            ds.Tables.Add(CreateScoreRecordsTable());
            ds.Tables.Add(CreateAttemptedWithdrawalsTable());
            ds.Tables.Add(CreateEmployees1Table());
            ds.Tables.Add(CreateEmployees2Table());

            CreateCustomersAndOrdersTables(ds);

            ds.AcceptChanges();
            return ds;
        }


        private static DataTable CreateNumbersTable()
        {

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            DataTable table = new DataTable("Numbers");
            table.Columns.Add("number", typeof(int));

            foreach (int n in numbers)
            {
                table.Rows.Add(new object[] { n });
            }

            return table;
        }

        private static DataTable CreateEmptyNumbersTable()
        {

            DataTable table = new DataTable("EmptyNumbers");
            table.Columns.Add("number", typeof(int));
            return table;
        }

        private static DataTable CreateDigitsTable()
        {

            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            DataTable table = new DataTable("Digits");
            table.Columns.Add("digit", typeof(string));

            foreach (string digit in digits)
            {
                table.Rows.Add(new object[] { digit });
            }
            return table;
        }

        private static DataTable CreateWordsTable()
        {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            DataTable table = new DataTable("Words");
            table.Columns.Add("word", typeof(string));

            foreach (string word in words)
            {
                table.Rows.Add(new object[] { word });
            }
            return table;
        }

        private static DataTable CreateWords2Table()
        {
            string[] words = { "believe", "relief", "receipt", "field" };
            DataTable table = new DataTable("Words2");
            table.Columns.Add("word", typeof(string));

            foreach (string word in words)
            {
                table.Rows.Add(new object[] { word });
            }
            return table;
        }

        private static DataTable CreateWords3Table()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            DataTable table = new DataTable("Words3");
            table.Columns.Add("word", typeof(string));

            foreach (string word in words)
            {
                table.Rows.Add(new object[] { word });
            }
            return table;
        }

        private static DataTable CreateWords4Table()
        {
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };
            DataTable table = new DataTable("Words4");
            table.Columns.Add("word", typeof(string));

            foreach (string word in words)
            {
                table.Rows.Add(new object[] { word });
            }
            return table;
        }

        private static DataTable CreateAnagramsTable()
        {
            string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            DataTable table = new DataTable("Anagrams");
            table.Columns.Add("anagram", typeof(string));

            foreach (string word in anagrams)
            {
                table.Rows.Add(new object[] { word });
            }
            return table;
        }

        private static DataTable CreateScoreRecordsTable()
        {
            var scoreRecords = new[] { new {Name = "Alice", Score = 50},
                                new {Name = "Bob"  , Score = 40},
                                new {Name = "Cathy", Score = 45}
                            };

            DataTable table = new DataTable("ScoreRecords");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Score", typeof(int));

            foreach (var r in scoreRecords)
            {
                table.Rows.Add(new object[] { r.Name, r.Score });
            }
            return table;
        }

        private static DataTable CreateAttemptedWithdrawalsTable()
        {
            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };

            DataTable table = new DataTable("AttemptedWithdrawals");
            table.Columns.Add("withdrawal", typeof(int));

            foreach (var r in attemptedWithdrawals)
            {
                table.Rows.Add(new object[] { r });
            }
            return table;
        }

        private static DataTable CreateNumbersATable()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            DataTable table = new DataTable("NumbersA");
            table.Columns.Add("number", typeof(int));

            foreach (int number in numbersA)
            {
                table.Rows.Add(new object[] { number });
            }
            return table;
        }

        private static DataTable CreateNumbersBTable()
        {
            int[] numbersB = { 1, 3, 5, 7, 8 };
            DataTable table = new DataTable("NumbersB");
            table.Columns.Add("number", typeof(int));

            foreach (int number in numbersB)
            {
                table.Rows.Add(new object[] { number });
            }
            return table;
        }

        private static DataTable CreateLowNumbersTable()
        {
            int[] lowNumbers = { 1, 11, 3, 19, 41, 65, 19 };
            DataTable table = new DataTable("LowNumbers");
            table.Columns.Add("number", typeof(int));

            foreach (int number in lowNumbers)
            {
                table.Rows.Add(new object[] { number });
            }
            return table;
        }

        private static DataTable CreateFactorsOf300()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };

            DataTable table = new DataTable("FactorsOf300");
            table.Columns.Add("factor", typeof(int));

            foreach (int factor in factorsOf300)
            {
                table.Rows.Add(new object[] { factor });
            }
            return table;
        }

        private static DataTable CreateDoublesTable()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            DataTable table = new DataTable("Doubles");
            table.Columns.Add("double", typeof(double));

            foreach (double d in doubles)
            {
                table.Rows.Add(new object[] { d });
            }
            return table;
        }

        private static DataTable CreateEmployees1Table()
        {

            DataTable table = new DataTable("Employees1");
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("worklevel", typeof(int));

            table.Rows.Add(new object[] { 1, "Jones", 5 });
            table.Rows.Add(new object[] { 2, "Smith", 5 });
            table.Rows.Add(new object[] { 2, "Smith", 5 });
            table.Rows.Add(new object[] { 3, "Smith", 6 });
            table.Rows.Add(new object[] { 4, "Arthur", 11 });
            table.Rows.Add(new object[] { 5, "Arthur", 12 });

            return table;
        }

        private static DataTable CreateEmployees2Table()
        {

            DataTable table = new DataTable("Employees2");
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("lastname", typeof(string));
            table.Columns.Add("level", typeof(int));

            table.Rows.Add(new object[] { 1, "Jones", 10 });
            table.Rows.Add(new object[] { 2, "Jagger", 5 });
            table.Rows.Add(new object[] { 3, "Thomas", 6 });
            table.Rows.Add(new object[] { 4, "Collins", 11 });
            table.Rows.Add(new object[] { 4, "Collins", 12 });
            table.Rows.Add(new object[] { 5, "Arthur", 12 });

            return table;
        }

        private static void CreateCustomersAndOrdersTables(DataSet ds)
        {

            DataTable customers = new DataTable("Customers");
            customers.Columns.Add("CustomerID", typeof(string));
            customers.Columns.Add("CompanyName", typeof(string));
            customers.Columns.Add("Address", typeof(string));
            customers.Columns.Add("City", typeof(string));
            customers.Columns.Add("Region", typeof(string));
            customers.Columns.Add("PostalCode", typeof(string));
            customers.Columns.Add("Country", typeof(string));
            customers.Columns.Add("Phone", typeof(string));
            customers.Columns.Add("Fax", typeof(string));

            ds.Tables.Add(customers);

            DataTable orders = new DataTable("Orders");

            orders.Columns.Add("OrderID", typeof(int));
            orders.Columns.Add("CustomerID", typeof(string));
            orders.Columns.Add("OrderDate", typeof(DateTime));
            orders.Columns.Add("Total", typeof(decimal));

            ds.Tables.Add(orders);

            DataRelation co = new DataRelation("CustomersOrders", customers.Columns["CustomerID"], orders.Columns["CustomerID"], true);
            ds.Relations.Add(co);

            var customerList = (
                from e in XDocument.Load(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ProjectionOperators.customers.xml")).
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
                }
                ).ToList();

            foreach (Customer cust in customerList)
            {
                customers.Rows.Add(new object[] {cust.CustomerID, cust.CompanyName, cust.Address, cust.City, cust.Region,
                                                cust.PostalCode, cust.Country, cust.Phone, cust.Fax});
                foreach (Order order in cust.Orders)
                {
                    orders.Rows.Add(new object[] { order.OrderID, cust.CustomerID, order.OrderDate, order.Total });
                }
            }
        }

        private static DataTable CreateProductList()
        {

            DataTable table = new DataTable("Products");
            table.Columns.Add("ProductID", typeof(int));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(decimal));
            table.Columns.Add("UnitsInStock", typeof(int));

            var productList = new[] {
              new { ProductID = 1, ProductName = "Chai", Category = "Beverages", 
                UnitPrice = 18.0000M, UnitsInStock = 39 },
              new { ProductID = 2, ProductName = "Chang", Category = "Beverages", 
                UnitPrice = 19.0000M, UnitsInStock = 17 },
              new { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments", 
                UnitPrice = 10.0000M, UnitsInStock = 13 },
              new { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments", 
                UnitPrice = 22.0000M, UnitsInStock = 53 },
              new { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments", 
                UnitPrice = 21.3500M, UnitsInStock = 0 },
              new { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", Category = "Condiments", 
                UnitPrice = 25.0000M, UnitsInStock = 120 },
              new { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", Category = "Produce", 
                UnitPrice = 30.0000M, UnitsInStock = 15 },
              new { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", Category = "Condiments", 
                UnitPrice = 40.0000M, UnitsInStock = 6 },
              new { ProductID = 9, ProductName = "Mishi Kobe Niku", Category = "Meat/Poultry", 
                UnitPrice = 97.0000M, UnitsInStock = 29 },
              new { ProductID = 10, ProductName = "Ikura", Category = "Seafood", 
                UnitPrice = 31.0000M, UnitsInStock = 31 },
              new { ProductID = 11, ProductName = "Queso Cabrales", Category = "Dairy Products", 
                UnitPrice = 21.0000M, UnitsInStock = 22 },
              new { ProductID = 12, ProductName = "Queso Manchego La Pastora", Category = "Dairy Products", 
                UnitPrice = 38.0000M, UnitsInStock = 86 },
              new { ProductID = 13, ProductName = "Konbu", Category = "Seafood", 
                UnitPrice = 6.0000M, UnitsInStock = 24 },
              new { ProductID = 14, ProductName = "Tofu", Category = "Produce", 
                UnitPrice = 23.2500M, UnitsInStock = 35 },
              new { ProductID = 15, ProductName = "Genen Shouyu", Category = "Condiments", 
                UnitPrice = 15.5000M, UnitsInStock = 39 },
              new { ProductID = 16, ProductName = "Pavlova", Category = "Confections", 
                UnitPrice = 17.4500M, UnitsInStock = 29 },
              new { ProductID = 17, ProductName = "Alice Mutton", Category = "Meat/Poultry", 
                UnitPrice = 39.0000M, UnitsInStock = 0 },
              new { ProductID = 18, ProductName = "Carnarvon Tigers", Category = "Seafood", 
                UnitPrice = 62.5000M, UnitsInStock = 42 },
              new { ProductID = 19, ProductName = "Teatime Chocolate Biscuits", Category = "Confections", 
                UnitPrice = 9.2000M, UnitsInStock = 25 },
              new { ProductID = 20, ProductName = "Sir Rodney's Marmalade", Category = "Confections", 
                UnitPrice = 81.0000M, UnitsInStock = 40 },
              new { ProductID = 21, ProductName = "Sir Rodney's Scones", Category = "Confections", 
                UnitPrice = 10.0000M, UnitsInStock = 3 },
              new { ProductID = 22, ProductName = "Gustaf's Kn�ckebr�d", Category = "Grains/Cereals", 
                UnitPrice = 21.0000M, UnitsInStock = 104 },
              new { ProductID = 23, ProductName = "Tunnbr�d", Category = "Grains/Cereals", 
                UnitPrice = 9.0000M, UnitsInStock = 61 },
              new { ProductID = 24, ProductName = "Guaran� Fant�stica", Category = "Beverages", 
                UnitPrice = 4.5000M, UnitsInStock = 20 },
              new { ProductID = 25, ProductName = "NuNuCa Nu�-Nougat-Creme", Category = "Confections", 
                UnitPrice = 14.0000M, UnitsInStock = 76 },
              new { ProductID = 26, ProductName = "Gumb�r Gummib�rchen", Category = "Confections", 
                UnitPrice = 31.2300M, UnitsInStock = 15 },
              new { ProductID = 27, ProductName = "Schoggi Schokolade", Category = "Confections", 
                UnitPrice = 43.9000M, UnitsInStock = 49 },
              new { ProductID = 28, ProductName = "R�ssle Sauerkraut", Category = "Produce", 
                UnitPrice = 45.6000M, UnitsInStock = 26 },
              new { ProductID = 29, ProductName = "Th�ringer Rostbratwurst", Category = "Meat/Poultry", 
                UnitPrice = 123.7900M, UnitsInStock = 0 },
              new { ProductID = 30, ProductName = "Nord-Ost Matjeshering", Category = "Seafood", 
                UnitPrice = 25.8900M, UnitsInStock = 10 },
              new { ProductID = 31, ProductName = "Gorgonzola Telino", Category = "Dairy Products", 
                UnitPrice = 12.5000M, UnitsInStock = 0 },
              new { ProductID = 32, ProductName = "Mascarpone Fabioli", Category = "Dairy Products", 
                UnitPrice = 32.0000M, UnitsInStock = 9 },
              new { ProductID = 33, ProductName = "Geitost", Category = "Dairy Products", 
                UnitPrice = 2.5000M, UnitsInStock = 112 },
              new { ProductID = 34, ProductName = "Sasquatch Ale", Category = "Beverages", 
                UnitPrice = 14.0000M, UnitsInStock = 111 },
              new { ProductID = 35, ProductName = "Steeleye Stout", Category = "Beverages", 
                UnitPrice = 18.0000M, UnitsInStock = 20 },
              new { ProductID = 36, ProductName = "Inlagd Sill", Category = "Seafood", 
                UnitPrice = 19.0000M, UnitsInStock = 112 },
              new { ProductID = 37, ProductName = "Gravad lax", Category = "Seafood", 
                UnitPrice = 26.0000M, UnitsInStock = 11 },
              new { ProductID = 38, ProductName = "C�te de Blaye", Category = "Beverages", 
                UnitPrice = 263.5000M, UnitsInStock = 17 },
              new { ProductID = 39, ProductName = "Chartreuse verte", Category = "Beverages", 
                UnitPrice = 18.0000M, UnitsInStock = 69 },
              new { ProductID = 40, ProductName = "Boston Crab Meat", Category = "Seafood", 
                UnitPrice = 18.4000M, UnitsInStock = 123 },
              new { ProductID = 41, ProductName = "Jack's New England Clam Chowder", Category = "Seafood", 
                UnitPrice = 9.6500M, UnitsInStock = 85 },
              new { ProductID = 42, ProductName = "Singaporean Hokkien Fried Mee", Category = "Grains/Cereals", 
                UnitPrice = 14.0000M, UnitsInStock = 26 },
              new { ProductID = 43, ProductName = "Ipoh Coffee", Category = "Beverages", 
                UnitPrice = 46.0000M, UnitsInStock = 17 },
              new { ProductID = 44, ProductName = "Gula Malacca", Category = "Condiments", 
                UnitPrice = 19.4500M, UnitsInStock = 27 },
              new { ProductID = 45, ProductName = "Rogede sild", Category = "Seafood", 
                UnitPrice = 9.5000M, UnitsInStock = 5 },
              new { ProductID = 46, ProductName = "Spegesild", Category = "Seafood", 
                UnitPrice = 12.0000M, UnitsInStock = 95 },
              new { ProductID = 47, ProductName = "Zaanse koeken", Category = "Confections", 
                UnitPrice = 9.5000M, UnitsInStock = 36 },
              new { ProductID = 48, ProductName = "Chocolade", Category = "Confections", 
                UnitPrice = 12.7500M, UnitsInStock = 15 },
              new { ProductID = 49, ProductName = "Maxilaku", Category = "Confections", 
                UnitPrice = 20.0000M, UnitsInStock = 10 },
              new { ProductID = 50, ProductName = "Valkoinen suklaa", Category = "Confections", 
                UnitPrice = 16.2500M, UnitsInStock = 65 },
              new { ProductID = 51, ProductName = "Manjimup Dried Apples", Category = "Produce", 
                UnitPrice = 53.0000M, UnitsInStock = 20 },
              new { ProductID = 52, ProductName = "Filo Mix", Category = "Grains/Cereals", 
                UnitPrice = 7.0000M, UnitsInStock = 38 },
              new { ProductID = 53, ProductName = "Perth Pasties", Category = "Meat/Poultry", 
                UnitPrice = 32.8000M, UnitsInStock = 0 },
              new { ProductID = 54, ProductName = "Tourti�re", Category = "Meat/Poultry", 
                UnitPrice = 7.4500M, UnitsInStock = 21 },
              new { ProductID = 55, ProductName = "P�t� chinois", Category = "Meat/Poultry", 
                UnitPrice = 24.0000M, UnitsInStock = 115 },
              new { ProductID = 56, ProductName = "Gnocchi di nonna Alice", Category = "Grains/Cereals", 
                UnitPrice = 38.0000M, UnitsInStock = 21 },
              new { ProductID = 57, ProductName = "Ravioli Angelo", Category = "Grains/Cereals", 
                UnitPrice = 19.5000M, UnitsInStock = 36 },
              new { ProductID = 58, ProductName = "Escargots de Bourgogne", Category = "Seafood", 
                UnitPrice = 13.2500M, UnitsInStock = 62 },
              new { ProductID = 59, ProductName = "Raclette Courdavault", Category = "Dairy Products", 
                UnitPrice = 55.0000M, UnitsInStock = 79 },
              new { ProductID = 60, ProductName = "Camembert Pierrot", Category = "Dairy Products", 
                UnitPrice = 34.0000M, UnitsInStock = 19 },
              new { ProductID = 61, ProductName = "Sirop d'�rable", Category = "Condiments", 
                UnitPrice = 28.5000M, UnitsInStock = 113 },
              new { ProductID = 62, ProductName = "Tarte au sucre", Category = "Confections", 
                UnitPrice = 49.3000M, UnitsInStock = 17 },
              new { ProductID = 63, ProductName = "Vegie-spread", Category = "Condiments", 
                UnitPrice = 43.9000M, UnitsInStock = 24 },
              new { ProductID = 64, ProductName = "Wimmers gute Semmelkn�del", Category = "Grains/Cereals", 
                UnitPrice = 33.2500M, UnitsInStock = 22 },
              new { ProductID = 65, ProductName = "Louisiana Fiery Hot Pepper Sauce", Category = "Condiments", 
                UnitPrice = 21.0500M, UnitsInStock = 76 },
              new { ProductID = 66, ProductName = "Louisiana Hot Spiced Okra", Category = "Condiments", 
                UnitPrice = 17.0000M, UnitsInStock = 4 },
              new { ProductID = 67, ProductName = "Laughing Lumberjack Lager", Category = "Beverages", 
                UnitPrice = 14.0000M, UnitsInStock = 52 },
              new { ProductID = 68, ProductName = "Scottish Longbreads", Category = "Confections", 
                UnitPrice = 12.5000M, UnitsInStock = 6 },
              new { ProductID = 69, ProductName = "Gudbrandsdalsost", Category = "Dairy Products", 
                UnitPrice = 36.0000M, UnitsInStock = 26 },
              new { ProductID = 70, ProductName = "Outback Lager", Category = "Beverages", 
                UnitPrice = 15.0000M, UnitsInStock = 15 },
              new { ProductID = 71, ProductName = "Flotemysost", Category = "Dairy Products", 
                UnitPrice = 21.5000M, UnitsInStock = 26 },
              new { ProductID = 72, ProductName = "Mozzarella di Giovanni", Category = "Dairy Products", 
                UnitPrice = 34.8000M, UnitsInStock = 14 },
              new { ProductID = 73, ProductName = "R�d Kaviar", Category = "Seafood", 
                UnitPrice = 15.0000M, UnitsInStock = 101 },
              new { ProductID = 74, ProductName = "Longlife Tofu", Category = "Produce", 
                UnitPrice = 10.0000M, UnitsInStock = 4 },
              new { ProductID = 75, ProductName = "Rh�nbr�u Klosterbier", Category = "Beverages", 
                UnitPrice = 7.7500M, UnitsInStock = 125 },
              new { ProductID = 76, ProductName = "Lakkalik��ri", Category = "Beverages", 
                UnitPrice = 18.0000M, UnitsInStock = 57 },
              new { ProductID = 77, ProductName = "Original Frankfurter gr�ne So�e", Category = "Condiments", 
                UnitPrice = 13.0000M, UnitsInStock = 32 }
            };

            foreach (var x in productList)
            {
                table.Rows.Add(new object[] { x.ProductID, x.ProductName, x.Category, x.UnitPrice, x.UnitsInStock });
            }

            return table;
        }
    }

    public class Customer
    {
        public Customer(string customerID, string companyName)
        {
            CustomerID = customerID;
            CompanyName = companyName;
            Orders = new Order[10];
        }

        public Customer() { }

        public string CustomerID;
        public string CompanyName;
        public string Address;
        public string City;
        public string Region;
        public string PostalCode;
        public string Country;
        public string Phone;
        public string Fax;
        public Order[] Orders;
    }

    public class Order
    {
        public Order(int orderID, DateTime orderDate, decimal total)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            Total = total;
        }

        public Order() { }

        public int OrderID;
        public DateTime OrderDate;
        public decimal Total;
    }

    #endregion
}
