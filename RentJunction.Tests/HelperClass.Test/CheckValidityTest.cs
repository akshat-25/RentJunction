using System.Diagnostics;

namespace RentJunction.Tests

{
    [TestClass]
    public class CheckValidityTest
    {

        [TestMethod]
        public void IsValidPassword_ValidPassword_ReturnTrue()
        {
            var password = "Password@123";

            bool result = CheckValidity.IsValidPassword(password);

            Assert.IsTrue(result);

        } 

        [TestMethod]
        public void IsValidPassword_InvalidPassword_ReturnFalse()
        {
            var password = "password123";

            bool result = CheckValidity.IsValidPassword(password);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsValidEmail_ValidEmail_ReturnTrue()
        {
            var email = "test@gmail.com";

            bool result = CheckValidity.IsValidEmail(email);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsValidEmail_InvalidEmail_ReturnFalse()
        {
            var email = "testgmail.com";

            bool result = CheckValidity.IsValidEmail(email);

            Assert.IsFalse(result);

        }
        [TestMethod]
        public void IsValidPhoneNum_ValidPhoneNumber_ReturnTrue()
        {
            var phoneNumber = "8818181818";

            bool result = CheckValidity.IsValidPhoneNum(phoneNumber);

            Assert.IsTrue(result);

        }
        [TestMethod]
        public void IsValidUsername_ValidUsername_ReturnTrue()
        {
            var username = "test123";

            bool result = CheckValidity.IsValidUsername(username);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsValidUsername_InvalidUsername_ReturnFalse()
        {
            var username = "test";

            bool result = CheckValidity.IsValidUsername(username);

            Assert.IsFalse(result);

        }
        
        [TestMethod]
        public void IsValidAddress_ValidAddress_ReturnTrue()
        {
            var city = "Jaipur";

            bool result = CheckValidity.IsValidAddress(city);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsValidAddress_EmptyAddress_ReturnFalse()
        {
            var city = "";

            bool result = CheckValidity.IsValidAddress(city);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsValidAddress_NumericAddress_ReturnFalse()
        {
            var city = "123";

            bool result = CheckValidity.IsValidAddress(city);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsValidAddress_InvalidAddress_ReturnFalse()
        {
            var city = "Jaipur123";

            bool result = CheckValidity.IsValidAddress(city);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void IsValidInput_ValidInput_ReturnTrue()
        {
            var input = "11";

            bool result = CheckValidity.IsValidInput(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidInput_EmptyInput_ReturnFalse()
        {
            var input = "";

            bool result = CheckValidity.IsValidInput(input);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void IsValidInput_NonIntegerInput_ReturnFalse()
        {
            var input = "abc";

            bool result = CheckValidity.IsValidInput(input);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void IsValidName_ValidName_ReturnTrue()
        {
            var input = "testname";

            bool result = CheckValidity.IsValidName(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidName_IntergerName_ReturnFalse()
        {
            var input = "123";

            bool result = CheckValidity.IsValidName(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidName_AlphaNumbericName_ReturnFalse()
        {
            var input = "test123";

            bool result = CheckValidity.IsValidName(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidName_EmptyName_ReturnFalse()
        {
            var input = "";

            bool result = CheckValidity.IsValidName(input);

            Assert.IsFalse(result);
        }

        [TestMethod]

        public void IsValidRole_ValidRole_ReturnTrue()
        {
            var input = (int)Role.Owner;

            bool result = CheckValidity.IsValidRole(input.ToString());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidRole_InvalidRole_ReturnFalse()
        {
            var input = 11;

            bool result = CheckValidity.IsValidRole(input.ToString());

            Assert.IsFalse(result);
        }

        [TestMethod]

        public void CheckNull_ValidInput_ReturnTrue()
        {
            var input = "test";

            bool result = CheckValidity.CheckNull(input);

            Assert.IsTrue(result);

        }

        [TestMethod]

        public void CheckNull_InvalidInput_ReturnFalse()
        {
            var input = "";

            bool result = CheckValidity.CheckNull(input);

            Assert.IsFalse(result);

        }


        [TestMethod]

        public void IsValidRole_ValidInput_ReturnTrue()
        {
            var input = (int)Role.Customer;

            bool result = CheckValidity.IsValidRole(input.ToString()); 

            Assert.IsTrue(result);
        }
        [TestMethod]

        public void IsValidRole_InvalidInput_ReturnFalse()
        {
            int input = 5;

            bool result = CheckValidity.IsValidRole(input.ToString()); 

            Assert.IsFalse(result);
        }
    }
}