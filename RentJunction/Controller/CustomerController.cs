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
