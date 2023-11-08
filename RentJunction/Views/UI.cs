using commonData;
using RentJunction.Controller;
using RentJunction.Models;
using RentJunction.Views;
public class UI
{
    public static void StartMenu()
    {
        while (true)
        {
            Console.WriteLine(Message.startMenu);
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
                        Console.WriteLine(Message.invalid);
                        break;
                }
        }
    }
    public static void Register()
    {
            User user = Commonn.Details();
       
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
        Console.WriteLine();
        while (!CheckValidity.CheckNull(username))
        {
            Console.WriteLine(Message.userNameNotEmpty);
            username = Console.ReadLine();
        }

        Console.WriteLine(Message.pswd);
        string password = CheckValidity.HideCharacter();
        Console.WriteLine();
        Message.Design();

        while (!CheckValidity.CheckNull(password))
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
                    Console.WriteLine(customer.FullName + Message.loginCust);
                    Message.Design();
                    custUIObj.LoginCustomerMenu(customer);
                }
                else if (entity is Owner)
                {
                    OwnerUI oui = new OwnerUI();
                    Owner owner = (Owner)entity;
                    Console.WriteLine(owner.FullName + Message.loginOwner);
                    Message.Design();
                    oui.LoginOwnerMenu(owner);
                }
                else if(entity is Admin) 
                {
                    AdminUI adm = new AdminUI();
                    Admin admin = (Admin)entity;
                    Console.WriteLine(Message.loginAdmin + admin.Username);
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