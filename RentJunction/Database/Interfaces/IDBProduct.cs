using RentJunction.Models;

public interface IDBProduct : IDBHandler
{
    public List<Product> GetProducts(int input, string city);
    public List<string> chooseCategory();
    public bool AddProductMaster(Product product);
    public List<Product> ProductList { get; set; }

}