using RentJunction.Controller;
using RentJunction.Models;

namespace commonData
{
    public class Commonn
    {

        public static User Details()
        {
            Console.WriteLine(Message.reg);
            string name = CheckValidity.IsValidName();

            Console.WriteLine();

            Console.WriteLine(Message.city);

            string address = CheckValidity.IsValidAddress();

            Console.WriteLine();

            Console.WriteLine(Message.username);

            string username = CheckValidity.IsValidUsername();

            long phoneNumber = CheckValidity.IsValidPhoneNum();

            Console.WriteLine();

            Console.WriteLine(Message.email);

            string email = CheckValidity.IsValidEmailReg();

            Console.WriteLine();

            Console.WriteLine(Message.pswd);

            string password = CheckValidity.IsValidPassword();

            Console.WriteLine();
            int roletaken = CheckValidity.IsValidRole();

            Console.WriteLine();

            if(roletaken == (int)Role.Customer)
            {
                Customer cust = new Customer
                {
                    FullName = name,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Password = password,
                    role = (Role)roletaken,
                    Username = username
                };

                return cust;
            }
            else
            {
                Owner owner = new Owner
                {
                    FullName = name,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Password = password,
                    role = (Role)roletaken,
                    Username = username
                };

                return owner;
            }

        }
    }
}