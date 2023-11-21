using RentJunction.Models;

namespace RentJunction.Views
{

    public class CustomerUI
    {
        public CustomerController CustomerController { get; set; }  
        public IProductControllerCust ProductController { get; set; }

        public List<Product> GetProductsRentedByCustomer;
        public CustomerUI(CustomerController customerController, IProductControllerCust productController)
        {
           
            CustomerController = customerController;
            ProductController = productController;
        }
        public void LoginCustomerMenu(User customer)
        {
            Console.WriteLine(customer.FullName + Strings.loginCust);
            Strings.Design();
            Console.WriteLine(Strings.custMenu);
            Console.WriteLine(Strings.design);
            GetProductsRentedByCustomer = ProductController.GetRentedProductsByCustomer(customer);


            var input = Console.ReadLine();
            bool isValidInput;

            while (true)
            {
                isValidInput = CheckValidity.IsValidInput(input);

                if (!isValidInput)
                {
                    Console.WriteLine(Strings.invalid);
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }

            CustomerMenu option = Enum.Parse<CustomerMenu>(input);

            Console.WriteLine();

            switch (option)
            {
                case CustomerMenu.BrowseProducts:
                    BrowseProducts(customer);
                    Console.WriteLine();
                    LoginCustomerMenu(customer);
                    break;
                case CustomerMenu.View_rented_products:
                    ViewProducts();
                    Strings.Design();
                    LoginCustomerMenu(customer);
                    break;
                case CustomerMenu.Extend_rent_period:
                    ExtendRentPeriod();
                    Console.WriteLine();
                    LoginCustomerMenu(customer);
                    break;
                case CustomerMenu.logout:
                    Console.WriteLine(Strings.logoutSucc);
                    customer = null;
                    Console.WriteLine();
                    return;
                default:
                    Strings.Design();
                    Console.WriteLine();
                    LoginCustomerMenu(customer);
                    break;
            }
        }
        public void BrowseProducts(User customer)
        {
            Console.WriteLine(Strings.entCity);

            string address  = Console.ReadLine();
            bool isValidAddress;

            while (true)
            {
                isValidAddress = CheckValidity.IsValidAddress(address);
                if (isValidAddress)
                {
                    break;
                }
                else
                {
                    address = Console.ReadLine();
                }
            }

            Console.WriteLine(Strings.design);
            Console.WriteLine(Strings.chooseCate);

            List<string> categories = ProductController.ChooseCategory();

            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }

            var input = Console.ReadLine();
            var isValidInput = CheckValidity.IsValidInput(input);

            while (true)
            {
                if (!isValidInput)
                {
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }


            List<Product> productList = ProductController.GetProducts(Convert.ToInt32(input), address);

            if (productList.Count > 0)
            {
                Strings.Design();
                start:
                Console.WriteLine(Strings.prodIdEnt);
                int prodID;
                try
                {
                    bool isValidId = int.TryParse(Console.ReadLine() , out prodID);
                    if (!isValidId)
                    {
                        Console.WriteLine(Strings.invalid);
                        goto start;
                    }
                    else {
                      bool flag2 = false; 
                     foreach(var product in productList)
                        {
                            if (product.ProductId.Equals(prodID))
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
                RentAProd(productList,prodID, customer);
                Console.WriteLine();
                Console.WriteLine(Strings.thanksRent);
                
                Console.WriteLine();
                Console.WriteLine(Strings.design);
                LoginCustomerMenu(customer);
            }

            else
            {
                Console.WriteLine(Strings.noProdAva);
               
            }
        }
        public void ViewProducts()
        {
            foreach (var product in GetProductsRentedByCustomer)
            {
                Console.WriteLine(Strings.design);
                Console.WriteLine(Strings.disProdId + product.ProductId);
                Console.WriteLine(Strings.disProdName + product.ProductName);
                Console.WriteLine(Strings.disProdDesc + product.Description);
                Console.WriteLine(Strings.disProdPrice + product.Price + " per day");
                Console.WriteLine(Strings.disProdCate + Enum.Parse<Category>(product.ProductCategory.ToString()));
                Console.WriteLine(Strings.startDate + product.StartDate);
                Console.WriteLine(Strings.endDate + product.EndDate);
            }


        }
        public void ExtendRentPeriod()
        {
            ViewProducts();
            if(GetProductsRentedByCustomer == null) 
            return;
            
            Console.WriteLine();
            start1:
            Console.WriteLine(Strings.entProdId);

            int prodID;

            try
            {

                bool isValidId = int.TryParse(Console.ReadLine(), out prodID);
                if (!isValidId)
                {
                    Console.WriteLine(Strings.invalid);
                    goto start1;
                }
                else
                {
                    bool flag2 = false;
                    foreach (var prod in GetProductsRentedByCustomer)
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

            List<Product> products = ProductController.GetProductsMasterList();
            Product rentedProduct = products.Find((product) => product.ProductId.Equals(prodID));
            
            DateTime prevEndDate;
            DateTime newEndDate;

            var isValidPrevEndDate = DateTime.TryParse(rentedProduct.EndDate, out prevEndDate);

            Console.WriteLine(Strings.enternewEndDate);
            rentedProduct.EndDate = Console.ReadLine();
            var isValidEndDate = DateTime.TryParse(rentedProduct.EndDate, out newEndDate);

            while (true)
            {
                if(prevEndDate < newEndDate)
                {
                    break;
                }
                if (prevEndDate == newEndDate)
                {
                    Console.WriteLine(Strings.prevDateSame);
                    Console.WriteLine();
                  
                }
                else if (newEndDate < prevEndDate)
                {
                    Console.WriteLine(Strings.NewDateGreater);
                    Console.WriteLine();
                   
                }
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

            Console.WriteLine(Strings.remainingAmt + differenceDays * rentedProduct.Price);
            ProductController.UpdateDBProds(products);
        }
        public void RentAProd(List<Product> productList,int productId,User customer)
        {     
            Product rentProduct = productList.Find((product) => product.ProductId == productId);   
           
            DateTime sdt;
            Console.WriteLine(Strings.enterStartDate);
            rentProduct.StartDate = Console.ReadLine();
            Console.WriteLine();
            bool isValidStartDate;
            while (true)
            {  
                isValidStartDate = DateTime.TryParse(rentProduct.StartDate, out sdt);
                if (!(sdt < DateTime.Today))
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine(Strings.enterValidDate);
                    rentProduct.StartDate = Console.ReadLine();
                }
            }     
            DateTime edt;
            Console.WriteLine(Strings.enterEndDate);
            rentProduct.EndDate = Console.ReadLine();
            Console.WriteLine();
            bool isValidEndDate;
            while (true)
            {
                isValidEndDate = DateTime.TryParse(rentProduct.EndDate, out edt);
                if (edt > sdt)
                {
                    break;
                }
                else if (sdt == edt)
                {
                    Console.WriteLine(Strings.startEndDateSame);
                    
                }
                else
                {
                    Console.WriteLine(Strings.enterValidDate);
                   
                }
            }
            int days;
            days = edt.Subtract(sdt).Days;

            if (isValidEndDate && isValidStartDate)
            {
                Console.WriteLine(Strings.rentDays + days);
                Console.WriteLine(Strings.design);
            }

            else
            {
                Console.WriteLine(Strings.invalidDate);
            }

            rentProduct.CustomerID = customer.UserID;
            List<Product> productMasterList = ProductController.GetProductsMasterList();
            int index = productMasterList.FindIndex((product) => product.ProductId.Equals(productId));
            productMasterList[index] = rentProduct;
            ProductController.UpdateDBProds(productMasterList);
            Console.WriteLine("Total Price is " + rentProduct.Price * days);
        }
     
    }
}
