using RentJunction.Models;
using System.Text.RegularExpressions;

public class CheckValidity
{
    static string hasOnlyAlphaNumeric;
    static string hasNumber;
    static string hasUpperChar;
    static string hasLowerChar;
    static string hasMiniChars;
    static string hasSymbols;
    static string hasEmail;
    static CheckValidity()
    {
        hasOnlyAlphaNumeric = @"^[a-zA-Z][a-zA-Z0-9]*$";
                  hasNumber = @"[0-9]+";
               hasUpperChar = @"[A-Z]+";
               hasLowerChar = @"[a-z]+";
               hasMiniChars = @".{8,}";
                 hasSymbols = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
                   hasEmail = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
    }
    public static string IsValidUsername()
    {
        bool isValidUsername = false;
        string username = Console.ReadLine().Trim();
        while (isValidUsername == false)
        {
            if (!checkNull(username) || !Regex.IsMatch(username,hasOnlyAlphaNumeric) || username.Length < 5)
            {
                Console.WriteLine("Username should be alphanumeric ," +
                    " should not be empty and have length should be greater than 5");
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
    public static string HideCharacter()
    {
        var pass = string.Empty;
        ConsoleKey key; do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;
            if (key == ConsoleKey.Backspace && pass.Length > 0)
            {
                Console.Write("\b \b");
                pass = pass[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

        return pass;
    }
    public static bool IsValidPasswordReg(string password)
    {
        var isValidated = Regex.IsMatch(password, hasLowerChar) && Regex.IsMatch(password, hasNumber) && Regex.IsMatch(password, hasUpperChar) && Regex.IsMatch(password, hasSymbols) && Regex.IsMatch(password, hasMiniChars);
        return isValidated;
    }
    public static bool IsValidEmail(string email)
    {
        bool res = Regex.IsMatch(email, hasEmail, RegexOptions.IgnoreCase) && checkNull(email);
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
                Console.WriteLine(Message.invalid);
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
    public static string IsValidEmailReg()
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
        string password = HideCharacter();

        
        while (IsValidPasswordReg(password) == false || !checkNull(password))
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


}