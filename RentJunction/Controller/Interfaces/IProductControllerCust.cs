using RentJunction.Models;

public interface IProductControllerCust
{
    public List<Product> GetProducts(int input, string city);
    public List<Product> GetProductsMasterList();
    public void UpdateDBProds(List<Product> list);
    public List<Product> GetRentedProductsByCustomer(User customer);
    public List<string> ChooseCategory();
}