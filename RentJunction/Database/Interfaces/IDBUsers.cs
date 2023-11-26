using RentJunction.Models;

public interface IDBUsers : IDBHandler
{
    public List<User> UserList { get; set; }

}