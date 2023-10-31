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

            int input = Commonn.IsValidInput();
            

            switch (input)
            {
                case (int)OwnerMenu.add_product:
                    AddProduct(owner);
                    LoginOwnerMenu(owner);
                    break;
                case (int)OwnerMenu.view_listed_products:
                    ViewListedProducts(owner);
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
                case (int)OwnerMenu.update_listed_products:
                    UpdateListedProducts(owner);
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
                case (int)OwnerMenu.delete_listed_products:
                    DeleteListedProducts(owner);
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
                case (int)OwnerMenu.logout:
                    Console.WriteLine("Logout Successful!!!");
                    Console.WriteLine();
                    owner = null;
                    UI.StartMenu();
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("Invalid Option");
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
            Console.WriteLine("Enter Product Name");
            string nameProd = Console.ReadLine();
            while (!Commonn.checkNull(nameProd))
            {
                Console.WriteLine("Name cannot be empty");
                nameProd = Console.ReadLine();
            }
            Console.WriteLine("Enter Product Description");
            string descProd = Console.ReadLine();
            while (!Commonn.checkNull(descProd))
            {
                Console.WriteLine("Description cannot be empty");
                nameProd = Console.ReadLine();
            }
            Console.WriteLine("Enter Product Category");

            int categoryProd;

            while (true)
            {
                bool flag = int.TryParse(Console.ReadLine(), out categoryProd);

                if (categoryProd < 1 || categoryProd > 12)
                {
                    Message.InvalidInput();
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
            Console.WriteLine("Enter Product Price");

            double priceProd;

            
            while (true)
            {
                bool flag = double.TryParse(Console.ReadLine(), out priceProd);

                if (priceProd <= 0 )
                {
                    Console.WriteLine("Price cannot be less than or equal to 0. Please try again.");
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
                    Console.WriteLine("Product Added Successfully!!");
                    LoginOwnerMenu(owner);
                }
                else
                {
                    Message.ErrorOcc();
                    LoginOwnerMenu(owner);
                }
            }
            catch
            {
                Message.ErrorOcc();
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
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("Product ID          - " + product.ProductId);
                    Console.WriteLine("Product Name        - " + product.ProductName);
                    Console.WriteLine("Product Description - " + product.Description);
                    Console.WriteLine("Product Price       - " + "Rs." + product.Price + " per day");
                    Console.WriteLine("Product Category    - " + Enum.Parse<Category>(product.ProductCategory.ToString()));
                }
            }
            else
            {
                Console.WriteLine("No Listed Product!!");
            }
        }
        public  void UpdateListedProducts(Owner owner)
        {
            ViewListedProducts(owner);
            if(owner.ListedProducts  == null ) {
     
                return;
            }
            start:
            Console.WriteLine("Enter Product ID to update :");
            
            int input;
            try
            {
                bool flag = int.TryParse(Console.ReadLine(), out input);
                if (!flag)
                {
                    Message.InvalidInput();
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
                        Message.InvalidInput();
                        goto start;
                    }
                }

            }
            catch
            {
                Console.WriteLine("Enter valid product ID");
                goto start;
            }
            List<Product> MasterListProducts = ownerCtrl.GetProductList();
            List<Controller.Owner> list = new List<Controller.Owner>();
            List<Controller.Customer> CustomerList = ownerCtrl.GetCustomerList();

            bool isRented = CustomerList.Any((cust) => cust.rentedProducts != null && cust.rentedProducts.Any((prod)
                => prod.ProductId == input));

            if (isRented)
            {
                Console.WriteLine("You cannot update this product as it is already rented.");
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
                            Message.InvalidInput();
                            goto start2;
                        }
                        else
                        {
                            switch (opt)
                            {
                                case (int)UpdateProductMenu.product_name:
                                    Console.WriteLine("Enter new name");
                                    string name = Console.ReadLine();
                                    while (!Commonn.checkNull(name))
                                    {
                                        Console.WriteLine("Name cannot be empty. Please try again");
                                        name = Console.ReadLine();
                                    }
                                    product.ProductName = name;
                                    Console.WriteLine("Name Changed Successfully!!");
                                    Console.WriteLine("-------------------------------------------");
                                    break;
                                case (int)UpdateProductMenu.product_description:
                                    Console.WriteLine("Enter new description");
                                    string desc = Console.ReadLine();
                                    while (!Commonn.checkNull(desc) || desc.Length < 10)
                                    {
                                        Console.WriteLine("Description cannot be less than 10 characters");
                                        name = Console.ReadLine();
                                    }
                                    product.Description = desc;
                                    Console.WriteLine("Description Changed Successfully!!");
                                    Console.WriteLine("-------------------------------------------");
                                    break;
                                case (int)UpdateProductMenu.product_price:
                                    Console.WriteLine("Enter new price");

                                    double priceProd;
                                    while (true)
                                    {
                                        bool flag2 = double.TryParse(Console.ReadLine(), out priceProd);

                                        if (priceProd <= 0)
                                        {
                                            Console.WriteLine("Price cannot be less than or equal to 0. Please try again.");
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
                                    Console.WriteLine("Price Changed Successfully!!");
                                    Console.WriteLine("-------------------------------------------");
                                    break;
                                case (int)UpdateProductMenu.product_category:
                                    Console.WriteLine("Enter new Category");
                                    int categoryProd;
                                    while (true)
                                    {
                                        bool flag1 = int.TryParse(Console.ReadLine(), out categoryProd);

                                        if (categoryProd < 1 || categoryProd > 12)
                                        {
                                            Message.InvalidInput();
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
                                    Console.WriteLine("Category Changed Successfully!!");
                                    Console.WriteLine("-------------------------------------------");
                                    break;

                                default:
                                    Message.InvalidInput();
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
                        Message.InvalidInput();
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
