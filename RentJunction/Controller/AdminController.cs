using RentJunction.Controller;
public class AdminController
{
    
    //public Admin 
    public List<Customer> getCustomer()
    {
        return DBCustomer.Instance._customerList;
    }
    public List<Owner> getOwners()
    {
        return DBOwner.Instance._ownerList;
    }
    public void updateDBCust(List<Customer> list)
    {
        DBCustomer.Instance.UpdateDB(Message.customerPath,list);
    }
    public void UPdateDbOwner(List<Owner> list)
    {
        DBOwner.Instance.UpdateDB(Message.ownerPath, list);
    }

}