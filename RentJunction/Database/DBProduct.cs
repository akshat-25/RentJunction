using Newtonsoft.Json;

using RentJunction.Models;

public sealed class DBProduct : DBHandler , IDBHandler
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
            _productList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Strings.productsPath));
        }
        catch(Exception ex)
        {
            Console.WriteLine(Strings.error);
            File.AppendAllText(Strings.errorLoggerPath, ex.ToString());
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
            _productCategoryList.Add(Strings.cate1);
            _productCategoryList.Add(Strings.cate2);
            _productCategoryList.Add(Strings.cate3);
            _productCategoryList.Add(Strings.cate4);
            _productCategoryList.Add(Strings.cate5);
            _productCategoryList.Add(Strings.cate6);
            _productCategoryList.Add(Strings.cate7);
            _productCategoryList.Add(Strings.cate8);
            _productCategoryList.Add(Strings.cate9);
            _productCategoryList.Add(Strings.cate10);
            _productCategoryList.Add(Strings.cate11);
            _productCategoryList.Add(Strings.cate12);

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
        DBProduct.Instance.UpdateDB(Strings.productsPath,_productList);
        return true;
    }
}

