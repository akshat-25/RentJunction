
using RentJunction.Models;

public interface IProductControllerOwner
{
    public List<Product> GetProductsMasterList();
    public bool AddProductMaster(Product product);
    public void UpdateDBProds(List<Product> list);
    public List<Product> GetListedProductsByOwner(User owner);
}