using Moq;
using RentJunction.Models;

namespace RentJunction.Tests
{
    [TestClass]
    public class OwnerControllerTest
    {
        [TestMethod]
        public void GetOwner_ShouldReturnList()
        {
         
            var mockDBOwner = new Mock<IDBUsers>();

            mockDBOwner.Setup((owner) => owner.UserList).Returns(DummyData.dummyOwnerList);

            IOwnerController ownerController = new OwnerController(mockDBOwner.Object);

            List<User> owners = ownerController.GetOwnerList();

            Assert.IsNotNull(owners);
            Assert.AreEqual(2, owners.Count);
            Assert.IsTrue(owners.TrueForAll(owner => owner.Role == Role.Owner));
        }
    }
}