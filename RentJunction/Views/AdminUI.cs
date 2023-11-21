
using RentJunction.Models;
public class AdminUI
{
    public ICustomerController CustomerController { get; set; }
    public IOwnerController OwnerController { get; set; }
    public UserController UserController { get; set; }
    public AdminUI(ICustomerController customerController, IOwnerController ownerController, UserController userController)
    {
        CustomerController = customerController;
        OwnerController = ownerController;
        UserController = userController;
    }
    public void LoginAdminMenu(User admin)
    {
        Console.WriteLine(Strings.loginAdmin + admin.Username);
        Strings.Design();
        Console.WriteLine(Strings.adminMenu);
        Console.WriteLine(Strings.design);

        var input = Console.ReadLine();
        var isValidInput = CheckValidity.IsValidInput(input);

        while (true)
        {
            if (!isValidInput)
            {
                input = Console.ReadLine();
                Console.WriteLine(Strings.invalid);
            }
            else
            {
                break;
            }
        }
        
        Options option = Enum.Parse<Options>(input);
        
        Console.WriteLine();

        switch (option)
        {
            case Options.view_customer:
                ViewAllCustomers();
                LoginAdminMenu(admin);
                break;
            case Options.view_owners:
                ViewAllOwners();
                Console.WriteLine(Strings.design);
                LoginAdminMenu(admin);
                break;
            case Options.delete_customer:
                DeleteCustomer();
                LoginAdminMenu(admin);
                break;
            case Options.delete_owner:
                DeleteOwner();
                LoginAdminMenu(admin);
                break;
            case Options.add_admin:
                AddNewAdmin();
                LoginAdminMenu(admin);
                break;
            case Options.logout:
                Console.WriteLine(Strings.logoutSuccc);
                Console.WriteLine();
                return;
            default:
                Console.WriteLine(Strings.invalid);
                LoginAdminMenu(admin);
                break;
        }
    }
    public void ViewAllCustomers()
    {
        List<User> customers = CustomerController.GetCustomer();
        foreach (var customer in customers)
        {
            Console.WriteLine(Strings.design);
            Console.WriteLine($"CustomerID   -   {customer.UserID}");
            Console.WriteLine($"Name         -   {customer.FullName}");
            Console.WriteLine($"Address      -   {customer.City}");
            Console.WriteLine($"Phone        -   {customer.PhoneNumber}");
            Console.WriteLine($"Email        -   {customer.Email}");
        }
    }
    public void ViewAllOwners()
    {
        List<User> owners = OwnerController.GetOwnerList();
        Console.WriteLine(Strings.custList);
        foreach (var owner in owners)
        {
            Console.WriteLine(Strings.design);
            Console.WriteLine($"OwnerID      -   {owner.UserID}");
            Console.WriteLine($"Name         -   {owner.FullName}");
            Console.WriteLine($"City         -   {owner.City}");
            Console.WriteLine($"Phone        -   {owner.PhoneNumber}");
            Console.WriteLine($"Email        -   {owner.Email}");
        }
    }
    public void DeleteCustomer()
    {
        ViewAllCustomers();
        Console.WriteLine(Strings.custID);
        string customerId = Console.ReadLine();

        while (!CheckValidity.CheckNull(customerId))
        {
            Console.WriteLine(Strings.invalid);
            customerId = Console.ReadLine().Trim();
        }
        Console.WriteLine();
        Console.WriteLine();

        List<User> userList = UserController.GetUserMasterList();
        var indexOfCustomer = userList.FindIndex((customer) => customer.UserID.Equals(customerId));
        userList.RemoveAt(indexOfCustomer);
        UserController.UpdateDBUser(userList);  
        Console.WriteLine("Deleted Successfully");
        Console.WriteLine();
        Console.WriteLine();
    }
    public void DeleteOwner()
    {
        ViewAllOwners();
   
        Console.WriteLine(Strings.OwnerID);
        string ownerId = Console.ReadLine();

        while (!CheckValidity.CheckNull(ownerId))
        {
            Console.WriteLine(Strings.invalid);
            ownerId = Console.ReadLine().Trim();
        }
        Console.WriteLine();
        Console.WriteLine();

        List<User> userList = UserController.GetUserMasterList();
        var indexOfOwner = userList.FindIndex((owner) => owner.UserID.Equals(ownerId));
        userList.RemoveAt(indexOfOwner);
        UserController.UpdateDBUser(userList);
        Console.WriteLine("Deleted Successfully");
        Console.WriteLine();
        Console.WriteLine();


    }
    public void AddNewAdmin()
    {
        User newAdmin = RequestUserInput.Details(UserController.GetUserMasterList());
        newAdmin = RoleHelper.RoleSetter(newAdmin, (int)Role.Admin);
        IAuthController authController = new AuthController(DBUsers.Instance);

        bool flag = authController.Register(newAdmin);

        if (flag)
        {
            Console.WriteLine(Strings.regsuccess);
            Strings.Design();
            LoginAdminMenu(newAdmin);
        }
        else
        {
            Console.WriteLine(Strings.wrong);
            Strings.Design();
            AddNewAdmin();
        }

    }
}
