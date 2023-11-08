using RentJunction.Controller;
using RentJunction.Models;

public interface IOwnerController
{
    List<Owner> GetOwnerList();
    public void UpdateOwnerList(Product product, Owner owner);
    public void UpdateDBOwner(List<Owner> list);

}