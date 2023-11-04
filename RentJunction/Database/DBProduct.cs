using Newtonsoft.Json;

using RentJunction.Models;

public sealed class DBProduct : DBHandler
{

    private static DBProduct _instance = null;
    private static readonly object _lockObj = new object();
    public List<Product> _productList { get; set; }
    public List<string> _productCategoryList = new List<string>();
    public static DBProduct Instance
    {
        get
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new DBProduct();
                }
            }
            return _instance;
        }
    }
    private DBProduct()
    {
        _productList = new List<Product>();

        try
        {
            _productList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Message.productsPath));
        }
        catch
        {
            Console.WriteLine(Message.error);
            UI.StartMenu();
        }

    }

    public List<Product> GetProducts(int input, string city)
    {
        List<Product> filteredProd = _productList.FindAll((product) => (product.ProductCategory == input)
        && (product.City == city));
        return filteredProd;
    }
    public List<string> chooseCategory()
    {
        if (_productCategoryList.Count == 0)
        {

            _productCategoryList.Add("1.  Property");
            _productCategoryList.Add("2.  Electronics");
            _productCategoryList.Add("3.  Computer_Accessories");
            _productCategoryList.Add("4.  Audio_Visual");
            _productCategoryList.Add("5.  Security_Systems");
            _productCategoryList.Add("6.  Clothes_Jewellery");
            _productCategoryList.Add("7.  Generator ");
            _productCategoryList.Add("8.  Media_Entertainment_Equipment");
            _productCategoryList.Add("9.  Vehicle");
            _productCategoryList.Add("10. Health_Supplements");
            _productCategoryList.Add("11. Furniture");
            _productCategoryList.Add("12. Miscellaneous");

        }

        return _productCategoryList;


    }
    public bool AddProductMaster(Product product)
    {
        if (_productList != null)
        {
            _productList.Add(product);
        }
        else
        {
           _productList.Add(product);
        }
        DBProduct.Instance.UpdateDB(Message.productsPath,_productList);
        return true;
    }
}

