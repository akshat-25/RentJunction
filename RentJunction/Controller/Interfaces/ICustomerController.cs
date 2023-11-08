using RentJunction.Controller;
using RentJunction.Models;

public interface ICustomerController
{
    List<Customer> GetCustomer();
    void UpdateCustList(Product product, Owner owner);

    public void UpdateDBCust(List<Customer> list);
}