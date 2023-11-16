using RentJunction.Controller;
using RentJunction.Models;
using System.Security.Cryptography;

namespace RentJunction.Views
{
    public class OwnerUI
    {
        OwnerController ownerCtrl;
        ICustomerController custCtrl;
        IProductControllerOwner prodCtrl ;

        public OwnerUI()
        {
            ownerCtrl = new OwnerController();
            custCtrl  = new CustomerController();
            prodCtrl  = new ProductController();
        }
        public void LoginOwnerMenu(Owner owner)
        {
            Console.WriteLine(Strings.ownerMenu);
            OwnerMenu input = (OwnerMenu)CheckValidity.IsValidInput();
            Console.WriteLine(Strings.design);
            Console.WriteLine();
            
            switch (input)
            {
                case OwnerMenu.add_product:
                    AddProduct(owner);
                    LoginOwnerMenu(owner);
                    break;
                case OwnerMenu.view_listed_products:
                    ViewListedProducts(owner);
                    Console.WriteLine(Strings.design);
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
                case OwnerMenu.update_listed_products:
                    UpdateListedProducts(owner);
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
                case OwnerMenu.delete_listed_products:
                    DeleteListedProducts(owner);
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
                case OwnerMenu.logout:
                    Console.WriteLine(Strings.logoutSuccc);
                    Console.WriteLine();
                    owner = null;
                    UI.StartMenu();
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine(Strings.invalid);
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
            }
        }
        public void AddProduct(Owner owner)
        {
            List<string> categories = DBProduct.Instance.chooseCategory();
            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(Strings.prodname);
            string nameProd = Console.ReadLine();
            while (!CheckValidity.CheckNull(nameProd))
            {
                Console.WriteLine(Strings.nameEmpty);
                nameProd = Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine(Strings.prodDesc);
            string descProd = Console.ReadLine();
            while (!CheckValidity.CheckNull(descProd))
            {
                Console.WriteLine(Strings.descEmpty);
                nameProd = Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine(Strings.prodCate);

            int categoryProd;

            while (true)
            {
                bool flag = int.TryParse(Console.ReadLine(), out categoryProd);

                if (categoryProd < 1 || categoryProd > 12)
                {
                    Console.WriteLine(Strings.invalid);
                }
                else if (flag)
                {
                    break;
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
            Console.WriteLine(Strings.prodPrice);

            double priceProd;

            
            while (true)
            {
                bool flag = double.TryParse(Console.ReadLine(), out priceProd);

                if (priceProd <= 0 )
                {
                    Console.WriteLine(Strings.prodPriceError);
                }
                else if(flag)
                {
                    break;
                }
                else
                {
                    break;
                }
            }
            int id = RandomNumberGenerator.GetInt32(0, int.MaxValue);
            while (true)
            {
                List<Product> prods = prodCtrl.GetProductsMasterList();
                if (prods != null && prods.Any((prod) => prod.ProductId == id))
                {
                    id = RandomNumberGenerator.GetInt32(0, int.MaxValue);
                }
                else
                {
                    break;
                }
            }

            Product product = new Product()
            {
                ProductId = id,
                ProductName = nameProd,
                Description = descProd,
                Price = priceProd,
                OwnerName = owner.FullName,
                OwnerNum = owner.PhoneNumber,
                ProductCategory = categoryProd,
                City = owner.Address
            };
            try
            {
                if (AddProducts(product,owner))
                {
                    Console.WriteLine(Strings.prodSucc);
                    LoginOwnerMenu(owner);
                }
                else
                {
                    Console.WriteLine(Strings.error);
                    LoginOwnerMenu(owner);
                }
            }
            catch
            {
                Console.WriteLine(Strings.error);
                LoginOwnerMenu(owner);
            }
        }
        public bool AddProducts(Product product, Owner owner)
        {
            List<Owner> listowner = ownerCtrl.GetOwnerList();

            if (prodCtrl.AddProductMaster(product))
            {
                if (owner.ListedProducts != null)
                {
                    owner.ListedProducts.Add(product);
                }

                else
                {
                    owner.ListedProducts = new List<Product>();
                    owner.ListedProducts.Add(product);
                }

                int index= DBOwner.Instance._ownerList.FindIndex((obj)=>obj.Username==owner.Username);
                DBOwner.Instance._ownerList[index] = owner;
                ownerCtrl.UpdateDBOwner(listowner);
                return true;
            }

            return false;
        }
        public void ViewListedProducts(Owner owner)
        {
            if (owner.ListedProducts  !=  null)
            {
                foreach (Product product in owner.ListedProducts)
                {
                    Console.WriteLine(Strings.design);
                    Console.WriteLine(Strings.disProdId + product.ProductId);
                    Console.WriteLine(Strings.disProdName + product.ProductName);
                    Console.WriteLine(Strings.disProdDesc + product.Description);
                    Console.WriteLine(Strings.disProdPrice + "Rs." + product.Price + " per day");
                    Console.WriteLine(Strings.disProdCate + Enum.Parse<Category>(product.ProductCategory.ToString()));
                }
            }
            else
            {
                Console.WriteLine(Strings.noProd);
            }
        }
        public  void UpdateListedProducts(Owner owner)
        {
            ViewListedProducts(owner);

            if(owner.ListedProducts  == null ) {
     
                return;
            }
            Console.WriteLine(Strings.design);
            Console.WriteLine();
            Console.WriteLine();
        start:
            Console.WriteLine(Strings.prodIdUpdate);
            Console.WriteLine();
            Console.WriteLine();
            int input;
            try
            {
                bool flag = int.TryParse(Console.ReadLine(), out input);
                if (!flag)
                {
                    Console.WriteLine(Strings.invalid);
                    goto start;
                }
                else
                {
                    bool flag2 = false;
                    foreach (var prod in owner.ListedProducts)
                    {
                        if (prod.ProductId.Equals(input))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        Console.WriteLine(Strings.invalid);
                        goto start;
                    }
                }

            }
            catch
            {
                Console.WriteLine(Strings.validId);
                goto start;
            }

            List<Product> MasterListProducts = prodCtrl.GetProductsMasterList();
            List<Owner> list = ownerCtrl.GetOwnerList();
            List<Customer> CustomerList = custCtrl.GetCustomer();

            bool isRented = CustomerList.Any((cust) => cust.rentedProducts != null && cust.rentedProducts.Any((prod)
                => prod.ProductId == input));

            if (isRented)
            {
                Console.WriteLine(Strings.cannotDeleteCust);
                Console.WriteLine();
                ViewListedProducts(owner);
                
            }
            foreach (var product in MasterListProducts)
            {
                if (product.ProductId.Equals(input))
                {
                    start2:
                    Console.WriteLine(Strings.updateListedProdMenuOptions);
                    int opt;
                    try
                    {
                        bool flag = int.TryParse(Console.ReadLine(),out opt);
                        if (!flag)
                        {
                            Console.WriteLine(Strings.invalid);
                            goto start2;
                        }
                        else
                        {
                            switch (opt)
                            {
                                case (int)UpdateProductMenu.product_name:
                                    Console.WriteLine(Strings.prodUpdateName);
                                    string name = Console.ReadLine();
                                    Console.WriteLine();
                                    while (!CheckValidity.CheckNull(name))
                                    {
                                        Console.WriteLine(Strings.nameError);
                                        name = Console.ReadLine();
                                    }
                                    product.ProductName = name;
                                    Console.WriteLine(Strings.nameChangedSucc);
                                    Console.WriteLine(Strings.design);
                                    break;
                                case (int)UpdateProductMenu.product_description:
                                    Console.WriteLine(Strings.enterNewDesc);
                                    string desc = Console.ReadLine();
                                    Console.WriteLine();
                                    while (!CheckValidity.CheckNull(desc) || desc.Length < 10)
                                    {
                                        Console.WriteLine(Strings.descError);
                                        desc = Console.ReadLine();
                                    }
                                    product.Description = desc;
                                    Console.WriteLine(Strings.descChangedSucc);
                                    Console.WriteLine(Strings.design);
                                    break;
                                case (int)UpdateProductMenu.product_price:
                                    Console.WriteLine(Strings.enterNewPrice);

                                    double priceProd;
                                    while (true)
                                    {
                                        bool flag2 = double.TryParse(Console.ReadLine(), out priceProd);

                                        if (priceProd <= 0)
                                        {
                                            Console.WriteLine(Strings.prodPriceError);
                                        }
                                        else if (flag2)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    Console.WriteLine();
                                    product.Price = priceProd;
                                    Console.WriteLine(Strings.priceChangedSucc);
                                    Console.WriteLine(Strings.design);
                                    break;
                                case (int)UpdateProductMenu.product_category:
                                    Console.WriteLine(Strings.enterNewCate);
                                    int categoryProd;
                                    while (true)
                                    {
                                        bool flag1 = int.TryParse(Console.ReadLine(), out categoryProd);

                                        if (categoryProd < 1 || categoryProd > 12)
                                        {
                                            Console.WriteLine(Strings.invalid);
                                        }
                                        else if (flag1)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    product.ProductCategory = categoryProd;
                                    Console.WriteLine(Strings.CateChangedSucc);
                                    Console.WriteLine(Strings.design);
                                    break;

                                default:
                                    Console.WriteLine(Strings.invalid);
                                    goto start2;
                                    break;
                            }
                            prodCtrl.UpdateDBProds(MasterListProducts);
                            ownerCtrl.UpdateOwnerList(product, owner);
                            ownerCtrl.UpdateDBOwner(list);
                        }

                    }
                    catch(Exception ex)
                    {
                        File.AppendAllText(Strings.errorLoggerPath, ex.ToString() + DateTime.Now);
                        Console.WriteLine(Strings.invalid);
                        goto start2;
                    }
                }
            }

        }
        public void DeleteListedProducts(Owner owner)
        {
            List<Owner> list = ownerCtrl.GetOwnerList();
            
            ViewListedProducts(owner);
            if (owner.ListedProducts == null)
            {
                return;
            }
        start:
            Console.WriteLine(Strings.prodDelID);
            Console.WriteLine();
            int input;
            try
            {
                bool flag = int.TryParse(Console.ReadLine(), out input);
                if (!flag)
                {
                    Console.WriteLine(Strings.invalid);
                    goto start;
                }
                else
                {
                    bool flag2 = false;
                    foreach (var prod in owner.ListedProducts)
                    {
                        if (prod.ProductId.Equals(input))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        Console.WriteLine(Strings.invalid);
                        goto start;
                    }
                }

            }
            catch(Exception ex)
            {
                File.AppendAllText(Strings.errorLoggerPath, ex.ToString());
                Console.WriteLine(Strings.invalid);
                goto start;
            }
            List<Controller.Customer> CustomerList = custCtrl.GetCustomer();
            bool isRented = CustomerList.Any((cust) => cust.rentedProducts != null && cust.rentedProducts.Any((prod)
                => prod.ProductId == input));

            if (isRented)
            {
                Console.WriteLine(Strings.prodDelDenied);
                Console.WriteLine();
                ViewListedProducts(owner);

            }

            foreach (var product in owner.ListedProducts)
            {
                if (product.ProductId.Equals(input))
                {
                    owner.ListedProducts.Remove(product);
                    break;
                }
            }

            List<Product> MasterListProducts = prodCtrl.GetProductsMasterList();
            foreach (var product in MasterListProducts)
            {
                if (product.ProductId.Equals(input))
                {
                    MasterListProducts.Remove(product);
                    break;
                }
            }
            ownerCtrl.UpdateDBOwner(list);
            prodCtrl.UpdateDBProds(MasterListProducts);
            Console.WriteLine(Strings.prodDelSucc);
        }
    }
}
