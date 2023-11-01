using Newtonsoft.Json;
using RentJunction.Controller;
using RentJunction.Models;
public sealed class DbHandler : DatabaseLayer
{
      public List<Customer> _customerList;
      public List<Owner> _ownerList;
      public List<Product> _ProductList;
      public List<Admin> _adminList;
      public List<string> _productCategoryList = new List<string>();
   
      string _customerPath = @"C:\Users\aparakh\source\repos\RentJunction\RentJunction\Data\CustomerMasterList.json";
      string _ownerPath = @"C:\Users\aparakh\source\repos\RentJunction\RentJunction\Data\OwnerMasterList.json";
      string _productsPath = @"C:\Users\aparakh\source\repos\RentJunction\RentJunction\Data\ProductMasterList.json";
      string _adminPath = @"C:\Users\aparakh\source\repos\RentJunction\RentJunction\Data\AdminList.json";
    
    private static DbHandler _instance = null;
    private static readonly object _lockObj = new object();
    private DbHandler()
    {
        _customerList = new List<Customer>();
        _ownerList = new List<Owner>();
        _ProductList = new List<Product>();
        _adminList = new List<Admin>();
        try
        {
            _customerList = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(_customerPath));
            _ownerList = JsonConvert.DeserializeObject<List<Owner>>(File.ReadAllText(_ownerPath));
            _ProductList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(_productsPath));
            _adminList = JsonConvert.DeserializeObject<List<Admin>>(File.ReadAllText(_adminPath));
        }
        catch
        {
            Console.WriteLine(Message.error);
            UI.StartMenu();
        }
    }
    public static DbHandler Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new DbHandler();
                }
            }            
            return _instance;
        }
    }  
    public bool IsExists(string username)
    {
        if (_customerList.FindIndex((ent) => ent.Username == username) != -1)
            return true;
        else if (_ownerList.FindIndex((ent) => ent.Username == username) != -1)
            return true;
        else if(_adminList.FindIndex((ent) => ent.Username == username) != -1)
            return true;
        else
            return false;
    }
    public  bool DbRegister(object entity)
    {
        if (entity is Customer)
        {
            Customer customer = (Customer)entity;
            if (IsExists(customer.Username)) {
                Console.WriteLine(Message.usernameExist);
                return false;
            }

            if (_customerList != null)
                {
                    foreach (var customerEntity in _customerList)
                    {
                        if (customerEntity.Email == customer.Email)
                        {
                        Console.WriteLine(Message.emailExist);
                        return false;
                        }
                    }
                    _customerList.Add(customer);
                    UpdateDB(_customerList);
                    return true;
                }
                _customerList.Add(customer);
                UpdateDB(_customerList);
                return true;
        }
        else
        {
            Owner owner = (Owner)entity;
            if (IsExists(owner.Username))
            {
                Console.WriteLine(Message.usernameExist);
                return false;
            }
            if (_ownerList != null)
            {
                foreach (var ownerEntity in _ownerList)
                {
                    if (ownerEntity.Email == owner.Email)
                    {
                        Console.WriteLine(Message.emailExist);
                        return false;
                    }
                }
                _ownerList.Add(owner);
                UpdateDB(_ownerList);
                return true;
            }
            _ownerList.Add(owner);
            UpdateDB(_ownerList);
            return true;
        }
    } 
    public  bool DbRegisterAdmin(Admin admin)
    {
        if (IsExists(admin.Username)) {
            Console.WriteLine(Message.usernameExist);
            return false; }

        if (_adminList != null)
        {
            foreach (var adminEntity in _adminList)
            {
                if (adminEntity.Username == admin.Username)
                {
                    Console.WriteLine(Message.usernameExist);
                    return false;
                }
                
            }
            _adminList.Add(admin);
            UpdateDB(_adminList);
            return true;
        }

        _adminList.Add(admin);
        UpdateDB(_adminList);
        return true;
    }
    public void UpdateDB<T>(List<T> list)
    {
      if(list is List<Customer>)
      {
            var customerJson = JsonConvert.SerializeObject(list);
            File.WriteAllText(_customerPath, customerJson);           
      }
      else if(list is List<Owner>)
      {
            var ownerJson = JsonConvert.SerializeObject(list);
            File.WriteAllText(_ownerPath, ownerJson);
      }
      else if(list is List<Admin>)
      {
            var adminJson = JsonConvert.SerializeObject(list);
            File.WriteAllText(_adminPath, adminJson);
      }
      else
      {
            var prodJson = JsonConvert.SerializeObject(list);
            File.WriteAllText(_productsPath, prodJson);
      } 
    }
    public Object Login(string username, string password)
    {
        if (_customerList.FindIndex((cust) => cust.Username == username && cust.Password == password) != -1)
            return _customerList[_customerList.FindIndex((cust) => cust.Username == username && cust.Password == password)];

        else if (_ownerList.FindIndex((own) => own.Username == username && own.Password == password) != -1)
            return _ownerList[_ownerList.FindIndex((own) => own.Username == username && own.Password == password)];

        else if (_adminList.FindIndex((adm) => adm.Username == username && adm.password == password) != -1)
            return _adminList[_adminList.FindIndex((adm) => adm.Username == username && adm.password == password)];

        else
            return null;
    }
    public  List<Product> GetProducts(int input, string city)
    {
        List<Product> filteredProd = _ProductList.FindAll((product) => (product.ProductCategory == input) && (product.City == city));
        return filteredProd;
    }
    public  List<string> chooseCategory()
    {
        if(_productCategoryList.Count == 0) {

            _productCategoryList.Add("1.  Property");
            _productCategoryList.Add("2.  Electronics");
            _productCategoryList.Add("3.  Computer_Accessories");
            _productCategoryList.Add("4.  Audio_Visual");
            _productCategoryList.Add("5.  Security_Systems");
            _productCategoryList.Add("6.  Clothes_Jewellery");
            _productCategoryList.Add("7.  Generator ");
            _productCategoryList.Add("8.  Media_Entertainment_Equipment");
            _productCategoryList.Add("9.  Vehicle");
            _productCategoryList.Add("10. Health_Supplements");
            _productCategoryList.Add("11. Furniture");
            _productCategoryList.Add("12. Miscellaneous");
            
        }

        return _productCategoryList;
       
        
    }
    public  bool AddProductMaster(Product product)
    {
        if(_ProductList != null )
        {
        _ProductList.Add(product);
        }
        else
        {
            _ProductList.Add(product);
        }
        UpdateDB(_ProductList);
        return true;
    }
    
}