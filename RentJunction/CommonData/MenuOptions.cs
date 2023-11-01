namespace MenuOpt
{
    public class MenuOptions
    {
        public static void ownerMenu()
        {
            Console.WriteLine("Choose an option : ");
            Console.WriteLine("1. Add Products for rent");
            Console.WriteLine("2. View your listed Products");
            Console.WriteLine("3. Update your listed Product");
            Console.WriteLine("4. Delete your listed Product");
            Console.WriteLine("5. Logout");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
        }
        public static void updateListedProdMenuOptions()
        {
            Console.WriteLine("Select Field number to be updated");
            Console.WriteLine("1. Product Name");
            Console.WriteLine("2. Product Description");
            Console.WriteLine("3. Product Price");
            Console.WriteLine("4. Product Category");
        }
        public static void startMenu()
        {
            Console.WriteLine("----------------Rent Junction - Renting Made Easy----------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Choose an option : ");
            Console.WriteLine("1. Create an account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit App");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
        }
        public static void adminMenu()
        {
            Console.WriteLine("Choose an option : ");
            Console.WriteLine("1. View All Customers");
            Console.WriteLine("2. View All Owners");
            Console.WriteLine("3. Delete a Customer");
            Console.WriteLine("4. Delete a Owner");
            Console.WriteLine("5. Add new Admin");
            Console.WriteLine("6. Logout");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
        }
    }
}