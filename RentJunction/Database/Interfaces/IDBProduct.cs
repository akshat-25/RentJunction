using RentJunction.Models;

interface IDBProduct
{
    public List<Product> GetProducts(int input, string city);
    public List<string> chooseCategory();
    public bool AddProductMaster(Product product);
}