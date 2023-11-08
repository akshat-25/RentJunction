interface IDBAuth
{
    public bool DbRegister(object entity);
    public Object Login(string username,string password);
    public bool IsExists(string username);
}