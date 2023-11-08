using RentJunction.Controller;
using RentJunction.Models;

public interface IProductControllerCust
{
    public List<Product> GetProducts(int input, string city);
    public void ViewRentedProd(Customer cust);
    public List<Product> GetProductsMasterList();
    public void UpdateDBProds(List<Product> list);
}