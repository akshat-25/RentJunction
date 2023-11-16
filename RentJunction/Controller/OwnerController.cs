using RentJunction.Controller;

namespace RentJunction.Models
{
    public class OwnerController : IOwnerController
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
        public void UpdateDBOwner(List<Owner> list)
        {
            DBOwner.Instance.UpdateDB(Strings.ownerPath, list);
        }
        public List<Owner> GetOwnerList()
        {
            return DBOwner.Instance._ownerList;
        }
    }
}
