using RentJunction.Models;

public interface IUserController 
{
    public List<User> GetUserMasterList();
    public void UpdateDBUser(List<User> list);

}