using RentJunction.Controller;

namespace RentJunction.Models
{
    public class OwnerController
    {
        public void UpdateOwnerList(Product product, Owner owner)
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
        public List<Customer> GetCustomerList()
        {
            return DBCustomer.Instance._customerList;
        }
        public bool AddProductMaster(Product product)
        {
            return DBProduct.Instance.AddProductMaster(product);
        }
        public List<Product> GetProductList()
        {
            return DBProduct.Instance._productList;
        }

        public void updateDBProducts(List<Product> list)
        {
            DBProduct.Instance.UpdateDB(Message.productsPath, list);
        }

        public void updateDBOwner(List<Owner> list)
        {
            DBOwner.Instance.UpdateDB(Message.ownerPath, list);
        }

        public List<Owner> getOwnerList()
        {
            return DBOwner.Instance._ownerList;
        }
    }
}
