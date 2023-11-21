using Moq;
using RentJunction.Models;

namespace RentJunction.Tests
{
    [TestClass]

    public class ProductControllerTest
    {
        [TestMethod]
        public void GetMasterListGetProductsMasterList_ShouldReturnList()
        {
            var mockDBProduct = new Mock<IDBProduct>();
                    
            mockDBProduct.Setup((product) => product.ProductList).Returns(DummyData.dummyProductList);

            IProductControllerCust productController = new ProductController(mockDBProduct.Object);

            List<Product> productList = productController.GetProductsMasterList();

            Assert.IsNotNull(productList);
            Assert.AreEqual(3, productList.Count);
            Assert.IsTrue(productList.TrueForAll(product => product.ProductId != null));
        }

        [TestMethod]

        public void AddProductMaster_ReturnTrue()
        {
            Product dummyProduct = new Product()
            {
                ProductId = 4,
                ProductName = "Product 4",
                City = "City 4"
            };

            var mockDBProduct = new Mock<IDBProduct>();

            mockDBProduct.Setup((product) => product.AddProductMaster(dummyProduct)).Returns(true);

            IProductControllerOwner productController = new ProductController(mockDBProduct.Object);

            bool result = productController.AddProductMaster(dummyProduct);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetListedProductByOwner_ShouldReturnList()
        {

            User owner = new User()
            {
                UserID = "ce8un3y"
            };
            var mockDBProduct = new Mock<IDBProduct>();

            mockDBProduct.Setup((product) => product.ProductList).Returns(DummyData.GetListedProductByOwner);

            IProductControllerOwner productController = new ProductController(mockDBProduct.Object);
            
            List<Product> productList =  productController.GetListedProductsByOwner(owner);

            Assert.IsTrue(productList.TrueForAll((product) => product.OwnerID == owner.UserID));

        }

        [TestMethod]
        public void GetntedProductByCustomer_ShouldReturnList()
        {

            User customer = new User()
            {
                UserID = "ce8un3y"
            };
            var mockDBProduct = new Mock<IDBProduct>();

            mockDBProduct.Setup((product) => product.ProductList).Returns(DummyData.GetRentedProductByCustomer);

            IProductControllerCust productController = new ProductController(mockDBProduct.Object);

            List<Product> productList = productController.GetRentedProductsByCustomer(customer);

            Assert.IsTrue(productList.TrueForAll((product) => product.CustomerID == customer.UserID));

        }

        [TestMethod]
        public void GetProducts_ShouldReturnList()
        {    
            var mockDBProduct = new Mock<IDBProduct>();

            mockDBProduct.Setup((product) => product.GetProducts(1, "Test")).Returns(DummyData.GetProducts);

            IProductControllerCust productController = new ProductController(mockDBProduct.Object);

            List<Product> productList = productController.GetProducts(1,"Test");

            Assert.IsTrue(productList.TrueForAll((prod) => (prod.ProductCategory == 1) && (prod.City == "Test")));
            Assert.IsNotNull(productList);

        }

        [TestMethod]
        public void ChooseCategory_ShouldReturnList()
        {
            var mockDBProduct = new Mock<IDBProduct>();

            mockDBProduct.Setup((product) => product.chooseCategory()).Returns(DummyData.category);

            IProductControllerCust productController = new ProductController(mockDBProduct.Object);

            List<string> categories = productController.ChooseCategory();

            Assert.AreEqual(12, categories.Count);
            Assert.IsNotNull(categories);

        }

        [TestMethod]
        public void UpdateDBProds_ShouldAddToDatabase()
        {
            var mockDBUpdate = new Mock<IDBProduct>();

            mockDBUpdate.Setup((user) => user.UpdateDB(Strings.productsPath, DummyData.dummyProductList));

            IProductControllerOwner productController = new ProductController(mockDBUpdate.Object);

            Product product = DummyData.dummyProductList[1];

            product.ProductName = "Updated Name";

            productController.UpdateDBProds(DummyData.dummyProductList);

            List<Product> productList = DummyData.dummyProductList;

            Assert.IsTrue(productList[1].ProductName == "Updated Name");
        }
    }
}