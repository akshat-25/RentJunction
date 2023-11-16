using RentJunction.Controller;
public class AuthController : IAuthController
{
    public bool Register(object entity)
    {
        if (entity is Customer)
        {
            Customer customer = (Customer)entity;
            return RegisterCustomer(customer);
        }
        else if (entity is Owner)
        {
            Owner owner = (Owner)entity;
            return RegisterOwner(owner);    
        }
        else
        {
            Admin admin = (Admin)entity;
            return RegisterAdmin(admin);
        }
    }

    private bool RegisterCustomer(Customer customer)
    {
       
        if (IsExists(customer.Username))
        {
            return false;
        }

        if (DBCustomer.Instance._customerList != null)
        {
            foreach (var customerEntity in DBCustomer.Instance._customerList)
            {
                if (customerEntity.Email == customer.Email)
                {
                    Console.WriteLine(Strings.emailExist);
                    return false;
                }
            }
            DBCustomer.Instance._customerList.Add(customer);
            DBCustomer.Instance.UpdateDB(Strings.customerPath, DBCustomer.Instance._customerList);
            return true;
        }
        DBCustomer.Instance._customerList.Add(customer);
        DBCustomer.Instance.UpdateDB(Strings.customerPath, DBCustomer.Instance._customerList);
        return true;
    }
    private bool RegisterOwner(Owner owner)
    {
      
        if (IsExists(owner.Username))
        {
            return false;
        }

        if (DBOwner.Instance._ownerList != null)
        {
            foreach (var ownerEntity in DBOwner.Instance._ownerList)
            {
                if (ownerEntity.Email == owner.Email)
                {
                    Console.WriteLine(Strings.emailExist);
                    return false;
                }
            }
            DBOwner.Instance._ownerList.Add(owner);
            DBOwner.Instance.UpdateDB(Strings.ownerPath, DBOwner.Instance._ownerList);
            return true;
        }
        DBOwner.Instance._ownerList.Add(owner);
        DBOwner.Instance.UpdateDB(Strings.ownerPath, DBOwner.Instance._ownerList);
        return true;
    }
    private bool RegisterAdmin(Admin admin)
    {
       
        if (IsExists(admin.Username))
        {
            Console.WriteLine(Strings.usernameExist);
            return false;
        }

        if (DBAdmin.Instance._adminList != null)
        {
            foreach (var adminEntity in DBAdmin.Instance._adminList)
            {
                if (adminEntity.Username == admin.Username)
                {
                    Console.WriteLine(Strings.usernameExist);
                    return false;
                }
            }
            DBAdmin.Instance._adminList.Add(admin);
            DBAdmin.Instance.UpdateDB(Strings.adminPath, DBAdmin.Instance._adminList);
            return true;
        }
        return false;
    }
    public bool IsExists(string username)
    {
        if (DBCustomer.Instance._customerList.FindIndex((ent) => ent.Username == username) != -1)
            return true;
        else if (DBOwner.Instance._ownerList.FindIndex((ent) => ent.Username == username) != -1)
            return true;
        else if (DBAdmin.Instance._adminList.FindIndex((ent) => ent.Username == username) != -1)
            return true;
        else
            return false;
    }
    public Object Login(string username, string password)
    { 
        if (DBCustomer.Instance._customerList.FindIndex((cust) => cust.Username == username && cust.Password == password) != -1)
            return DBCustomer.Instance._customerList[DBCustomer.Instance._customerList.FindIndex((cust) => cust.Username == username && cust.Password == password)];

        else if (DBOwner.Instance._ownerList.FindIndex((own) => own.Username == username && own.Password == password) != -1)
            return DBOwner.Instance._ownerList[DBOwner.Instance._ownerList.FindIndex((own) => own.Username == username && own.Password == password)];

        else if (DBAdmin.Instance._adminList.FindIndex((adm) => adm.Username == username && adm.Password == password) != -1)
            return DBAdmin.Instance._adminList[DBAdmin.Instance._adminList.FindIndex((adm) => adm.Username == username && adm.Password == password)];

        else
            return null;
    }
}
