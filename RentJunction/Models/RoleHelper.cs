using RentJunction.Models;

public class RoleHelper
{
    public static User RoleSetter(User user, int roleTaken)
    {
        if (roleTaken.Equals((int)Role.Customer))
        {
            user.Role = Role.Customer;
            return user;
        }
        else if (roleTaken.Equals((int)Role.Owner))
        {
            user.Role = Role.Owner;
            return user;
        }
        else
        {
            user.Role = Role.Admin;
            return user;
        }
    }
}