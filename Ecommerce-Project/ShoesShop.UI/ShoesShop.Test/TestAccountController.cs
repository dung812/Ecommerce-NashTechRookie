using Microsoft.AspNetCore.Mvc;
using ShoesShop.Service;
using ShoesShop.DTO;
using ShoesShop.UI.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Win32;
using ShoesShop.Domain;

namespace ShoesShop.Test
{
    public class TestAccountController
    {
        // Service
        [Fact]
        public void Test_CustomerLogin_ValidAccount()
        {
            // Arrange
            CustomerService customerService = new CustomerService();
            string Email = "knguyn16@gmail.com";
            string Password = "4297f44b13955235245b2497399d7a93";

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
            Assert.Null(result.Email);
        }
        
        [Fact]
        public void Test_RegistrationAccount_ValidAccount()
        {
            // Arrange
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Email = "notExistEmail@gmail.com", // Email not exist in Customer entity
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

        [Fact]
        public void Test_CreateNewAddress()
        {
            // Arrange
            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel()
            {
                CustomerId = 1,
                FirstName = "test",
                LastName =  "test",
                Address = "test address",
                Phone = "123"
            };            
            bool isDefault = true;

            CustomerAddressService customerService = new CustomerAddressService();

            // Act
            var result = customerService.CreateAddressOfCustomer(customerAddressViewModel, isDefault);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_UpdateAddress_InvalidAddressId()
        {
            // Arrange
            int customerAddressId = - 1;// Invalid id
            bool isDefault = true;

            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel()
            {
                CustomerId = 1,
                FirstName = "test",
                LastName = "test",
                Address = "test address",
                Phone = "123"
            };

            CustomerAddressService customerService = new CustomerAddressService();

            // Act
            var result = customerService.UpdateAddressOfCustomer(customerAddressId, customerAddressViewModel, isDefault);

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

            CustomerAddressService customerService = new CustomerAddressService();

            // Act
            var result = customerService.UpdateAddressOfCustomer(customerAddressId, customerAddressViewModel, isDefault);

            //Assert
            Assert.True(result);
        }
    }

}
