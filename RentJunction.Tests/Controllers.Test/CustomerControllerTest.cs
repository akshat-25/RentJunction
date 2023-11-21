using Moq;
using RentJunction.Models;

namespace RentJunction.Tests
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void GetCustomer_ShouldReturnList()
        {
            

            //Arrange
            var mockDBCustomer = new Mock<IDBUsers>();

            mockDBCustomer.Setup((customer) => customer.UserList).Returns(DummyData.dummyCustomerList);
            
            ICustomerController customerController = new CustomerController(mockDBCustomer.Object);


            //Act
            List<User> customers = customerController.GetCustomer();

            //Assert
            Assert.IsNotNull(customers);
            Assert.AreEqual(2, customers.Count);
            Assert.IsTrue(customers.TrueForAll(cust => cust.Role == Role.Customer));
        }
    }
}