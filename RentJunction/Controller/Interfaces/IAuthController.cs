using RentJunction.Models;

interface IAuthController
{
    public bool Register<T>(T user) where T : User;
    public User Login(string username, string password);

}