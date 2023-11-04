using RentJunction.Controller;

namespace RentJunction.Models
{
    public class CustomerController
    {
        public void chooseCategory()
        {
            List<string> categories = DBProduct.Instance.chooseCategory();
            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }

        }
        public List<Product> getProducts(int input, string city)
        {

            foreach (var product in DBProduct.Instance.GetProducts(input, city))
            {
                Console.WriteLine(Message.design);
                Console.WriteLine(Message.disProdId      + product.ProductId);
                Console.WriteLine(Message.disProdName    + product.ProductName);
                Console.WriteLine(Message.disProdDesc    + product.Description);
                Console.WriteLine(Message.disProdPrice   + "Rs." + product.Price + " per day");
                Console.WriteLine(Message.disProdCate    + Enum.Parse<Category>(product.ProductCategory.ToString()));
                Console.WriteLine(Message.disProdOwnName + product.OwnerName);
                Console.WriteLine(Message.disProdOwnNum  + product.OwnerNum);
            }
            return DBProduct.Instance.GetProducts(input, city);
        }
        public List<Customer> getCustomer()
        {
            return DBCustomer.Instance._customerList;
        }
        public void updateDBCust(List<Customer> list)
        {
            DBCustomer.Instance.UpdateDB(Message.customerPath, list);
        }
        public void UpdateCustList(Product product, Owner owner)
        {
            foreach (var productUpdate in owner.ListedProducts)
            {
                if (productUpdate.ProductId.Equals(product.ProductId))
                {
                    productUpdate.ProductName = product.ProductName;
                    productUpdate.Description = product.Description;
                    productUpdate.Price = product.Price;
                    productUpdate.ProductCategory = product.ProductCategory;
                }
            }
        }

    }
}
