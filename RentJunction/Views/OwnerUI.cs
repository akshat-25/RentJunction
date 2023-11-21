using RentJunction.Models;
using System.Security.Cryptography;

namespace RentJunction.Views
{
    public class OwnerUI
    {
        public OwnerController OwnerController { get; set; }
        public ICustomerController CustomerController { get; set; }
        public IProductControllerOwner ProductController { get; set; } 
       
        public List<Product> ListedProducts;
        public OwnerUI(OwnerController ownerController, ICustomerController customerController, IProductControllerOwner productController)
        {
            OwnerController = ownerController;
            CustomerController = customerController;
            ProductController = productController;
        }
        public void LoginOwnerMenu(User owner)
        {
            Console.WriteLine(owner.FullName + Strings.loginOwner);
            Strings.Design();
            ListedProducts = ProductController.GetListedProductsByOwner(owner);
            Console.WriteLine(Strings.ownerMenu);
            var input = Console.ReadLine();
            var isValidInput = CheckValidity.IsValidInput(input);

            while (true)
            {
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

            OwnerMenu option = Enum.Parse<OwnerMenu>(input);
            Console.WriteLine(Strings.design);
            Console.WriteLine();
            
            switch (option)
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
                    return;

                default:
                    Console.WriteLine(Strings.invalid);
                    Console.WriteLine();
                    LoginOwnerMenu(owner);
                    break;
            }
        }
        public void AddProduct(User owner)
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

            int categoryProduct;

            while (true)
            {
                bool flag = int.TryParse(Console.ReadLine(), out categoryProduct);

                if (categoryProduct < 1 || categoryProduct > 12)
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
            List<Product> productList = ProductController.GetProductsMasterList();
            while (true)
            {
                if (productList != null && productList.Any((prod) => prod.ProductId == id))
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
                OwnerID = owner.UserID.ToString(),
                OwnerName = owner.FullName,
                ProductCategory = categoryProduct,
                City = owner.City
            };
            try
            {
                if (AddProducts(product,productList))
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
        public bool AddProducts(Product product, List<Product> productList)
        {
            if(productList != null)
            {
                productList.Add(product);
                
            }
            else
            {
                productList = new List<Product>();
                productList.Add(product);
            }

            ProductController.UpdateDBProds(productList);
            return true;
        }
        public void ViewListedProducts(User owner)
        {
            Console.WriteLine();
            if (ListedProducts  !=  null)
            {
                foreach (Product product in ListedProducts)
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
        public void UpdateListedProducts(User owner)
        {
            var productAvailable = ListedProducts.Any((product) => product.OwnerID.Equals(owner.UserID));

            if (!productAvailable)
            {
                Console.WriteLine(Strings.noProd);
                Console.WriteLine(Strings.design);
                return;
            }

            ViewListedProducts(owner);

            Console.WriteLine();
            Console.WriteLine(Strings.prodIdUpdate);
            Console.WriteLine();
            Console.WriteLine();

            var productId = Console.ReadLine();
            bool isValidInput;

            while (true)
            {
                isValidInput = CheckValidity.IsValidInput(productId);

                if (!isValidInput)
                {
                    productId = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine(productId);

            Product updateProduct = ListedProducts.Find((product) => product.ProductId == Convert.ToInt32(productId));

            if (updateProduct.CustomerID != null)
            {
                Console.WriteLine(Strings.cannotUpdateProd);
                return;
            }

            Console.WriteLine(Strings.updateListedProdMenuOptions);

            var input = Console.ReadLine();
            bool isValidOption;

            while (true)
            {
                isValidOption = CheckValidity.IsValidInput(input);

                if (!isValidOption)
                {
                    input = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }

            UpdateProductMenu option = Enum.Parse<UpdateProductMenu>(input);

            Console.WriteLine();

            switch (option)
            {
                case UpdateProductMenu.product_name:
                    Console.WriteLine(Strings.prodUpdateName);
                    string name = Console.ReadLine();
                    Console.WriteLine();
                    while (!CheckValidity.CheckNull(name))
                    {
                        Console.WriteLine(Strings.nameError);
                        name = Console.ReadLine();
                    }
                    updateProduct.ProductName = name;
                    Console.WriteLine(Strings.nameChangedSucc);
                    Console.WriteLine(Strings.design);
                    break;
                case UpdateProductMenu.product_description:
                    Console.WriteLine(Strings.enterNewDesc);
                    string desc = Console.ReadLine();
                    Console.WriteLine();
                    while (!CheckValidity.CheckNull(desc) || desc.Length < 10)
                    {
                        Console.WriteLine(Strings.descError);
                        desc = Console.ReadLine();
                    }
                    updateProduct.Description = desc;
                    Console.WriteLine(Strings.descChangedSucc);
                    Console.WriteLine(Strings.design);
                    break;
                case UpdateProductMenu.product_price:
                    Console.WriteLine(Strings.enterNewPrice);
                    double priceProd;
                    while (true)
                    {
                        bool isValidPrice = double.TryParse(Console.ReadLine(), out priceProd);

                        if (priceProd <= 0)
                        {
                            Console.WriteLine(Strings.prodPriceError);
                        }

                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine();
                    updateProduct.Price = priceProd;
                    Console.WriteLine(Strings.priceChangedSucc);
                    Console.WriteLine(Strings.design);
                    break;

                case UpdateProductMenu.product_category:
                    Console.WriteLine(Strings.enterNewCate);
                    int categoryProd;
                    while (true)
                    {
                        bool isValidCategory = int.TryParse(Console.ReadLine(), out categoryProd);

                        if (categoryProd < 1 || categoryProd > 12)
                        {
                            Console.WriteLine(Strings.invalid);
                        }

                        else
                        {
                            break;
                        }
                    }
                    updateProduct.ProductCategory = categoryProd;
                    Console.WriteLine(Strings.CateChangedSucc);
                    Console.WriteLine(Strings.design);
                    break;

                default:
                    Console.WriteLine(Strings.invalid);
                    break;


            }
            List<Product> productMasterList = ProductController.GetProductsMasterList();
            int index = productMasterList.FindIndex((product) => product.ProductId.Equals(updateProduct.ProductId));
            productMasterList[index] = updateProduct;
            ProductController.UpdateDBProds(productMasterList);

        }
        public void DeleteListedProducts(User owner)
        {
            List<User> list = OwnerController.GetOwnerList();
            
            ViewListedProducts(owner);
            if (ListedProducts == null)
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
                    foreach (var prod in ListedProducts)
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
            List<User> CustomerList = CustomerController.GetCustomer();
            List<Product> MasterListProducts = ProductController.GetProductsMasterList();
            Product isRented = MasterListProducts.Find((product) => product.ProductId == input && product.StartDate != null);
            if (isRented != null)
            {
                Console.WriteLine(Strings.prodDelDenied);
                Console.WriteLine();
                ViewListedProducts(owner);

            }

            foreach (var product in ListedProducts)
            {
                if (product.ProductId.Equals(input))
                {
                    ListedProducts.Remove(product);
                    break;
                }
            }

            foreach (var product in MasterListProducts)
            {
                if (product.ProductId.Equals(input))
                {
                    MasterListProducts.Remove(product);
                    break;
                }
            }
            
            ProductController.UpdateDBProds(MasterListProducts);
            Console.WriteLine(Strings.prodDelSucc);
        }
    }
}
