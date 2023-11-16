using RentJunction.Controller;
using RentJunction.Models;

public class ProductController : IProductControllerCust , IProductControllerOwner
{
    public List<Product> GetProducts(int input, string city)
    {

        foreach (var product in DBProduct.Instance.GetProducts(input, city))
        {
            Console.WriteLine(Strings.design);
            Console.WriteLine(Strings.disProdId + product.ProductId);
            Console.WriteLine(Strings.disProdName + product.ProductName);
            Console.WriteLine(Strings.disProdDesc + product.Description);
            Console.WriteLine(Strings.disProdPrice + "Rs." + product.Price + " per day");
            Console.WriteLine(Strings.disProdCate + Enum.Parse<Category>(product.ProductCategory.ToString()));
            Console.WriteLine(Strings.disProdOwnName + product.OwnerName);
            Console.WriteLine(Strings.disProdOwnNum + product.OwnerNum);
        }
        return DBProduct.Instance.GetProducts(input, city);
    }
    public List<Product> GetProductsMasterList()
    {
        return DBProduct.Instance._productList;
    }
    public bool AddProductMaster(Product product)
    {
        return DBProduct.Instance.AddProductMaster(product);
    }
    public void UpdateDBProds(List<Product> list)
    {
        DBProduct.Instance.UpdateDB(Strings.productsPath, list);
    }
    public void ViewRentedProd(Customer cust)
    {
        foreach (var product in cust.rentedProducts)
        {
            Console.WriteLine(Strings.design);
            Console.WriteLine(Strings.disProdId    + product.ProductId);
            Console.WriteLine(Strings.disProdName  + product.ProductName);
            Console.WriteLine(Strings.disProdDesc  + product.Description);
            Console.WriteLine(Strings.disProdPrice + product.Price + " per day");
            Console.WriteLine(Strings.disProdCate  + Enum.Parse<Category>(product.ProductCategory.ToString()));
            Console.WriteLine(Strings.startDate + product.StartDate);
            Console.WriteLine(Strings.endDate + product.EndDate);
        }
    }



}