using RentJunction.Models;

namespace RentJunction.Tests
{
    public class DummyData
    {
       public static List<User> dummyOwnerList = new List<User>()
       {
           new User {  UserID  = "aa34rkfa", FullName = "Owner1", Role = Role.Owner },
           new User {  UserID  = "qjd34bni", FullName = "Owner2", Role = Role.Owner }
       };

       public static List<User> dummyCustomerList = new List<User>()
       {
           new User {  UserID  = "aa34rkfa", FullName = "User1", Role = Role.Customer },
           new User {  UserID  = "qjd34bni", FullName = "User2", Role = Role.Customer }
       };

       public static List<User> dummyUserList = new List<User>()
       {
           new User {  UserID  = "aa34rkfa", FullName = "User1", Role = Role.Customer , Username = "user123" , Password = "User@1234" },
           new User {  UserID  = "qjd34bni", FullName = "User2", Role = Role.Owner, Username = "user121" , Password = "User@123" },
           new User {  UserID  = "aed4rkfa", FullName = "User3", Role = Role.Admin , Username = "user113" , Password = "User@1234"},
           new User {  UserID  = "rtd34bni", FullName = "User4", Role = Role.Customer, Username = "user323" , Password = "User@1234" }
       };

       public static List<Product> dummyProductList = new List<Product>()
       {
            new Product
            {
             ProductId = 1,
             ProductName = "Product 1",
             City = "City 1"
            }, 
            new Product
            {
             ProductId = 2,
             ProductName = "Product 2",
             City = "City 2"
            }, 
            new Product
            {
             ProductId = 3,
             ProductName = "Product 3",
             City = "City 3"
            },
       };

        public static List<Product> GetListedProductByOwner = new List<Product>()
        {
            new Product
            {
             ProductId = 1,
             ProductName = "Product 1",
             City = "City 1",
             OwnerID = "ce8un3y"
            },
            new Product
            {
             ProductId = 2,
             ProductName = "Product 2",
             City = "City 2",
             OwnerID = "ce8un3y"
            },
            new Product
            {
             ProductId = 3,
             ProductName = "Product 3",
             City = "City 3",
             OwnerID = "ce8un3y"
            },
        };

        public static List<Product> GetRentedProductByCustomer = new List<Product>()
        {
            new Product
            {
             ProductId = 1,
             ProductName = "Product 1",
             City = "City 1",
             CustomerID = "ce8un3y"
            },
            new Product
            {
             ProductId = 2,
             ProductName = "Product 2",
             City = "City 2",
             CustomerID = "ce8un3"
            },
            new Product
            {
             ProductId = 3,
             ProductName = "Product 3",
             City = "City 3",
             CustomerID = "ce8un3y"
            },
        };

        public static List<Product> GetProducts = new List<Product>()
        {
            new Product
            {
             ProductId = 1,
             ProductName = "Product 1",
             City = "Test",
             ProductCategory = 1
            },
            new Product
            {
             ProductId = 2,
             ProductName = "Product 2",
             City = "Test",
             ProductCategory = 1
            },
            new Product
            {
             ProductId = 3,
             ProductName = "Product 3",
             City = "Test",
             ProductCategory = 1
            },
        };

        public static List<string> category = new List<string>()
        {
            "1.  Property",
            "2.  Electronics",
            "3.  Computer_Accessories",
            "4.  Audio_Visual",
            "5.  Security_Systems",
            "6.  Clothes_Jewellery",
            "7.  Generator ",
            "8.  Media_Entertainment_Equipment",
            "9.  Vehicle",
            "10. Health_Supplements",
            "11. Furniture",
            "12. Miscellaneous",
        };
    }
}