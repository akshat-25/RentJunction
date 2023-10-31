using commonData;
using MenuOpt;
using RentJunction.Controller;
using RentJunction.Models;
using RentJunction.Views;


public class UI
{
    public static void StartMenu()
    {

        while (true)
        {
            MenuOptions.startMenu();
            int input = Commonn.IsValidInput();

            switch (input)
                {
                    case (int)Menu.create_account:
                        Register();
                        break;
                    case (int)Menu.login:
                        Login();
                        break;
                    case (int)Menu.exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
        }
    }
    public static void Register()
    {
        User user = Commonn.Details();

        Console.WriteLine(user.FullName);
            bool flag = AuthManager.Instance.Register(user);
            if (flag)
            {
            Console.WriteLine("Registered Successfully!!");
            Message.Design();
            Console.WriteLine("Enter credentials to Log in to the Application");
            Login();
            }
            else
            {
            Console.WriteLine("Something went wrong....");
            Message.Design();
            Register();
        }
     }
    public static void Login()
    {
        Console.WriteLine("Enter your username ->");
        string username = Console.ReadLine();

        while (!Commonn.checkNull(username))
        {
            Console.WriteLine("Username cannot be empty. Please try again...");
            username = Console.ReadLine();
        }

        Console.WriteLine("Enter Password ->");
        string password = Console.ReadLine();
        while (!Commonn.checkNull(password))
        {
            Console.WriteLine("Password cannot be empty. Please try again...");
            password = Console.ReadLine();
        }

        try
        {
            var entity = AuthManager.Instance.Login(username, password);

            if (entity != null)
            {
                if (entity is Customer)
                {
                    CustomerUI custUIObj = new CustomerUI();
                    Customer customer = (Customer)entity;
                    Console.WriteLine($"{customer.FullName} logged in successfully as Customer");
                    Message.Design();
                    custUIObj.LoginCustomerMenu(customer);

                }
                else if (entity is Owner)
                {
                    OwnerUI oui = new OwnerUI();
                    RentJunction.Controller.Owner owner = (Owner)entity;
                    Console.WriteLine($"{owner.FullName} logged in successfully as Owner");
                    Message.Design();
                    oui.LoginOwnerMenu(owner);
                }

                else if(entity is Admin) 
                {
                    AdminUI adm = new AdminUI();
                    Admin admin = (Admin)entity;
                    Console.WriteLine($"Successfully logged in as ADMIN -> {admin.Username}");
                    Message.Design();
                    adm.LoginAdminMenu(admin);
                }
                
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials.");
                Message.Design();
            }

           
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occured...." + ex);
            Message.Design();
        }
       Message.Design();
    }

}