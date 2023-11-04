using RentJunction.Controller;
using RentJunction.Models;

public class ProductController
{
    public List<Product> getProducts(int input, string city)
    {

        foreach (var product in DBProduct.Instance.GetProducts(input, city))
        {
            Console.WriteLine(Message.design);
            Console.WriteLine(Message.disProdId + product.ProductId);
            Console.WriteLine(Message.disProdName + product.ProductName);
            Console.WriteLine(Message.disProdDesc + product.Description);
            Console.WriteLine(Message.disProdPrice + "Rs." + product.Price + " per day");
            Console.WriteLine(Message.disProdCate + Enum.Parse<Category>(product.ProductCategory.ToString()));
            Console.WriteLine(Message.disProdOwnName + product.OwnerName);
            Console.WriteLine(Message.disProdOwnNum + product.OwnerNum);
        }
        return DBProduct.Instance.GetProducts(input, city);
    }
    public List<Product> getProductsMasterList()
    {
        return DBProduct.Instance._productList;
    }
    public void updateDBProds(List<Product> list)
    {
        DBProduct.Instance.UpdateDB(Message.productsPath, list);
    }
    public void viewRentedProd(Customer cust)
    {
        foreach (var product in cust.rentedProducts)
        {
            Console.WriteLine(Message.design);
            Console.WriteLine(Message.disProdId    + product.ProductId);
            Console.WriteLine(Message.disProdName  + product.ProductName);
            Console.WriteLine(Message.disProdDesc  + product.Description);
            Console.WriteLine(Message.disProdPrice + product.Price + "per day");
            Console.WriteLine(Message.disProdCate  + Enum.Parse<Category>(product.ProductCategory.ToString()));
            Console.WriteLine(Message.startDate + product.startDate);
            Console.WriteLine(Message.endDate + product.endDate);
        }
    }

}