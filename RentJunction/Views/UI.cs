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
            Menu input = (Menu)CheckValidity.IsValidInput();

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
                        Console.WriteLine(Message.invalid);
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
            Console.WriteLine(Message.regsuccess);
            Message.Design();
            Console.WriteLine(Message.enterCred);
            Login();
            }
            else
            {
            Console.WriteLine(Message.wrong);
            Message.Design();
            Register();
        }
     }
    public static void Login()
    {
        Console.WriteLine(Message.enterUserName);
        string username = Console.ReadLine();

        while (!CheckValidity.checkNull(username))
        {
            Console.WriteLine(Message.userNameNotEmpty);
            username = Console.ReadLine();
        }

        Console.WriteLine(Message.pswd);
        string password = CheckValidity.HideCharacter();
        Console.WriteLine();
        Message.Design();
        while (!CheckValidity.checkNull(password))
        {
            Console.WriteLine(Message.passwordNameNotEmpty);
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
                Console.WriteLine(Message.loginFailed);
                Message.Design();
            }

           
        }
        catch(Exception ex)
        {
            Console.WriteLine(Message.error + ex);
            Message.Design();
        }
       Message.Design();
    }

}