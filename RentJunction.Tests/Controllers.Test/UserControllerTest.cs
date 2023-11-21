using Moq;
using NUnit.Framework.Constraints;
using RentJunction.Models;

namespace RentJunction.Tests
{
    [TestClass]
    
    public class UserControllerTest
    {
        [TestMethod]

        public void GetUserList_ShouldReturnList()
        {
            var mockDBUser = new Mock<IDBUsers>();

            mockDBUser.Setup((user) => user.UserList).Returns(DummyData.dummyUserList);

            IUserController userController = new UserController(mockDBUser.Object);

            List<User> userList = userController.GetUserMasterList();

            Assert.IsNotNull(userList);
            Assert.AreEqual(4, userList.Count);
            Assert.IsTrue(userList.TrueForAll(user => user.Role == Role.Customer || user.Role == Role.Owner || user.Role == Role.Admin));
        }

        [TestMethod]
        public void UpdateDBUser_ShouldAddToDatabase()
        {
            var mockDBUpdate = new Mock<IDBUsers>();

            mockDBUpdate.Setup((user) => user.UpdateDB(Strings.userPath,DummyData.dummyUserList));

            IUserController userController = new UserController(mockDBUpdate.Object);

            User user = DummyData.dummyUserList[1];

            user.FullName = "Updated User";

            userController.UpdateDBUser(DummyData.dummyUserList);

            List<User> userList = DummyData.dummyUserList;

            Assert.IsTrue(userList[1].FullName == "Updated User");
        }
    }
}