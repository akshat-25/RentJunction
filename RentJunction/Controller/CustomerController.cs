using MenuOpt;
using RentJunction.Controller;


namespace RentJunction.Models
{
    public class CustomerController
    {
        public void chooseCategory()
        {
            List<string> categories = DbHandler.Instance.chooseCategory();
            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }

        }
        public List<Product> getProducts(int input, string city)
        {

            foreach (var product in DbHandler.Instance.GetProducts(input, city))
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Product ID          - " + product.ProductId);
                Console.WriteLine("Product Name        - " + product.ProductName);
                Console.WriteLine("Product Description - " + product.Description);
                Console.WriteLine("Product Price       - " + "Rs." + product.Price + " per day");
                Console.WriteLine("Product Category    - " + Enum.Parse<Category>(product.ProductCategory.ToString()));
                Console.WriteLine("Owner   Name        - " + product.OwnerName);
                Console.WriteLine("Owner   Number      - " + product.OwnerNum);
            }
            return DbHandler.Instance.GetProducts(input, city);
        }
        public List<Product> getProductsMasterList()
        {
            return DbHandler.Instance._ProductList;
        }
        public List<Customer> getCustomer()
        {
            return DbHandler.Instance._customerList;
        }
        public void updateDBCust(List<Customer> list)
        {
            DbHandler.Instance.UpdateDB(list);
        }
        public void updateDBProds(List<Product> list)
        {
            DbHandler.Instance.UpdateDB(list);
        }
    }
}
