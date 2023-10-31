using System.Text.RegularExpressions;
using RentJunction.Models;

namespace commonData
{
    public class Commonn
    {
        public static Regex hasOnlyAlphaNumeric = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");       
        public static string IsValidUsername()
        {
            bool isValidUsername = false;
            string username = Console.ReadLine().Trim();
            while (isValidUsername == false)
            {
                if (!checkNull(username) || !hasOnlyAlphaNumeric.IsMatch(username) || username.Length < 5)
                {
                    Console.WriteLine("Username should be alphanumeric , should not be empty and have length should be greater than 5");
                    username = Console.ReadLine().Trim();
                }
                else
                {
                    isValidUsername = true;
                }
            }
            return username;
        }
        public static bool checkNull(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {

                return false;
            }
            return true;
        }
        public static bool IsValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMiniChars = new Regex(@".{8,}");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            var isValidated = hasLowerChar.IsMatch(password) && hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasSymbols.IsMatch(password) && hasMiniChars.IsMatch(password);
            return isValidated;
        }
        public static bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            bool res = Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
            return res;
        }
        public static int IsValidInput()
        {
            int input;
            while (true)
            {
                bool flag = int.TryParse(Console.ReadLine(), out input);

                if (flag)
                {
                    break;
                }
                else
                {
                    Message.InvalidInput();
                }
            }

            return input;
        }
        public static string IsValidAddress()
        {
            string address = Console.ReadLine().ToLower().Trim();
            while (true)
            {
               
                bool flag = int.TryParse(address, out int resu);

                if (!checkNull(address))
                {
                    Console.WriteLine("Address cannot be empty");
                    address = Console.ReadLine().ToLower().Trim();
                }
                else if (flag)
                {
                    Console.WriteLine("Integers are not allowed in City name -");
                    address = Console.ReadLine().ToLower().Trim();
                }
                else if (!Regex.IsMatch(address, @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("Only alphabetic characters are allowed");
                    address = Console.ReadLine().ToLower().Trim();
                }
                else if (!flag && address.Length < 3)
                {
                    Console.WriteLine("Length of city must be greater than equal to 3 characters.");
                    address = Console.ReadLine().ToLower().Trim();
                }
                else
                {
                    return address;
                    break;
                }
                
            }
            return null;
        }
        public static string IsValidName()
        {
            var isValidname = false;
            string name = Console.ReadLine();
            while (!isValidname)
            {
                if (name.Length < 3 || !checkNull(name))
                {
                    Console.WriteLine("Name should be greater than 3 characters.Please try again !");
                    name = Console.ReadLine().Trim();
                }
                else if (int.TryParse(name, out int res))
                {
                    Console.WriteLine("Integers are not allowed in name.");
                    name = Console.ReadLine().Trim();
                }
                else if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("Only alphabetic characters are allowed");
                    name = Console.ReadLine().Trim();
                }
                else
                {
                    isValidname = true;
                    return name;
                }
            }
            return name;
           
        }     
        public static long IsValidPhoneNum()
        {
        start:
            Console.WriteLine("Enter Phone Number -> +91 ");
            long phoneNumber;
            try
            {
                phoneNumber = Convert.ToInt64(Console.ReadLine());
                string phNo = phoneNumber.ToString();
                if (phNo.Length != 10)
                {
                    Console.WriteLine("Phone number should be of 10 digits and alphabetic chara ters are not allowed.");
                    goto start;
                }
                return phoneNumber;
            }
            catch
            {
                Console.WriteLine("Phone number should be of 10 digits");
                goto start;
            }
        }   
        public static string IsValidEmail()
        {
            string email = Console.ReadLine();
            while (!IsValidEmail(email) || !checkNull(email))
            {
                Console.WriteLine("Please enter a valid email address");
                email = Console.ReadLine().Trim();
            }
            return email;
        }
        public static string IsValidPassword()
        {
            string password = Console.ReadLine().Trim();
            while (IsValidPassword(password) == false || !checkNull(password))
            {
                Console.WriteLine("Password should not be less 8 characters and should contain a \n UpperCase , Special Character and at least one Number");
                password = Console.ReadLine().Trim();
            }
            return password;
        }
        public static int IsValidRole()
        {
        start1:
            Console.WriteLine("Enter Role type (1. Customer    2. Owner) ->");
            int roletaken;
            try
            {
                roletaken = Convert.ToInt32(Console.ReadLine());
                if (roletaken != (int)Role.Customer && roletaken != (int)Role.Owner)
                {
                    Console.WriteLine("Please choose a valid role");
                    goto start1;
                }
                return roletaken;
            }
            catch
            {
                Console.WriteLine("Please choose a valid role");
                goto start1;
            }
        }
        public static User Details()
        {
            Console.WriteLine("Enter your Name ->");
            string name = IsValidName();

            Console.WriteLine(); 

            Console.WriteLine("Enter Your City ->");
            string address = IsValidAddress();

            Console.WriteLine();
 
            Console.WriteLine("Enter your useranme -> ");
            string username = IsValidUsername();

            long phoneNumber = IsValidPhoneNum();

            Console.WriteLine();

            Console.WriteLine("Enter Email->");
            string email = Console.ReadLine().Trim();
            IsValidEmail(email);
            Console.WriteLine();

            Console.WriteLine("Enter Password ->");
            string password = IsValidPassword();

            Console.WriteLine();
            int roletaken = IsValidRole();
            
            Console.WriteLine();

            User user = new User()
            {
                FullName = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                Password = password,
                role = (Role)roletaken,
                Username = username
            };

            return user;

        }
    }
}