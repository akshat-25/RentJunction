using RentJunction.Models;
using System.Diagnostics.Metrics;

public class Message
{
    public static void Design()
    {
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine();
        Console.WriteLine();
    }

    public static string design = "-------------------------------------------";
    public static string reg = "Enter your Name ->";
    public static string city = "Enter Your City ->";
    public static string username = "Enter your useranme -> ";
    public static string email = "Enter Email->";
    public static string pswd = "Enter Password ->";
    public static string regsuccess = "Registered Successfully!!";
    public static string enterCred = "Enter credentials to Log in to the Application";
    public static string wrong = "Something went wrong....";
    public static string invalid = "Invalid Input. Please try again.";
    public static string adminExist = "Admin Already Exists...";
    public static string usernameExist = "Username Already Exists...";
    public static string emailExist = "Email Already Exists...";
    public static string error = "An error occured...";
    public static string logoutSucc = "Logout Successful!!!";
    public static string entCity = "Enter the city you want to search for products -";
    public static string chooseCate = "Please choose product category -";
    public static string prodIdEnt = "To rent a product, enter Product ID";
    public static string thanksRent = "Thank You for Renting!!!!";
    public static string noProdAva = "Sorry! No products available in this category";
    public static string noRented = "No Rented Product...";
    public static string entProdId = "Enter the Product ID for extention of rent period.";
    public static string validId = "Enter valid product ID";
    public static string enternewEndDate = "Enter new End Date (MM/DD/YYYY)";
    public static string prevDateSame = "Previous and new end dates are same. Please enter a higher end date";
    public static string NewDateGreater = "New end Date should be greater than previous end date";
    public static string invalidDate = "Invalid format of date!!!";
    public static string enterStartDate = "Enter Start Date (MM/DD/YYYY)";
    public static string enterValidDate = "Enter valid date";
    public static string enterEndDate = "Enter End Date (MM/DD/YYYY)";
    public static string startEndDateSame = "End date and Start Date cannot be same.";
    public static string enterUserName = "Enter your username ->";
    public static string userNameNotEmpty = "Username cannot be empty. Please try again...";
    public static string passwordNameNotEmpty = "Password cannot be empty. Please try again...";
    public static string loginFailed = "Login failed. Please check your credentials.";
    public static string logoutSuccc = "Logout Successful!!!";
    public static string custList = "The owners list is as follows ->";
    public static string custEmail = "Enter the Email of the customer you want to delete from the list -";
    public static string OwnEmail = "Enter the Email of the owner you want to delete from the list -";
    public static string cannotDeleteCust = "Cannot delete the customer as he has rented some products.";
    public static string somethingWrong = "Something went wrong";
    public static string adminSucc = "Admin added successfully..";
    public static string adminpswd = "Enter password of new admin -";
    public static string adminUsername = "Enter username of new admin -";
    public static string prodname = "Enter Product Name";
    public static string nameEmpty = "Name cannot be empty";
    public static string prodDesc = "Enter Product Description";
    public static string descEmpty = "Description cannot be empty";
    public static string prodCate = "Enter Product Category";
    public static string prodPrice = "Enter Product Price";
    public static string prodPriceError = "Price cannot be less than or equal to 0. Please try again.";
    public static string prodSucc = "Product Added Successfully!!";
    public static string noProd = "No Listed Product!!";
    public static string prodIdUpdate = "Enter Product ID to update :";
    public static string validProdId = "Enter valid product ID";
    public static string cannotDeleteProd = "You cannot update this product as it is already rented.";
    public static string disProdId    = "Product ID          - ";
    public static string disProdName  = "Product Name        - ";
    public static string disProdDesc  = "Product Description - ";
    public static string disProdPrice = "Product Price       - ";
    public static string disProdCate = "Product Category    - ";
    public static string disProdOwnName = "Owner   Name        - ";
    public static string disProdOwnNum = "Owner   Number      - ";
    public static string CateChangedSucc = "Category Changed Successfully!!";
    public static string priceChangedSucc = "Price Changed Successfully!!";
    public static string descChangedSucc = "Description Changed Successfully!!";
    public static string nameChangedSucc = "Name Changed Successfully!!";
    public static string enterNewCate = "Enter new Category";
    public static string enterNewPrice = "Enter new Price";
    public static string enterNewDesc = "Enter new description";
    public static string descError = "Description cannot be less than 10 characters" ;
    public static string nameError = "Name cannot be empty. Please try again";
    public static string custMenu = "Choose an option:\n 1. Browse Products \n 2. View Rented Products\n 3. Extend Rent Period\n 4. Logout \n";




}                                   