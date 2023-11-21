using RentJunction.Models;

public class ProductController : IProductControllerCust , IProductControllerOwner
{
    public IDBProduct DBProduct { get; set; }
    public ProductController(IDBProduct DbProduct)
    {
        DBProduct = DbProduct;
    }
    public List<Product> GetProducts(int input, string city)
    {
        return DBProduct.GetProducts(input, city);
    }
    public List<Product> GetProductsMasterList()
    {
        return DBProduct.ProductList;
    }
    public List<string> ChooseCategory()
    {
        return DBProduct.chooseCategory();
        
    }
    public bool AddProductMaster(Product product)
    {
        return DBProduct.AddProductMaster(product);
    }
    public void UpdateDBProds(List<Product> list)
    {
        DBProduct.UpdateDB(Strings.productsPath, list);
    }
    public List<Product> GetListedProductsByOwner(User owner)
    {
        List<Product> products = GetProductsMasterList();
        return products.FindAll((product) => product.OwnerID == owner.UserID.ToString());
    }
    public List<Product> GetRentedProductsByCustomer(User customer)
    {
        List<Product> products = GetProductsMasterList();
        return products.FindAll((product) => product.CustomerID == customer.UserID.ToString());
    }

}