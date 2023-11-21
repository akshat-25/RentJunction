using RentJunction.Models;

public interface IAuthController
{
    public bool Register(User user);
    public bool GetUserUI(string username, string password);

}