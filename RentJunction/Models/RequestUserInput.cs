using RentJunction.Controller;

namespace RentJunction.Models
{
    public class RequestUserInput
    {
        public static User Details()
        {
            Console.WriteLine(Strings.reg);
            string name = CheckValidity.IsValidName();

            Console.WriteLine();

            Console.WriteLine(Strings.city);

            string address = CheckValidity.IsValidAddress();

            Console.WriteLine();

            Console.WriteLine(Strings.username);

            string username = CheckValidity.IsValidUsername();
            Console.WriteLine();
            long phoneNumber = CheckValidity.IsValidPhoneNum();

            Console.WriteLine();

            Console.WriteLine(Strings.email);

            string email = CheckValidity.IsValidEmailReg();

            Console.WriteLine();

            Console.WriteLine(Strings.pswd);

            string password = CheckValidity.IsValidPassword();

            Console.WriteLine();
            Console.WriteLine();
            while (true)
            {
                int roletaken = CheckValidity.IsValidRole();

                Console.WriteLine();

                if (roletaken == (int)Role.Customer)
                {
                    Customer cust = new Customer
                    {
                        FullName = name,
                        Address = address,
                        PhoneNumber = phoneNumber,
                        Email = email,
                        Password = password,
                        Role = (Role)roletaken,
                        Username = username
                    };

                    return cust;
                }
                else if (roletaken == (int)Role.Owner)
                {
                    Owner owner = new Owner
                    {
                        FullName = name,
                        Address = address,
                        PhoneNumber = phoneNumber,
                        Email = email,
                        Password = password,
                        Role = (Role)roletaken,
                        Username = username
                    };

                    return owner;
                }

                else
                {
                    Console.WriteLine("invalid input ! please try again...");
                }
            }
        }
    }
}