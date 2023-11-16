using RentJunction.Controller;
using RentJunction.Models;
using RentJunction.Views;
public class UI
{
    public static void StartMenu()
    {
        while (true)
        {
            Console.WriteLine(Strings.startMenu);
            Menu input = (Menu)CheckValidity.IsValidInput();
            Console.WriteLine();
            switch (input)
                {
                    case Menu.create_account:
                        Register();
                        break;
                    case Menu.login:
                        Login();
                        break;
                    case Menu.exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine(Strings.invalid);
                        break;
                }
        }
    }
    public static void Register()
    {
            IAuthController authManager = new AuthController();

            User user = RequestUserInput.Details();
       
            bool flag = authManager.Register(user);
            
            if (flag)
            {
            Console.WriteLine(Strings.regsuccess);
            Strings.Design();
            Console.WriteLine(Strings.enterCred);
            Login();
            }
            else
            {
            Console.WriteLine(Strings.wrong);
            Strings.Design();
            Register();
        }
     }
    public static void Login()
    {
        IAuthController authManager = new AuthController();

        Console.WriteLine(Strings.enterUserName);
        string username = Console.ReadLine();
        Console.WriteLine();
        while (!CheckValidity.CheckNull(username))
        {
            Console.WriteLine(Strings.userNameNotEmpty);
            username = Console.ReadLine();
        }

        Console.WriteLine(Strings.pswd);
        string password = CheckValidity.HideCharacter();
        Console.WriteLine();
        Strings.Design();

        while (!CheckValidity.CheckNull(password))
        {
            Console.WriteLine(Strings.passwordNameNotEmpty);
            password = Console.ReadLine();
        }

        try
        {
            var entity = authManager.Login(username, password);

            if (entity != null)
            {
                if (entity is Customer)
                {
                    var custUIObj = new CustomerUI();
                    var customer = (Customer)entity;
                    Console.WriteLine(customer.FullName + Strings.loginCust);
                    Strings.Design();
                    custUIObj.LoginCustomerMenu(customer);
                }
                else if (entity is Owner)
                {
                    var oui = new OwnerUI();
                    var owner = (Owner)entity;
                    Console.WriteLine(owner.FullName + Strings.loginOwner);
                    Strings.Design();
                    oui.LoginOwnerMenu(owner);
                }
                else if(entity is Admin) 
                {
                    var adm = new AdminUI();
                    var admin = (Admin)entity;
                    Console.WriteLine(Strings.loginAdmin + admin.Username);
                    Strings.Design();
                    adm.LoginAdminMenu(admin);
                }
                
            }
            else
            {
                Console.WriteLine(Strings.loginFailed);
                Strings.Design();
            }

           
        }
        catch(Exception ex)
        {
            Console.WriteLine(Strings.error + ex);
            Strings.Design();
        }
       Strings.Design();
    }

}