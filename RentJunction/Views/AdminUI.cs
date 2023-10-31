using commonData;
using MenuOpt;
using RentJunction.Controller;


public class AdminUI
{
    AdminController admCtrl = new AdminController();
    public void LoginAdminMenu(Admin admin)
    {
        MenuOptions.adminMenu();
        int input = Commonn.IsValidInput();
        
        Console.WriteLine();

        switch (input)
        {
            case (int)Options.view_customer:
                ViewAllCustomers(admin);
                LoginAdminMenu(admin);
                break;
            case (int)Options.view_owners:
                ViewAllOwners(admin);
                Console.WriteLine("-------------------------------------------");
                LoginAdminMenu(admin);
                break;
            case (int)Options.delete_customer:
                DeleteCustomer(admin);
                LoginAdminMenu(admin);
                break;
            case (int)Options.delete_owner:
                DeleteOwner(admin);
                LoginAdminMenu(admin);
                break; 
            case (int)Options.add_admin:
                AddNewAdmin();
                LoginAdminMenu(admin);
                break;
            case (int)Options.logout:
                Console.WriteLine("Logout Successful!!!");
                Console.WriteLine();
                UI.StartMenu();
                break;
            default:
                Console.WriteLine("Invalid Option");
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
        Console.WriteLine("The owners list is as follows ->");
        foreach (var owner in owners)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Name         -   {owner.FullName}");
            Console.WriteLine($"Address      -   {owner.Address}");
            Console.WriteLine($"Phone        -   {owner.PhoneNumber}");
            Console.WriteLine($"Email        -   {owner.Email}");
        }
    }
    public void DeleteCustomer(Admin admin) {
        ViewAllCustomers(admin);
        Console.WriteLine("Enter the Email of the customer you want to delete from the list -");
        string input = Console.ReadLine();

        while (!Commonn.IsValidEmail(input) || !Commonn.checkNull(input))
        {
            Console.WriteLine("Please enter a valid email address");
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
                    Console.WriteLine("Cannot delete the customer as he has rented some products.");
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine();

    }
    public void DeleteOwner(Admin admin) {
        ViewAllOwners(admin);
        Console.WriteLine("Enter the Email of the owner you want to delete from the list -");
        var input = Console.ReadLine();
        while (!Commonn.IsValidEmail(input) || !Commonn.checkNull(input))
        {
            Console.WriteLine("Please enter a valid email address");
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
        Console.WriteLine("Enter username of new admin -");
        string input = Commonn.IsValidUsername();
        
        Console.WriteLine();

        Console.WriteLine("Enter password of new admin -");
        string pass = Commonn.IsValidPassword();
     
        if (AuthManager.Instance.AddAdmin(input, pass))
        {
            Console.WriteLine("Admin added successfully..");
        }
        else
        {
            Console.WriteLine("Something went wrong");
        }
        Console.WriteLine();

    }
}



