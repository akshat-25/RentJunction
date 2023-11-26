using RentJunction.Models;

namespace RentJunction.Helper
{
    public class RequestUserInput
    {
        public static string GenerateUniqueId()
        {
            Guid guid = Guid.NewGuid();
            string randomComponent = guid.ToString("N").Substring(0, 8);

            string uniqueId = randomComponent;

            return uniqueId;
        }
        public static User Details()
        {
            Console.WriteLine(Strings.reg);
            string name = Console.ReadLine().Trim();
            bool isValidName;
            while (true)
            {
                isValidName = CheckValidity.IsValidName(name);
                if (!isValidName)
                {
                    name = Console.ReadLine().Trim();
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();

            Console.WriteLine(Strings.city);
            string city = Console.ReadLine().ToLower();
            bool isValidCity;

            while (true)
            {
                isValidCity = CheckValidity.IsValidAddress(city);
                if (isValidCity)
                {
                    break;
                }
                else
                {
                    city = Console.ReadLine();
                }
            }
            Console.WriteLine();

            Console.WriteLine(Strings.username);

            string username = Console.ReadLine();
            bool isValidUsername;

            while (true)
            {
                isValidUsername = CheckValidity.IsValidUsername(username);

                if (isValidUsername)
                {
                    break;
                }
                else
                {
                    username = Console.ReadLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine(Strings.enterPhNo);
            string phoneNumber = Console.ReadLine();
            bool isValidPhoneNumber;
            while (true)
            {
                isValidPhoneNumber = CheckValidity.IsValidPhoneNum(phoneNumber);
                if (isValidPhoneNumber)
                {
                    break;
                }
                else
                {
                    phoneNumber = Console.ReadLine();
                }
            }

            Console.WriteLine();

            Console.WriteLine(Strings.email);

            string email = Console.ReadLine();
            bool isValidEmail;

            while (true)
            {
                isValidEmail = CheckValidity.IsValidEmail(email);
                if (isValidEmail)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(Strings.validEmail);
                    email = Console.ReadLine();
                }
            }
            Console.WriteLine();

            Console.WriteLine(Strings.pswd);

            string password = CheckValidity.HideCharacter();

            bool isValidPassword;

            while (true)
            {
                isValidPassword = CheckValidity.IsValidPassword(password);
                if (isValidPassword)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(Strings.passwordNameNotEmpty);
                    password = CheckValidity.HideCharacter();
                }
            }
            Console.WriteLine();
            Console.WriteLine();

            string Id = GenerateUniqueId();
            //while (true)
            //{
            //    if (userList != null && userList.Any((user) => user.UserID == Id))
            //    {
            //        Id = GenerateUniqueId();
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            var user = new User
            {
                UserID = Id,
                FullName = name,
                City = city,
                Email = email,
                PhoneNumber = Convert.ToInt64(phoneNumber),
                Password = password,
                Username = username
            };

            return user;

        }
    }
}