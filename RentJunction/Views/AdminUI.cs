using RentJunction.Controller;
using RentJunction.Models;

public class AdminUI
{
    ICustomerController custCtrl;
    IOwnerController ownerCtrl;
    public AdminUI() 
    { 
       custCtrl = new CustomerController();
       ownerCtrl = new OwnerController();
    }
    public void LoginAdminMenu(Admin admin)
    {
        Console.WriteLine(Strings.adminMenu);
        Console.WriteLine(Strings.design);
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
                Console.WriteLine(Strings.design);
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
                Console.WriteLine(Strings.logoutSuccc);
                Console.WriteLine();
                UI.StartMenu();
                break;
            default:
                Console.WriteLine(Strings.invalid);
                LoginAdminMenu(admin);
                break;
        }
    }
    public void ViewAllCustomers(Admin admin)
    {

        List<Customer> customers = custCtrl.GetCustomer();
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
        List<Owner> owners = ownerCtrl.GetOwnerList();
        Console.WriteLine(Strings.custList);
        foreach (var owner in owners)
        {
            Console.WriteLine(Strings.design);
            Console.WriteLine($"Name         -   {owner.FullName}");
            Console.WriteLine($"Address      -   {owner.Address}");
            Console.WriteLine($"Phone        -   {owner.PhoneNumber}");
            Console.WriteLine($"Email        -   {owner.Email}");
        }
    }
    public void DeleteCustomer(Admin admin) {
        ViewAllCustomers(admin);
        Console.WriteLine(Strings.custEmail);
        string input = Console.ReadLine();

        while (!CheckValidity.IsValidEmail(input) || !CheckValidity.CheckNull(input))
        {
            Console.WriteLine(Strings.invalid);
            input = Console.ReadLine().Trim();
        }
        Console.WriteLine();
        Console.WriteLine();

        List<Customer> customers = custCtrl.GetCustomer();

        foreach (var customer in customers)
        {
            if (customer.Email.Equals(input))
            {
                if (customer.rentedProducts == null)
                {
                    customers.Remove(customer);
                    Console.WriteLine($"{customer.FullName} Deleted Successfully");
                    custCtrl.UpdateDBCust(customers);
                    break;
                }
                else
                {
                    Console.WriteLine(Strings.cannotDeleteCust);
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine();

    }
    public void DeleteOwner(Admin admin) {
        ViewAllOwners(admin);
        Console.WriteLine(Strings.OwnEmail);
        var input = Console.ReadLine();
        while (!CheckValidity.IsValidEmail(input) || !CheckValidity.CheckNull(input))
        {
            Console.WriteLine(Strings.invalid);
            input = Console.ReadLine().Trim();
        }
        Console.WriteLine();
        Console.WriteLine();

        List<Owner> owners = ownerCtrl.GetOwnerList();

        foreach (var owner in owners)
        {
            if (owner.Email.Equals(input))
            {
                owners.Remove(owner);
                Console.WriteLine($"{owner.FullName} Deleted Successfully");
                ownerCtrl.UpdateDBOwner(owners);
                
                break;
            }
            
        }
        
        
        }   
    public void AddNewAdmin()
    {
        IAuthController athManager = new AuthController();
        Console.WriteLine(Strings.username);
        string input = CheckValidity.IsValidUsername();
        
        Console.WriteLine();

        Console.WriteLine(Strings.adminpswd);
        string pass = CheckValidity.IsValidPassword();
        Console.WriteLine();
        Admin admin = new Admin
        {
            Username = input,
            Password = pass,
        };

        if (athManager.Register(admin))
        {
            Console.WriteLine(Strings.adminSucc);
        }
        else
        {
            Console.WriteLine(Strings.somethingWrong);
        }
        Console.WriteLine();
        Console.WriteLine(Strings.design);

    }

}



