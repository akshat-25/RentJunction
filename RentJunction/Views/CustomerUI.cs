using System.Text.RegularExpressions;
using commonData;
using MenuOpt;
using RentJunction.Controller;
using RentJunction.Models;


namespace RentJunction.Views
{
    public class CustomerUI
    {
        CustomerController custCtrl = new CustomerController();
        public void LoginCustomerMenu(Customer cust)
        {
            MenuOptions.customerMenu();

            int input = Commonn.IsValidInput();
            
            Console.WriteLine();

            switch (input)
            {
                case (int)CustomerMenu.BrowseProducts:
                    BrowseProducts(cust);
                    Console.WriteLine();
                    LoginCustomerMenu(cust);
                    break;
                case (int)CustomerMenu.View_rented_products:
                    ViewRentedProducts(cust);
                    Message.Design();
                    LoginCustomerMenu(cust);
                    break;
                case (int)CustomerMenu.Extend_rent_period:
                    ExtendRentPeriod(cust);
                    Console.WriteLine();
                    LoginCustomerMenu(cust);
                    break;
                case (int)CustomerMenu.logout:
                    Console.WriteLine("Logout Successful!!!");
                    cust = null;
                    Console.WriteLine();
                    UI.StartMenu();
                    break ;
                default:
                    Message.Design();
                    Console.WriteLine();
                    LoginCustomerMenu(cust);
                    break;
            }
        }
        public void BrowseProducts(Customer cust)
        {
            Console.WriteLine("Enter the city you want to search for products -");
            string address = Commonn.IsValidAddress();
           
            Console.WriteLine("Please choose product category -");

            custCtrl.chooseCategory();

            int input = Commonn.IsValidInput();
           
            
            List<Product> res = custCtrl.getProducts(input, address);

            if (res.Count > 0)
            {
                Message.Design();
                start:
                Console.WriteLine("To rent a product, enter Product ID");
                int prodID;
                try
                {
                    bool flag = int.TryParse(Console.ReadLine() , out prodID);
                    if (!flag)
                    {
                        Message.InvalidInput();
                        goto start;
                    }
                    else {
                      bool flag2 = false; 
                     foreach(var prod in res)
                        {
                            if (prod.ProductId.Equals(prodID))
                            {
                                flag2 = true;
                                break;
                            }
                        }
                     if(!flag2) {
                            
                            goto start;
                        }
                    }
            
                }
                catch
                {
                    Message.InvalidInput();
                    goto start;
                }
                                      
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------");
                RentAProd(res, prodID, cust.rentedProducts, cust);
                Console.WriteLine("Thank You for Renting!!!!");
                LoginCustomerMenu(cust);
            }

            else
            {
                Console.WriteLine("Sorry! No products available in this category");
               
            }
        }
        public static void ViewRentedProducts(Customer cust)
        {
            try
            {
                foreach (var product in cust.rentedProducts)
                {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("Product ID          - " + product.ProductId);
                    Console.WriteLine("Product Name        - " + product.ProductName);
                    Console.WriteLine("Product Description - " + product.Description);
                    Console.WriteLine("Product Price       - " + product.Price + "per day");
                    Console.WriteLine("Product Category    - " + Enum.Parse<Category>(product.ProductCategory.ToString()));
                    Console.WriteLine("Start Date          - " + product.startDate);
                    Console.WriteLine("End Date            - " + product.endDate);
                }

            }catch{ Console.WriteLine("No Rented Product..."); }

         
        }
        public void ExtendRentPeriod(Customer cust)
        {
            ViewRentedProducts(cust);
            if(cust.rentedProducts == null) {
             
                return;
            }
            Console.WriteLine();
            start1:
            Console.WriteLine("Enter the Product ID for extention of rent period.");

            int prodID;

            try
            {

                bool flag = int.TryParse(Console.ReadLine(), out prodID);
                if (!flag)
                {
                    Console.WriteLine("Invalid Id. Please try again");
                    goto start1;
                }
                else
                {
                    bool flag2 = false;
                    foreach (var prod in cust.rentedProducts)
                    {
                        if (prod.ProductId.Equals(prodID))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        Console.WriteLine("Enter valid product ID");
                        goto start1;
                    }
                }

            }
            catch
            {
                Console.WriteLine("Enter valid product ID");
                goto start1;
            }

            List<Customer> list = custCtrl.getCustomer();
            foreach (var rentprod in cust.rentedProducts)
            {
                if (prodID.Equals(rentprod.ProductId))
                {                   
                    DateTime prevEndDate;
                    DateTime newEndDate;

                    var isValidPrevEndDate = DateTime.TryParse(rentprod.endDate, out prevEndDate);

                    start:
                    Console.WriteLine("Enter new End Date (MM/DD/YYYY)");
                    rentprod.endDate = Console.ReadLine();                
                    var isValidEndDate = DateTime.TryParse(rentprod.endDate, out newEndDate);

                    if(prevEndDate == newEndDate)
                    {
                        Console.WriteLine("Previous and new end dates are same. Please enter a higher end date");
                        Console.WriteLine();
                        goto start;
                    }
                    else if(newEndDate < prevEndDate)
                    {
                        Console.WriteLine("New end Date should be greater than previous end date");
                        Console.WriteLine();
                        goto start;
                    }
                    int differenceDays;
                    differenceDays = newEndDate.Subtract(prevEndDate).Days;

                    if (isValidEndDate && isValidPrevEndDate)
                    {

                        Console.WriteLine("Days of rent are " + differenceDays);

                    }
                    else
                    {
                        Console.WriteLine("Invalid format of date!!!");
                    }

                    Console.WriteLine("The remaining amount be be paid is : Rs. " + differenceDays * rentprod.Price);
                    custCtrl.updateDBCust(list);
                }
            }
        }
        public List<RentedProduct> RentAProd(List<Product> Masterlist, int input, List<RentedProduct> rentedlist, Controller.Customer cust)
        {
            List<Controller.Customer> listcust = custCtrl.getCustomer();
            RentedProduct rentprod = new RentedProduct();
                
                DateTime sdt;
                start:
                Console.WriteLine("Enter Start Date (MM/DD/YYYY)");
                rentprod.startDate = Console.ReadLine();
                Console.WriteLine();
                var isValidStartDate = DateTime.TryParse(rentprod.startDate, out sdt);
                if (sdt < DateTime.Today)
                {
                    Console.WriteLine("Enter valid date");
                    Console.WriteLine();
                    Console.WriteLine();
                goto start;
                }
         
            DateTime edt;
            start2:
            Console.WriteLine("Enter End Date (MM/DD/YYYY)");
            rentprod.endDate = Console.ReadLine();
            Console.WriteLine();
            var isValidEndDate = DateTime.TryParse(rentprod.endDate, out edt);

            if(sdt == edt) {
                Console.WriteLine("End date and Start Date cannot be same.");
                goto start2;
            }
            else if (edt < sdt)
            {
                Console.WriteLine("Please enter valid End Date");
                goto start2;
            }

            int days;
            days = edt.Subtract(sdt).Days;

            if (isValidEndDate && isValidStartDate)
            {

                Console.WriteLine("Total days of rent are " + days);

            }

            else
            {
                Console.WriteLine("Invalid format of date!!!");
            }
            foreach (var product in Masterlist)
            {
                if (input.Equals(product.ProductId))
                {
                    if (cust.rentedProducts == null)
                    {
                        cust.rentedProducts = new List<RentedProduct>();
                    }

                    rentprod.ProductName = product.ProductName;
                    rentprod.ProductId = product.ProductId;
                    rentprod.Price = product.Price;
                    rentprod.ProductCategory = product.ProductCategory;
                    rentprod.Description = product.Description;
                    rentprod.OwnerName = product.OwnerName;
                    rentprod.OwnerNum = product.OwnerNum;


                    cust.rentedProducts.Add(rentprod);
                    custCtrl.updateDBCust(listcust);
                    List<Product> list = custCtrl.getProductsMasterList();
                    list.Remove(product);
                    custCtrl.updateDBProds(list);
                    Console.WriteLine($"Total amount for {days} is Rs.{days * product.Price}");
                }
            }
            return rentedlist;
        }
     
    }
}
