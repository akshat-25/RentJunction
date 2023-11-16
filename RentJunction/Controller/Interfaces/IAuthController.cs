interface IAuthController
{
    public bool Register(object entity);
    public bool IsExists(string username);
    public Object Login(string username, string password);

}