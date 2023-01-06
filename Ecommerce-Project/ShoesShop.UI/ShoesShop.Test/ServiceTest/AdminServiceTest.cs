using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Service;
using ShoesShop.Test.TestData;
using AutoMapper;
using ShoesShop.API.Mapper;
using ShoesShop.DTO;
using ShoesShop.DTO.Admin;

namespace ShoesShop.Test.ServiceTest
{
    public class AdminServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Admin> _admins;
        private readonly List<Role> _roles;
        private readonly List<Activity> _activities;
        private readonly IMapper _mapper;
        public AdminServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminTestDB").Options;
            _context = new ApplicationDbContext(_options);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AdminMapper())).CreateMapper();
            _admins = AdminTestData.GetAdmins();
            _context.Admins.AddRange(_admins);
            _roles = RoleTestData.GetRoles();
            _context.Roles.AddRange(_roles);
            _activities = ActivityTestData.GetActivities();
            _context.Activities.AddRange(_activities);
            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }
        [Fact]
        public void GetAdmins_ShouldReturnListAdminDTO()
        {
            //Arrange
            List<AdminViewModel> listAdmin = _mapper.Map<List<AdminViewModel>>(AdminTestData.GetAdmins());
            AdminService adminService = new AdminService(_context, _mapper);


            // Act
            var result = adminService.GetAllAdmin();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<AdminViewModel>>(result);
            Assert.Equal(listAdmin.Count, result.Count); // Check total item in list compare fake list
        }

        [Fact]
        public void GetAdmin_ValidAdminId_ShouldReturnAdminDTO()
        {
            //Arrange
            int adminId = 1;
            AdminViewModel admin = _mapper.Map<AdminViewModel>(AdminTestData.GetAdmins().FirstOrDefault(m => m.AdminId == adminId));
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.GetAdminById(adminId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AdminViewModel>(result);
            Assert.Equal(admin.AdminId, result.AdminId);
        }
        [Fact]
        public void GetAdmin_InvalidAdminId_ShouldReturnNull()
        {
            //Arrange
            int adminId = 10;
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.GetAdminById(adminId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateAdmin_ValidUsername_ShouldReturnAdminDTO()
        {
            //Arrange
            AdminViewModel adminDTO = new AdminViewModel()
            {
                UserName = "Test",
                Password = "123",
                FirstName = "Nguyen",
                LastName = "A",
                Email = "Test",
                Phone = "Test",
                Birthday = DateTime.Now,
                RegisteredDate = DateTime.Now,
                Gender = "Men",
                RoleName = "Admin"
            };
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.CreateAdmin(adminDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AdminViewModel>(result);
            Assert.Equal(adminDTO.UserName, result.UserName);
        }
        [Fact]
        public void CreateAdmin_ExistedUsername_ShouldReturnNull()
        {
            //Arrange
            AdminViewModel adminDTO = new AdminViewModel()
            {
                UserName = "UserA", // Existed username
                Password = "123",
                FirstName = "Nguyen",
                LastName = "A",
                Email = "Test",
                Phone = "Test",
                Birthday = DateTime.Now,
                RegisteredDate = DateTime.Now,
                Gender = "Men",
                RoleName = "Admin"
            };
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.CreateAdmin(adminDTO);

            // Assert
            Assert.IsType<AdminViewModel>(result);
            Assert.True(result.IsExistedUsername);
        }
        [Fact]

        public void UpdateAdmin_ValidAdminId_ShouldReturnAdminDTO()
        {
            //Arrange
            int adminId = 1;
            var adminEdit = AdminTestData.GetAdmins().FirstOrDefault(m => m.AdminId == adminId); // Role admin
            AdminViewModel adminDTO = new AdminViewModel()
            {
                FirstName = "Nguyen",
                LastName = "Update",
                Email = "Test",
                Phone = "Test",
                Birthday = DateTime.Now,
                RegisteredDate = DateTime.Now,
                Gender = "Men",
                RoleName = "Employee" // Change role employee
            };
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.UpdateAdmin(adminId, adminDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AdminViewModel>(result);
            Assert.NotEqual(adminEdit.RoleId, result.RoleId); // Compare value before update vs after update must different
        }
        [Fact]
        public void UpdateAdmin_InvalidAdminId_ShouldReturnNull()
        {
            //Arrange
            int adminId = 10; // Invalid Admin Id
            AdminViewModel adminDTO = new AdminViewModel()
            {
                FirstName = "Nguyen",
                LastName = "Update",
                Email = "Test",
                Phone = "Test",
                Birthday = DateTime.Now,
                RegisteredDate = DateTime.Now,
                Gender = "Men",
                RoleName = "Employee"
            };
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.UpdateAdmin(adminId, adminDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DisabledAdmin_ValidAdminId_ShouldReturnAdminDTOHaveStatusFalse()
        {
            //Arrange
            int adminId = 1;
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.DisabledAdmin(adminId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AdminViewModel>(result);
            Assert.False(result.Status);
        }
        [Fact]
        public void DisabledAdmin_InvalidAdminId_ShouldReturnNull()
        {
            //Arrange
            int adminId = 10;
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.DisabledAdmin(adminId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AuthenticateAdmin_ValidAccountLogin_ShouldReturnAdminDTO()
        {
            // Arrange
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                UserName = "UserA",
                Password = "21232f297a57a5a743894a0e4a801fc3"
            };
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.AuthenticateAdmin(loginViewModel);

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void AuthenticateAdmin_InvalidAccountLogin_ShouldReturnNull()
        {
            // Arrange
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                UserName = "Test", // Invalid account
                Password = "21232f297a57a5a743894a0e4a801fc3"
            };
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.AuthenticateAdmin(loginViewModel);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(null, null, null, null, null, 1, 2)]
        [InlineData(null, null, null, "id", "asc", 1, 2)]
        [InlineData(null, null, null, "userName", "asc", 1, 2)]
        [InlineData(null, null, null, "lastName", "asc", 1, 2)]
        [InlineData(null, null, null, "registerDate", "asc", 1, 2)]
        [InlineData(null, null, null, "role", "asc", 1, 2)]
        [InlineData(null, null, null, "id", "desc", 1, 2)]
        [InlineData(null, null, null, "userName", "desc", 1, 2)]
        [InlineData(null, null, null, "lastName", "desc", 1, 2)]
        [InlineData(null, null, null, "registerDate", "desc", 1, 2)]
        [InlineData(null, null, null, "role", "desc", 1, 2)]
        [InlineData(null, null, "UserA", "id", "desc", 1, 2)]
        [InlineData(null, null, "UserA", "userName", "desc", 1, 2)]
        [InlineData(null, null, "UserA", "lastName", "desc", 1, 2)]
        [InlineData(null, null, "UserA", "registerDate", "desc", 1, 2)]
        [InlineData(null, null, "UserA", "role", "desc", 1, 2)]
        [InlineData(null, "2000-01-01", "UserA", "id", "desc", 1, 2)]
        [InlineData("Admin", null, "UserA", "id", "desc", 1, 2)]
        public void GetAllAdminPaging_TotalItemShouldBeMoreThanZero(string? filterByRole, string? filterByDate, string? searchString, string? fieldName, string? sortType, int page, int limit)
        {
            //Arrange
            DateTime dateformat = new DateTime();
            if (filterByDate != null)
            {
                dateformat = DateTime.ParseExact(filterByDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.GetAllAdminPaging(filterByRole, filterByDate != null ? dateformat : null, searchString, fieldName, sortType, page, limit);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AdminPagingViewModel>(result);
            Assert.True(result.TotalItem > 0);
        }

        [Fact]
        public void SaveActivity_ShouldReturnActivity()
        {
            //Arrange
            Activity activity = new Activity()
            {
                AdminId = 1,
                ActivityType = "Checked order",
                ObjectType = "Order",
                ObjectName = "HD123",
                Time = new DateTime(2000, 01, 01)
            };
            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.SaveActivity(activity.AdminId, activity.ActivityType,activity.ObjectType, activity.ObjectName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Activity>(result);
        }

        [Theory]
        [InlineData(1, null, null)]
        [InlineData(1, "Product", null)]
        [InlineData(1, "Product,Order", null)]
        [InlineData(1, "Product,Order", "2000-01-01")]
        public void GetActivitiesOfAdmin_ShouldReturnActivityList(int adminId, string objectType, string time)
        {
            //Arrange
            DateTime dateformat = new DateTime();
            if (time != null)
            {
                dateformat = DateTime.ParseExact(time, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            AdminService adminService = new AdminService(_context, _mapper);

            // Act
            var result = adminService.GetActivitiesOfAdmin(adminId, objectType, time != null ? dateformat : null);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Activity>>(result);
            Assert.True(result.Count > 0);
        }
    }
}
