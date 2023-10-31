using RentJunction.Controller;
public class AdminController
{
    public List<Customer> getCustomer()
    {
        return DbHandler.Instance._customerList;
    }
    public List<Owner> getOwners()
    {
        return DbHandler.Instance._ownerList;
    }
    public void updateDBCust(List<Customer> list)
    {
        DbHandler.Instance.UpdateDB(list);
    }
    public void UPdateDbOwner(List<Owner> list)
    {
        DbHandler.Instance.UpdateDB(list);
    }

}