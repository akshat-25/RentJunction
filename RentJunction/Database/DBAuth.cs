using RentJunction.Controller;

public class DBAuth
{
    private static DBAuth _instance = null;
    private static readonly object _lockObj = new object();
    private DBAuth() { }
    public static DBAuth Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new DBAuth();
                }
            }
            return _instance;
        }
    }
    public bool DbRegister(object entity)
    {
        if (entity is Customer)
        {
            Customer customer = (Customer)entity;
            if (IsExists(customer.Username))
            {
                Console.WriteLine(Message.usernameExist);
                return false;
            }

            if (DBCustomer.Instance._customerList != null)
            {
                foreach (var customerEntity in DBCustomer.Instance._customerList)
                {
                    if (customerEntity.Email == customer.Email)
                    {
                        Console.WriteLine(Message.emailExist);
                        return false;
                    }
                }
                DBCustomer.Instance._customerList.Add(customer);
                DBCustomer.Instance.UpdateDB(Message.customerPath, DBCustomer.Instance._customerList);
                return true;
            }
            DBCustomer.Instance._customerList.Add(customer);
            DBCustomer.Instance.UpdateDB(Message.customerPath, DBCustomer.Instance._customerList);
            return true;
        }
        else if(entity is Owner)
        {
            Owner owner = (Owner)entity;
            if (IsExists(owner.Username))
            {
                Console.WriteLine(Message.usernameExist);
                return false;
            }
            if (DBOwner.Instance._ownerList != null)
            {
                foreach (var ownerEntity in DBOwner.Instance._ownerList)
                {
                    if (ownerEntity.Email == owner.Email)
                    {
                        Console.WriteLine(Message.emailExist);
                        return false;
                    }
                }
                DBOwner.Instance._ownerList.Add(owner);
                DBOwner.Instance.UpdateDB(Message.ownerPath, DBOwner.Instance._ownerList);
                return true;
            }
            DBOwner.Instance._ownerList.Add(owner);
            DBOwner.Instance.UpdateDB(Message.ownerPath, DBOwner.Instance._ownerList);
            return true;
        }

        else
        {
            Admin admin = (Admin)entity;
            if (IsExists(admin.Username))
            {
                Console.WriteLine(Message.usernameExist);
                return false;
            }

            if (DBAdmin.Instance._adminList != null)
            {
                foreach (var adminEntity in DBAdmin.Instance._adminList)
                {
                    if (adminEntity.Username == admin.Username)
                    {
                        Console.WriteLine(Message.usernameExist);
                        return false;
                    }

                }
                DBAdmin.Instance._adminList.Add(admin);
                DBAdmin.Instance.UpdateDB(Message.adminPath, DBAdmin.Instance._adminList);
                return true;
            }
        }
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


}