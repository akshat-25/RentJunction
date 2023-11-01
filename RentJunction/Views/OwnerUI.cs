using commonData;
using MenuOpt;
using RentJunction.Controller;
using RentJunction.Models;
using System.Security.Cryptography;

namespace RentJunction.Views
{
    public class OwnerUI
    {
        OwnerController ownerCtrl = new OwnerController();
        public void LoginOwnerMenu(Owner owner)
        {
            MenuOptions.ownerMenu();

            OwnerMenu input = (OwnerMenu)CheckValidity.IsValidInput();
            

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
            List<string> categories = DbHandler.Instance.chooseCategory();
            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(Message.prodname);
            string nameProd = Console.ReadLine();
            while (!CheckValidity.checkNull(nameProd))
            {
                Console.WriteLine(Message.nameEmpty);
                nameProd = Console.ReadLine();
            }
            Console.WriteLine(Message.prodDesc);
            string descProd = Console.ReadLine();
            while (!CheckValidity.checkNull(descProd))
            {
                Console.WriteLine(Message.descEmpty);
                nameProd = Console.ReadLine();
            }
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
                List<Product> prods = ownerCtrl.GetProductList();
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
            List<Owner> listowner = ownerCtrl.getOwnerList();

            if (ownerCtrl.AddProductMaster(product))
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

                int index= DbHandler.Instance._ownerList.FindIndex((obj)=>obj.Username==owner.Username);
                DbHandler.Instance._ownerList[index] = owner;
                ownerCtrl.updateDBOwner(listowner);
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
            start:
            Console.WriteLine(Message.prodIdUpdate);
            
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
            List<Product> MasterListProducts = ownerCtrl.GetProductList();
            List<Controller.Owner> list = new List<Controller.Owner>();
            List<Controller.Customer> CustomerList = ownerCtrl.GetCustomerList();

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
                    MenuOptions.updateListedProdMenuOptions();
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
                                    Console.WriteLine("Enter new name");
                                    string name = Console.ReadLine();
                                    while (!CheckValidity.checkNull(name))
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
                                    while (!CheckValidity.checkNull(desc) || desc.Length < 10)
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
                            ownerCtrl.updateDBProducts(MasterListProducts);
                            ownerCtrl.UpdateCustList(product, owner);
                            ownerCtrl.updateDBOwner(list);
                        }

                    }
                    catch
                    {
                        Console.WriteLine(Message.invalid);
                        goto start2;
                    }
                }
            }

        }
        public void DeleteListedProducts(Owner owner)
        {
            List<Owner> list = new List<Owner>();
            
            ViewListedProducts(owner);
            if (owner.ListedProducts == null)
            {
                return;
            }
        start:
            Console.WriteLine("Enter the product ID to delete the product");
            int input;
            try
            {
                bool flag = int.TryParse(Console.ReadLine(), out input);
                if (!flag)
                {
                    Console.WriteLine("Invalid Id. Please try again");
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
                        Console.WriteLine("Enter valid product ID");
                        goto start;
                    }
                }

            }
            catch
            {
                Console.WriteLine("Enter valid product ID");
                goto start;
            }
            List<Controller.Customer> CustomerList = ownerCtrl.GetCustomerList();
            bool isRented = CustomerList.Any((cust) => cust.rentedProducts != null && cust.rentedProducts.Any((prod)
                => prod.ProductId == input));

            if (isRented)
            {
                Console.WriteLine("You cannot delete this product as it is already rented.");
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

            List<Product> MasterListProducts = ownerCtrl.GetProductList();
            foreach (var product in MasterListProducts)
            {
                if (product.ProductId.Equals(input))
                {
                    MasterListProducts.Remove(product);
                    break;
                }
            }
            ownerCtrl.updateDBOwner(list);
            ownerCtrl.updateDBProducts(MasterListProducts);

        }
    }
}
