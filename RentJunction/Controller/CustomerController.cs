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
                Console.WriteLine(Message.design);
                Console.WriteLine(Message.disProdId    + product.ProductId);
                Console.WriteLine(Message.disProdName  + product.ProductName);
                Console.WriteLine(Message.disProdDesc  + product.Description);
                Console.WriteLine(Message.disProdPrice + "Rs." + product.Price + " per day");
                Console.WriteLine(Message.disProdCate  + Enum.Parse<Category>(product.ProductCategory.ToString()));
                Console.WriteLine(Message.disProdOwnName + product.OwnerName);
                Console.WriteLine(Message.disProdOwnNum + product.OwnerNum);
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
