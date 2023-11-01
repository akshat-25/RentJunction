using RentJunction.Models;
interface DatabaseLayer
{
    public bool DbRegister(object entity);
    public bool DbRegisterAdmin(Admin admin);
    public void UpdateDB<T>(List<T> list);
    public Object Login(string username, string password);
    public List<Product> GetProducts(int input, string city);
    public List<string> chooseCategory();
    public bool AddProductMaster(Product product);
}