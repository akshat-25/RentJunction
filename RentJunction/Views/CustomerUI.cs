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
        ProductController  prodCtrl = new ProductController();
        public void LoginCustomerMenu(Customer cust)
        {
            Console.WriteLine(Message.custMenu);
            Console.WriteLine(Message.design);

            CustomerMenu input = (CustomerMenu)CheckValidity.IsValidInput();
            
            Console.WriteLine();

            switch (input)
            {
                case CustomerMenu.BrowseProducts:
                    BrowseProducts(cust);
                    Console.WriteLine();
                    LoginCustomerMenu(cust);
                    break;
                case CustomerMenu.View_rented_products:
                    ViewRentedProducts(cust);
                    Message.Design();
                    LoginCustomerMenu(cust);
                    break;
                case CustomerMenu.Extend_rent_period:
                    ExtendRentPeriod(cust);
                    Console.WriteLine();
                    LoginCustomerMenu(cust);
                    break;
                case CustomerMenu.logout:
                    Console.WriteLine(Message.logoutSucc);
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
            Console.WriteLine(Message.entCity);
            string address = CheckValidity.IsValidAddress();
           
            Console.WriteLine(Message.chooseCate);

            custCtrl.chooseCategory();

            int input = CheckValidity.IsValidInput();
            
            List<Product> res = prodCtrl.getProducts(input, address);

            if (res.Count > 0)
            {
                Message.Design();
                start:
                Console.WriteLine(Message.prodIdEnt);
                int prodID;
                try
                {
                    bool flag = int.TryParse(Console.ReadLine() , out prodID);
                    if (!flag)
                    {
                        Console.WriteLine(Message.invalid);
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
                    Console.WriteLine(Message.invalid);
                    goto start;
                }
                                      
                Console.WriteLine();
                Console.WriteLine(Message.design);
                RentAProd(res, prodID, cust.rentedProducts, cust);
                Console.WriteLine(Message.thanksRent);
                LoginCustomerMenu(cust);
            }

            else
            {
                Console.WriteLine(Message.noProdAva);
               
            }
        }
        public void ViewRentedProducts(Customer cust)
        {
            try
            {
                prodCtrl.viewRentedProd(cust);

            }
            catch{ Console.WriteLine(Message.noRented); }

         
        }
        public void ExtendRentPeriod(Customer cust)
        {
            ViewRentedProducts(cust);
            if(cust.rentedProducts == null) {
             
                return;
            }
            Console.WriteLine();
            start1:
            Console.WriteLine(Message.entProdId);

            int prodID;

            try
            {

                bool flag = int.TryParse(Console.ReadLine(), out prodID);
                if (!flag)
                {
                    Console.WriteLine(Message.invalid);
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
                        Console.WriteLine(Message.validId);
                        goto start1;
                    }
                }

            }
            catch
            {
                Console.WriteLine(Message.validId);
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
                    Console.WriteLine(Message.enternewEndDate);
                    rentprod.endDate = Console.ReadLine();                
                    var isValidEndDate = DateTime.TryParse(rentprod.endDate, out newEndDate);

                    if(prevEndDate == newEndDate)
                    {
                        Console.WriteLine(Message.prevDateSame);
                        Console.WriteLine();
                        goto start;
                    }
                    else if(newEndDate < prevEndDate)
                    {
                        Console.WriteLine(Message.NewDateGreater);
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
                        Console.WriteLine(Message.invalidDate);
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
                Console.WriteLine(Message.enterStartDate);
                rentprod.startDate = Console.ReadLine();
                Console.WriteLine();
                var isValidStartDate = DateTime.TryParse(rentprod.startDate, out sdt);
                if (sdt < DateTime.Today)
                {
                    Console.WriteLine(Message.enterValidDate);
                    Console.WriteLine();
                    Console.WriteLine();
                goto start;
                }
         
            DateTime edt;
            start2:
            Console.WriteLine(Message.enterEndDate);
            rentprod.endDate = Console.ReadLine();
            Console.WriteLine();
            var isValidEndDate = DateTime.TryParse(rentprod.endDate, out edt);

            if(sdt == edt) {
                Console.WriteLine(Message.startEndDateSame);
                goto start2;
            }
            else if (edt < sdt)
            {
                Console.WriteLine(Message.enterValidDate);
                goto start2;
            }

            int days;
            days = edt.Subtract(sdt).Days;

            if (isValidEndDate && isValidStartDate)
            {

                Console.WriteLine(Message.rentDays + days);

            }

            else
            {
                Console.WriteLine(Message.invalidDate);
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
                    List<Product> list = prodCtrl.getProductsMasterList();
                    list.Remove(product);
                    prodCtrl.updateDBProds(list);
                    Console.WriteLine($"Total amount for {days} is Rs.{days * product.Price}");
                }
            }
            return rentedlist;
        }
     
    }
}
