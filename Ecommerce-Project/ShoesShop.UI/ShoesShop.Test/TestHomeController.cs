using Microsoft.AspNetCore.Mvc;
using ShoesShop.Service;
using ShoesShop.DTO;
using ShoesShop.UI.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Win32;

namespace ShoesShop.Test
{
    public class TestHomeController
    {
        // Test Service
        [Fact]
        public void Test_ProductService_GetListProduct()
        {
            List<ProductViewModel> products = new List<ProductViewModel>()
            {
                new ProductViewModel(),
                new ProductViewModel()
            };

            // Arrange
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(x => x.GetAllProduct())
                    .Returns(products);

            ProductService productService = new ProductService();


            // Act
            var result = productService.GetAllProduct();

            // Assert
            Assert.IsType<List<ProductViewModel>>(result); // Check type
            //Assert.Equal(products.Count, result.Count); // Check quantity list item

        }


        // Test Action
        [Fact]
        public void Test_Contact_GET_ReturnsViewResultNullModel()
        {
            // Arrange
            IContactService context = null;
            var controller = new HomeController(null, null, context);

            // Act
            var result = controller.Contact();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);

            //This test is done because the Contact action method does not use IContactService.
        }

        [Fact]
        public void Test_Contact_POST_InvalidModelState()
        {
            // Arrange
            ContactViewModel contactViewModel = new ContactViewModel()
            {
                Name = "test name",
                Email = "test email",
                Subject = "test subject",
                Message = "test message",
                DateContact = DateTime.Now
            };

            var mockRepo = new Mock<IContactService>();
            mockRepo.Setup(repo => repo.Create(It.IsAny<ContactViewModel>()));
            var controller = new HomeController(null, null, mockRepo.Object);

            controller.ModelState.AddModelError("Name", "Name is required");

            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // Act
            var result = controller.Contact(contactViewModel);



            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            mockRepo.Verify();
        }

        [Fact]
        public void Test_Contact_POST_ValidModelState()
        {
            // Arrange
            ContactViewModel contactViewModel = new ContactViewModel()
            {
                Name = "test name",
                Email = "test email",
                Subject = "test subject",
                Message = "test message",
                DateContact = DateTime.Now
            };

            var mockRepo = new Mock<IContactService>();
            mockRepo.Setup(repo => repo.Create(It.IsAny<ContactViewModel>()))
                .Verifiable();
            var controller = new HomeController(null, null, mockRepo.Object);

            var mockTempData = new Mock<ITempDataDictionary>();
            controller.TempData = mockTempData.Object;

            // Act
            var result = controller.Contact(contactViewModel);

            // Assert
            /*
                3 conditions are tested:
                    - The action is redirecting to another action.
                    - The redirected controller is null.
                    - The redirected action method “Index”.
             */
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
    }
}
