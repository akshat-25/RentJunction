using RentJunction.Controller;
using RentJunction.Models;


namespace RentJunction.Views
{
    public class CustomerUI
    {
        CustomerController custCtrl;
        IProductControllerCust prodCtrl;

        public CustomerUI()
        {
            custCtrl = new CustomerController();
            prodCtrl = new ProductController();
        }
        public void LoginCustomerMenu(Customer cust)
        {
            Console.WriteLine(Strings.custMenu);
            Console.WriteLine(Strings.design);

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
                    Strings.Design();
                    LoginCustomerMenu(cust);
                    break;
                case CustomerMenu.Extend_rent_period:
                    ExtendRentPeriod(cust);
                    Console.WriteLine();
                    LoginCustomerMenu(cust);
                    break;
                case CustomerMenu.logout:
                    Console.WriteLine(Strings.logoutSucc);
                    cust = null;
                    Console.WriteLine();
                    UI.StartMenu();
                    break ;
                default:
                    Strings.Design();
                    Console.WriteLine();
                    LoginCustomerMenu(cust);
                    break;
            }
        }
        public void BrowseProducts(Customer cust)
        {
            Console.WriteLine(Strings.entCity);
            string address = CheckValidity.IsValidAddress();
            Console.WriteLine(Strings.design);
            Console.WriteLine(Strings.chooseCate);

            custCtrl.ChooseCategory();

            int input = CheckValidity.IsValidInput();
            
            List<Product> res = prodCtrl.GetProducts(input, address);

            if (res.Count > 0)
            {
                Strings.Design();
                start:
                Console.WriteLine(Strings.prodIdEnt);
                int prodID;
                try
                {
                    bool flag = int.TryParse(Console.ReadLine() , out prodID);
                    if (!flag)
                    {
                        Console.WriteLine(Strings.invalid);
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
                    Console.WriteLine(Strings.invalid);
                    goto start;
                }
                                      
                Console.WriteLine();
                Console.WriteLine(Strings.design);
                RentAProd(res, prodID, cust.rentedProducts, cust);
                Console.WriteLine();
                Console.WriteLine(Strings.thanksRent);
                
                Console.WriteLine();
                Console.WriteLine(Strings.design);
                LoginCustomerMenu(cust);
            }

            else
            {
                Console.WriteLine(Strings.noProdAva);
               
            }
        }
        public void ViewRentedProducts(Customer cust)
        {
            try
            {
                prodCtrl.ViewRentedProd(cust);

            }
            catch{ Console.WriteLine(Strings.noRented); }

         
        }
        public void ExtendRentPeriod(Customer cust)
        {
            ViewRentedProducts(cust);
            if(cust.rentedProducts == null) {
             
                return;
            }
            Console.WriteLine();
            start1:
            Console.WriteLine(Strings.entProdId);

            int prodID;

            try
            {

                bool flag = int.TryParse(Console.ReadLine(), out prodID);
                if (!flag)
                {
                    Console.WriteLine(Strings.invalid);
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
                        Console.WriteLine(Strings.validId);
                        goto start1;
                    }
                }

            }
            catch
            {
                Console.WriteLine(Strings.validId);
                goto start1;
            }

            List<Customer> list = custCtrl.GetCustomer();
            foreach (var rentprod in cust.rentedProducts)
            {
                if (prodID.Equals(rentprod.ProductId))
                {                   
                    DateTime prevEndDate;
                    DateTime newEndDate;

                    var isValidPrevEndDate = DateTime.TryParse(rentprod.EndDate, out prevEndDate);

                    start:
                    Console.WriteLine(Strings.enternewEndDate);
                    rentprod.EndDate = Console.ReadLine();                
                    var isValidEndDate = DateTime.TryParse(rentprod.EndDate, out newEndDate);

                    if(prevEndDate == newEndDate)
                    {
                        Console.WriteLine(Strings.prevDateSame);
                        Console.WriteLine();
                        goto start;
                    }
                    else if(newEndDate < prevEndDate)
                    {
                        Console.WriteLine(Strings.NewDateGreater);
                        Console.WriteLine();
                        goto start;
                    }
                    int differenceDays;
                    differenceDays = newEndDate.Subtract(prevEndDate).Days;

                    if (isValidEndDate && isValidPrevEndDate)
                    {

                        Console.WriteLine(Strings.daysOfRent + differenceDays);

                    }
                    else
                    {
                        Console.WriteLine(Strings.invalidDate);
                    }

                    Console.WriteLine(Strings.remainingAmt + differenceDays * rentprod.Price);
                    custCtrl.UpdateDBCust(list);
                }
            }
        }
        public List<RentedProduct> RentAProd(List<Product> Masterlist, int input, 
            List<RentedProduct> rentedlist,Customer cust)
        {
            List<Customer> listcust = custCtrl.GetCustomer();
            RentedProduct rentprod = new RentedProduct();
                
                DateTime sdt;
                start:
                Console.WriteLine(Strings.enterStartDate);
                rentprod.StartDate = Console.ReadLine();
                Console.WriteLine();
                var isValidStartDate = DateTime.TryParse(rentprod.StartDate, out sdt);
                if (sdt < DateTime.Today)
                {
                    Console.WriteLine(Strings.enterValidDate);
                    Console.WriteLine();
                    Console.WriteLine();
                goto start;
                }
         
            DateTime edt;
            start2:
            Console.WriteLine(Strings.enterEndDate);
            rentprod.EndDate = Console.ReadLine();
            Console.WriteLine();
            var isValidEndDate = DateTime.TryParse(rentprod.EndDate, out edt);

            if(sdt == edt) {
                Console.WriteLine(Strings.startEndDateSame);
                goto start2;
            }
            else if (edt < sdt)
            {
                Console.WriteLine(Strings.enterValidDate);
                goto start2;
            }

            int days;
            days = edt.Subtract(sdt).Days;

            if (isValidEndDate && isValidStartDate)
            {

                Console.WriteLine(Strings.rentDays + days);

            }

            else
            {
                Console.WriteLine(Strings.invalidDate);
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
                    custCtrl.UpdateDBCust(listcust);
                    List<Product> list = prodCtrl.GetProductsMasterList();
                    list.Remove(product);
                    prodCtrl.UpdateDBProds(list);
                    Console.WriteLine();
                    Console.WriteLine(Strings.design);
                }
            }
            Console.WriteLine("Total Price is " + rentprod.Price * days);
            return rentedlist;
        }
     
    }
}
