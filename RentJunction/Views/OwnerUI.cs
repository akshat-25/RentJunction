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
            Console.WriteLine(Message.ownerMenu);
            OwnerMenu input = (OwnerMenu)CheckValidity.IsValidInput();
            Console.WriteLine(Message.design);
            Console.WriteLine();
            
            switch (input)
            {
                case OwnerMenu.add_product:
                    AddProduct(owner);
                    LoginOwnerMenu(owner);
                    break;
                case OwnerMenu.view_listed_products:
                    ViewListedProducts(owner);
                    Console.WriteLine(Message.design);
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
                    Console.WriteLine(Message.logoutSuccc);
                    Console.WriteLine();
                    owner = null;
                    UI.StartMenu();
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine(Message.invalid);
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
            Console.WriteLine(Message.prodname);
            string nameProd = Console.ReadLine();
            while (!CheckValidity.CheckNull(nameProd))
            {
                Console.WriteLine(Message.nameEmpty);
                nameProd = Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine(Message.prodDesc);
            string descProd = Console.ReadLine();
            while (!CheckValidity.CheckNull(descProd))
            {
                Console.WriteLine(Message.descEmpty);
                nameProd = Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine(Message.prodCate);

            int categoryProd;

            while (true)
            {
                bool flag = int.TryParse(Console.ReadLine(), out categoryProd);

                if (categoryProd < 1 || categoryProd > 12)
                {
                    Console.WriteLine(Message.invalid);
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
            Console.WriteLine(Message.prodPrice);

            double priceProd;

            
            while (true)
            {
                bool flag = double.TryParse(Console.ReadLine(), out priceProd);

                if (priceProd <= 0 )
                {
                    Console.WriteLine(Message.prodPriceError);
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
                    Console.WriteLine(Message.prodSucc);
                    LoginOwnerMenu(owner);
                }
                else
                {
                    Console.WriteLine(Message.error);
                    LoginOwnerMenu(owner);
                }
            }
            catch
            {
                Console.WriteLine(Message.error);
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
                    Console.WriteLine(Message.design);
                    Console.WriteLine(Message.disProdId + product.ProductId);
                    Console.WriteLine(Message.disProdName + product.ProductName);
                    Console.WriteLine(Message.disProdDesc + product.Description);
                    Console.WriteLine(Message.disProdPrice + "Rs." + product.Price + " per day");
                    Console.WriteLine(Message.disProdCate + Enum.Parse<Category>(product.ProductCategory.ToString()));
                }
            }
            else
            {
                Console.WriteLine(Message.noProd);
            }
        }
        public  void UpdateListedProducts(Owner owner)
        {
            ViewListedProducts(owner);

            if(owner.ListedProducts  == null ) {
     
                return;
            }
            Console.WriteLine(Message.design);
            Console.WriteLine();
            Console.WriteLine();
        start:
            Console.WriteLine(Message.prodIdUpdate);
            Console.WriteLine();
            Console.WriteLine();
            int input;
            try
            {
                bool flag = int.TryParse(Console.ReadLine(), out input);
                if (!flag)
                {
                    Console.WriteLine(Message.invalid);
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
                        Console.WriteLine(Message.invalid);
                        goto start;
                    }
                }

            }
            catch
            {
                Console.WriteLine(Message.validId);
                goto start;
            }

            List<Product> MasterListProducts = prodCtrl.GetProductsMasterList();
            List<Owner> list = ownerCtrl.GetOwnerList();
            List<Customer> CustomerList = custCtrl.GetCustomer();

            bool isRented = CustomerList.Any((cust) => cust.rentedProducts != null && cust.rentedProducts.Any((prod)
                => prod.ProductId == input));

            if (isRented)
            {
                Console.WriteLine(Message.cannotDeleteCust);
                Console.WriteLine();
                ViewListedProducts(owner);
                
            }
            foreach (var product in MasterListProducts)
            {
                if (product.ProductId.Equals(input))
                {
                    start2:
                    Console.WriteLine(Message.updateListedProdMenuOptions);
                    int opt;
                    try
                    {
                        bool flag = int.TryParse(Console.ReadLine(),out opt);
                        if (!flag)
                        {
                            Console.WriteLine(Message.invalid);
                            goto start2;
                        }
                        else
                        {
                            switch (opt)
                            {
                                case (int)UpdateProductMenu.product_name:
                                    Console.WriteLine(Message.prodUpdateName);
                                    string name = Console.ReadLine();
                                    Console.WriteLine();
                                    while (!CheckValidity.CheckNull(name))
                                    {
                                        Console.WriteLine(Message.nameError);
                                        name = Console.ReadLine();
                                    }
                                    product.ProductName = name;
                                    Console.WriteLine(Message.nameChangedSucc);
                                    Console.WriteLine(Message.design);
                                    break;
                                case (int)UpdateProductMenu.product_description:
                                    Console.WriteLine(Message.enterNewDesc);
                                    string desc = Console.ReadLine();
                                    Console.WriteLine();
                                    while (!CheckValidity.CheckNull(desc) || desc.Length < 10)
                                    {
                                        Console.WriteLine(Message.descError);
                                        desc = Console.ReadLine();
                                    }
                                    product.Description = desc;
                                    Console.WriteLine(Message.descChangedSucc);
                                    Console.WriteLine(Message.design);
                                    break;
                                case (int)UpdateProductMenu.product_price:
                                    Console.WriteLine(Message.enterNewPrice);

                                    double priceProd;
                                    while (true)
                                    {
                                        bool flag2 = double.TryParse(Console.ReadLine(), out priceProd);

                                        if (priceProd <= 0)
                                        {
                                            Console.WriteLine(Message.prodPriceError);
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
                                    Console.WriteLine(Message.priceChangedSucc);
                                    Console.WriteLine(Message.design);
                                    break;
                                case (int)UpdateProductMenu.product_category:
                                    Console.WriteLine(Message.enterNewCate);
                                    int categoryProd;
                                    while (true)
                                    {
                                        bool flag1 = int.TryParse(Console.ReadLine(), out categoryProd);

                                        if (categoryProd < 1 || categoryProd > 12)
                                        {
                                            Console.WriteLine(Message.invalid);
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
                                    Console.WriteLine(Message.CateChangedSucc);
                                    Console.WriteLine(Message.design);
                                    break;

                                default:
                                    Console.WriteLine(Message.invalid);
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
                        File.AppendAllText(Message.errorLoggerPath, ex.ToString() + DateTime.Now);
                        Console.WriteLine(Message.invalid);
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
            Console.WriteLine(Message.prodDelID);
            Console.WriteLine();
            int input;
            try
            {
                bool flag = int.TryParse(Console.ReadLine(), out input);
                if (!flag)
                {
                    Console.WriteLine(Message.invalid);
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
                        Console.WriteLine(Message.invalid);
                        goto start;
                    }
                }

            }
            catch(Exception ex)
            {
                File.AppendAllText(Message.errorLoggerPath, ex.ToString());
                Console.WriteLine(Message.invalid);
                goto start;
            }
            List<Controller.Customer> CustomerList = custCtrl.GetCustomer();
            bool isRented = CustomerList.Any((cust) => cust.rentedProducts != null && cust.rentedProducts.Any((prod)
                => prod.ProductId == input));

            if (isRented)
            {
                Console.WriteLine(Message.prodDelDenied);
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
            Console.WriteLine(Message.prodDelSucc);
        }
    }
}
