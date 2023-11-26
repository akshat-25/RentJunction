using System.Net;
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
    static string hasName;
    static CheckValidity()
    {
        hasOnlyAlphaNumeric = @"^[a-zA-Z][a-zA-Z0-9]*$";
        hasNumber = @"[0-9]+";
        hasUpperChar = @"[A-Z]+";
        hasLowerChar = @"[a-z]+";
        hasName = @"^[a-zA-Z]+$";
        hasMiniChars = @".{8,}";
        hasSymbols = @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]";
        hasEmail = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
    }
    public static bool CheckNull(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }
        return true;
    }
    public static bool IsValidUsername(string username)
    {
        username = username.Trim();
       
            if (!CheckNull(username) || !Regex.IsMatch(username, hasOnlyAlphaNumeric) || username.Length < 5)
            {
                Console.WriteLine(Strings.userNameErr);
                return false;
            }
            else
            {
                return true;
            }
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
    public static bool IsValidEmail(string email)
    {
        if(!CheckNull(email))
        {
            Console.WriteLine(Strings.validEmail);
            return false;
        }

        if (Regex.IsMatch(email, hasEmail, RegexOptions.IgnoreCase) && CheckNull(email))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    public static bool IsValidInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine(Strings.invalid);
            return false;
        }
        int isValid;

        bool flag = int.TryParse(input, out isValid);

        if (flag)
        {
            return true;
        }
        else
        {
            Console.WriteLine(Strings.invalid);
            return false;
        }

    }
    public static bool IsValidAddress(string address)
    {
        address = address.Trim();
        int resu;
        bool flag = int.TryParse(address, out resu);
      
        if (!CheckNull(address))
        {
            Console.WriteLine(Strings.addressEmpty);
            return false;
        }
        else if (flag)
        {
            Console.WriteLine(Strings.cityIntError);
            return false;
        }
        else if (!Regex.IsMatch(address, @"^[a-zA-Z]+$"))
        {
            Console.WriteLine(Strings.onlyAlphabetsallowed);
            return false;
        }
        else if (!flag && address.Length < 3)
        {
            Console.WriteLine(Strings.cityLength);
            return false;
        }
        else
        {
            return true;
        }

    }
    public static bool IsValidName(string name)
    {

        if (name.Length < 3 || !CheckNull(name))
        {
            Console.WriteLine(Strings.nameLength);
            return false;
        }
        else if (int.TryParse(name, out int res))
        {
            Console.WriteLine(Strings.nameIntError);
            return false;
        }
        else if (!Regex.IsMatch(name,hasName))
        {
            Console.WriteLine(Strings.onlyAlphabetsallowed);
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool IsValidPhoneNum(string phoneNumber)
    {
        if (!CheckNull(phoneNumber))
        {
            Console.WriteLine(Strings.PhNoLenError);
            return false;
        }
        long res;
        bool flag = long.TryParse(phoneNumber, out res);
        if (phoneNumber.Length != 10 || !flag)
        {
            Console.WriteLine(Strings.PhNoLenError);
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool IsValidPassword(string password)
    {

        if(string.IsNullOrEmpty(password))
        {
            return false;
        }
        var isValidated = Regex.IsMatch(password, hasLowerChar) && Regex.IsMatch(password, hasNumber) &&
        Regex.IsMatch(password, hasUpperChar) && Regex.IsMatch(password, hasSymbols) && Regex.IsMatch(password, hasMiniChars);

        if(isValidated) {
            return true;
        }

        return false;

    }
    public static bool IsValidRole(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        int isValid;

        bool flag = int.TryParse(input, out isValid);

        if (flag)
        {
            if (isValid != (int)Role.Owner && isValid != (int)Role.Customer)
            {
                return false;
            }

            return true;
        }

        return false;
    }
}
