using NuGet.ContentModel;
using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
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
    public class TestAdminService
    {

        // Test authentication admin
        [Fact]
        public void Test_AuthenticateAdmin_ValidAccount()
        {
            // Arrange
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.UserName = "admin";
            loginViewModel.Password = Functions.MD5Hash("admin");

            AdminService adminService = new AdminService();

            // Act
            var result = adminService.AuthenticateAdmin(loginViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AdminViewModel>(result);
        }        
        [Fact]
        public void Test_AuthenticateAdmin_InvalidAccount()
        {
            // Arrange
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.UserName = "admin";
            loginViewModel.Password = Functions.MD5Hash("errorpassword");

            AdminService adminService = new AdminService();

            // Act
            var result = adminService.AuthenticateAdmin(loginViewModel);

            // Assert
            Assert.Null(result);
        }

        // Test create new admin entry
        [Fact]
        public void Test_CreateNewAdmin_Invalid()
        {
            // Arrange
            AdminViewModel adminViewModel = new AdminViewModel()
            {
                UserName = "admin",
                Password = "test password",
                FirstName = "test",
                LastName = "test",
                Email = "test",
                Phone = "123",
                Birthday = DateTime.Now,
                Gender = "Men",
                Avatar = "test",
                RegisteredDate = DateTime.Now,
                RoleId = 1
            };

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.CreateAdmin(adminViewModel);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Test_CreateNewAdmin_Valid()
        {
            // Arrange
            AdminViewModel adminViewModel = new AdminViewModel()
            {
                UserName = "test not exits username",
                Password = "test password",
                FirstName = "test",
                LastName = "test",
                Email = "test",
                Phone = "123",
                Birthday = DateTime.Now,
                Gender = "Men",
                Avatar = "test",
                RegisteredDate = DateTime.Now,
                RoleId = 1
            };

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.CreateAdmin(adminViewModel);

            // Assert
            Assert.True(result);
        }



        // Test update new admin entry
        [Fact]
        public void Test_CheckExistUserName_NotExistUsername()
        {
            // Arrange
            string username = "test";

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.CheckExistUserName(username);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void Test_CheckExistUserName_ExistUsername()
        {
            // Arrange
            string username = "admin";

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.CheckExistUserName(username);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void Test_UpdateAdmin_InvalidAdminId()
        {
            // Arrange
            int adminId = 6000;

            AdminViewModel adminViewModel = new AdminViewModel()
            {
                FirstName = "test",
                LastName = "test",
                Email = "test",
                Phone = "123",
                Birthday = DateTime.Now,
                Gender = "Men",
                Avatar = "test",
                RoleId = 1
            };

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.UpdateAdmin(adminId, adminViewModel);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void Test_UpdateAdmin_ValidAdminId()
        {
            // Arrange
            int adminId = 6;

            AdminViewModel adminViewModel = new AdminViewModel()
            {
                FirstName = "test update",
                LastName = "test update",
                Email = "test update",
                Phone = "123 update",
                Birthday = DateTime.Now,
                Gender = "Men update",
                Avatar = "test update",
                RoleId = 1
            };

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.UpdateAdmin(adminId, adminViewModel);

            // Assert
            Assert.True(result);
        }


        // Test delete admin entry
        [Fact]
        public void Test_DeleteAdmin_InvalidId()
        {
            // Arrange
            int adminId = 60;

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.DeleteAdmin(adminId);

            // Assert
            Assert.False(result);
        }        
        [Fact]
        public void Test_DeleteAdmin_ValidId()
        {
            // Arrange
            int adminId = 6;

            AdminService adminService = new AdminService();

            // Act
            bool result = adminService.DeleteAdmin(adminId);

            // Assert
            Assert.True(result);
        }
    
    
    }
}
