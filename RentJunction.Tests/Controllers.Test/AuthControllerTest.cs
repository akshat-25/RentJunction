using Moq;
using RentJunction.Models;

namespace RentJunction.Tests
{
    [TestClass]

    public class AuthControllerTest
    {
        [TestMethod]
        public void Register_ValidUser_ReturnTrue() {

            User user = new User()
            {
                FullName = "Test123",
                Username = "testtest123",
                Role = Role.Customer,
                UserID = "da2h2da",
                City = "city1",
                Email = "test12111@gmail.com",
                Password = "Password@111"
            };

            Mock<IDBUsers> mockDBUser = new Mock<IDBUsers>();

            mockDBUser.Setup((user) => user.UserList).Returns(DummyData.dummyUserList);

            IAuthController authController = new AuthController(mockDBUser.Object); 

            bool result = authController.Register(user);

            Assert.IsTrue(result);
        
        }
        [TestMethod]

        public void GetUserUI_ValidUserUI_ReturnTrue()
        {
            Mock<IDBUsers> mockDBUser = new Mock<IDBUsers>();

            mockDBUser.Setup((user) => user.UserList).Returns(DummyData.dummyUserList);

            IAuthController authController = new AuthController(mockDBUser.Object);

            bool result = authController.GetUserUI("user123", "User@1234");

            Assert.IsTrue(result);

        }
    }
}