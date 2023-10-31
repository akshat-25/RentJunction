namespace MenuOpt
{
    public enum Category
    {
        Property = 1,
        Electronics = 2,
        Computer_Accessories = 3,
        Audio_Visual = 4,
        Security_Systems = 5,
        Clothes_Jewellery = 6,
        Generator = 7,
        Media_Entertainment_Equipment = 8,
        Vehicle = 9,
        Health_Supplements = 10,
        Furniture = 11,
        Miscellaneous = 12,
    }
    public enum CustomerMenu
    {
        BrowseProducts = 1,
        View_rented_products = 2,
        Extend_rent_period = 3,
        logout = 4
    }
    public enum OwnerMenu
    {
        add_product = 1,
        view_listed_products = 2,
        update_listed_products = 3,
        delete_listed_products = 4,
        logout = 5
    }
    public enum Menu
    {
        create_account = 1,
        login = 2,
        exit = 3,
    }
    public enum Options
    {
        view_customer = 1,
        view_owners = 2,
        delete_customer = 3,
        delete_owner = 4,
        add_admin = 5,
        logout = 6,
    }
    public enum UpdateProductMenu
    {
        product_name = 1,
        product_description = 2,
        product_category = 4,
        product_price = 3,
    }
    public class MenuOptions
    {
       
        public static void customerMenu()
        {
            Console.WriteLine("Choose an option : ");
            Console.WriteLine("1. Browse Products");
            Console.WriteLine("2. View Rented Products");
            Console.WriteLine("3. Extend Rent Period");
            Console.WriteLine("4. Logout");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
        }
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