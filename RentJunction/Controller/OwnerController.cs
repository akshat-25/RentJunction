using RentJunction.Controller;

namespace RentJunction.Models
{
    public class OwnerController
    {
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
        public List<Customer> GetCustomerList()
        {
            return DbHandler.Instance._customerList;
        }
        public bool AddProductMaster(Product product)
        {
            return DbHandler.Instance.AddProductMaster(product);
        }
        public List<Product> GetProductList()
        {
            return DbHandler.Instance._ProductList;
        }

        public void updateDBProducts(List<Product> list)
        {
            DbHandler.Instance.UpdateDB(list);
        }

        public void updateDBOwner(List<Owner> list)
        {
            DbHandler.Instance.UpdateDB(list);
        }

        public List<Owner> getOwnerList()
        {
            return DbHandler.Instance._ownerList;
        }
    }
}
