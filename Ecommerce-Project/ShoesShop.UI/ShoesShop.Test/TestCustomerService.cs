using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test
{
    public  class TestCustomerService
    {
        // Customer Service
        [Fact]
        public void Test_CustomerLogin_ValidAccount()
        {
            // Arrange
            CustomerService customerService = new CustomerService();
            string Email = "knguyn16@gmail.com";
            string Password = "123123";

            Password = Functions.MD5Hash(Password);

            // Act
            var result = customerService.ValidateCustomerAccount(Email, Password);

            // Assert
            Assert.NotNull(result.Email);
            Assert.IsType<Customer>(result);
        }
        [Fact]
        public void Test_CustomerLogin_InvalidAccount()
        {
            // Arrange
            CustomerService customerService = new CustomerService();
            string Email = "testemail";
            string Password = "testpassword";

            // Act
            var result = customerService.ValidateCustomerAccount(Email, Password);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void Test_RegistrationAccount_CheckExistEmail()
        {
            // Arrange
            CustomerService customerService = new CustomerService();
            string exitEmail = "knguyn16@gmail.com";

            // Act
            var result = customerService.CheckExistEmailOfCustomer(exitEmail);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_RegistrationAccount_CheckNotxistEmail()
        {
            // Arrange
            CustomerService customerService = new CustomerService();
            string exitEmail = "testingemail@gmail.com";

            // Act
            var result = customerService.CheckExistEmailOfCustomer(exitEmail);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Test_RegistrationAccount_ValidAccount()
        {
            // Arrange
            CustomerService customerService = new CustomerService();
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Email = "newEmail123@gmail.com", // Email not exist in Customer entity
                FirstName = "test first name",
                LastName = "test last name",
                Password = "123",
                RegisterDate = DateTime.Now,
                Avatar = "avatar.jpg",
            };

            // Act
            var result = customerService.CreateCustomer(customerViewModel);

            // Assert
            Assert.True(result); // result return true if create success customer account

        }
        [Fact]
        public void Test_RegistrationAccount_ExistEmailOfAccount()
        {
            // Arrange
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Email = "knguyn16@gmail.com", // Email exist in Customer entity
                FirstName = "test first name",
                LastName = "test last name",
                Password = "123",
                RegisterDate = DateTime.Now,
                Avatar = "avatar.jpg",
            };

            CustomerService customerService = new CustomerService();

            // Act
            var result = customerService.CreateCustomer(customerViewModel);


            // Assert
            Assert.False(result); // result return false if have error
        }



        // Address Service
        [Fact]
        public void Test_CreateNewAddress()
        {
            // Arrange
            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel()
            {
                CustomerId = 1,
                FirstName = "test",
                LastName = "test",
                Address = "test address",
                Phone = "123"
            };
            bool isDefault = true;

            CustomerAddressService customerAddressService = new CustomerAddressService();

            // Act
            var result = customerAddressService.CreateAddressOfCustomer(customerAddressViewModel, isDefault);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void Test_UpdateAddress_InvalidAddressId()
        {
            // Arrange
            int customerAddressId = -1;// Invalid id
            bool isDefault = true;

            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel()
            {
                CustomerId = 1,
                FirstName = "test",
                LastName = "test",
                Address = "test address",
                Phone = "123"
            };

            CustomerAddressService customerAddressService = new CustomerAddressService();

            // Act
            var result = customerAddressService.UpdateAddressOfCustomer(customerAddressId, customerAddressViewModel, isDefault);

            //Assert
            Assert.False(result);
        }
        [Fact]
        public void Test_UpdateAddress_ValidAddressId()
        {
            // Arrange
            int customerAddressId = 7;// Valid id
            bool isDefault = true;

            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel()
            {
                CustomerId = 1,
                FirstName = "test",
                LastName = "test",
                Address = "test address",
                Phone = "123"
            };

            CustomerAddressService customerAddressService = new CustomerAddressService();

            // Act
            var result = customerAddressService.UpdateAddressOfCustomer(customerAddressId, customerAddressViewModel, isDefault);

            //Assert
            Assert.True(result);
        }
    }
}
