
using RentJunction.Controller;

public class AdminUI
{
    AdminController admCtrl = new AdminController();
    public void LoginAdminMenu(Admin admin)
    {
        Console.WriteLine(Message.adminMenu);
        Console.WriteLine(Message.design);
        Options input = (Options)CheckValidity.IsValidInput();
        
        Console.WriteLine();

        switch (input)
        {
            case Options.view_customer:
                ViewAllCustomers(admin);
                LoginAdminMenu(admin);
                break;
            case Options.view_owners:
                ViewAllOwners(admin);
                Console.WriteLine(Message.design);
                LoginAdminMenu(admin);
                break;
            case Options.delete_customer:
                DeleteCustomer(admin);
                LoginAdminMenu(admin);
                break;
            case Options.delete_owner:
                DeleteOwner(admin);
                LoginAdminMenu(admin);
                break; 
            case Options.add_admin:
                AddNewAdmin();
                LoginAdminMenu(admin);
                break;
            case Options.logout:
                Console.WriteLine(Message.logoutSuccc);
                Console.WriteLine();
                UI.StartMenu();
                break;
            default:
                Console.WriteLine(Message.invalid);
                LoginAdminMenu(admin);
                break;
        }
    }
    public void ViewAllCustomers(Admin admin)
    {

        List<Customer> customers = admCtrl.getCustomer();
        foreach (var customer in customers)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Name         -   {customer.FullName}");
            Console.WriteLine($"Address      -   {customer.Address}");
            Console.WriteLine($"Phone        -   {customer.PhoneNumber}");
            Console.WriteLine($"Email        -   {customer.Email}");
        }
    }
    public void ViewAllOwners(Admin admin)
    {
        List<Owner> owners = admCtrl.getOwners();
        Console.WriteLine(Message.custList);
        foreach (var owner in owners)
        {
            Console.WriteLine(Message.design);
            Console.WriteLine($"Name         -   {owner.FullName}");
            Console.WriteLine($"Address      -   {owner.Address}");
            Console.WriteLine($"Phone        -   {owner.PhoneNumber}");
            Console.WriteLine($"Email        -   {owner.Email}");
        }
    }
    public void DeleteCustomer(Admin admin) {
        ViewAllCustomers(admin);
        Console.WriteLine(Message.custEmail);
        string input = Console.ReadLine();

        while (!CheckValidity.IsValidEmail(input) || !CheckValidity.checkNull(input))
        {
            Console.WriteLine(Message.invalid);
            input = Console.ReadLine().Trim();
        }
        Console.WriteLine();
        Console.WriteLine();

        List<Customer> customers = admCtrl.getCustomer();

        foreach (var customer in customers)
        {
            if (customer.Email.Equals(input))
            {
                if (customer.rentedProducts == null)
                {
                    customers.Remove(customer);
                    Console.WriteLine($"{customer.FullName} Deleted Successfully");
                    admCtrl.updateDBCust(customers);
                    break;
                }
                else
                {
                    Console.WriteLine(Message.cannotDeleteCust);
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine();

    }
    public void DeleteOwner(Admin admin) {
        ViewAllOwners(admin);
        Console.WriteLine(Message.OwnEmail);
        var input = Console.ReadLine();
        while (!CheckValidity.IsValidEmail(input) || !CheckValidity.checkNull(input))
        {
            Console.WriteLine(Message.invalid);
            input = Console.ReadLine().Trim();
        }
        Console.WriteLine();
        Console.WriteLine();

        List<Owner> owners = admCtrl.getOwners();

        foreach (var owner in owners)
        {
            if (owner.Email.Equals(input))
            {
                owners.Remove(owner);
                Console.WriteLine($"{owner.FullName} Deleted Successfully");
                admCtrl.UPdateDbOwner(owners);
                
                break;
            }
            
        }
        
        
        }   
    public void AddNewAdmin()
    {
        Console.WriteLine(Message.username);
        string input = CheckValidity.IsValidUsername();
        
        Console.WriteLine();

        Console.WriteLine(Message.adminpswd);
        string pass = CheckValidity.IsValidPassword();
        Console.WriteLine();
        Admin admin = new Admin
        {
            Username = input,
            Password = pass,
        };

        if (AuthManager.Instance.Register(admin))
        {
            Console.WriteLine(Message.adminSucc);
        }
        else
        {
            Console.WriteLine(Message.somethingWrong);
        }
        Console.WriteLine();
        Console.WriteLine(Message.design);

    }

}



