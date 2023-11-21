using Newtonsoft.Json;
using RentJunction.Models;

public sealed class DBProduct : DBHandler, IDBProduct
{
    private static DBProduct _instance = null;

    private static readonly object _lockObj = new object();
    public List<Product> ProductList { get; set; }

    public List<string> _productCategoryList = new List<string>();
    public static DBProduct Instance
    {
        get
        {
            lock (_lockObj)            {

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
        ProductList = new List<Product>();

        try
        {
            ProductList = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(Strings.productsPath));
        }
        catch(Exception ex)
        {
            Console.WriteLine(Strings.error);
            File.AppendAllText(Strings.errorLoggerPath, ex.ToString());
            return;
        }

    }

    public List<Product> GetProducts(int input, string city)
    {
        List<Product> filteredProd = ProductList.FindAll((product) => (product.ProductCategory == input)
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
        ProductList.Add(product);  
        DBProduct.Instance.UpdateDB(Strings.productsPath, ProductList);
        return true;
    }
}

