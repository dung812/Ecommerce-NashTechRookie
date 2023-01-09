using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.DTO.Admin;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShoesShop.Service
{
    public interface IAdminService
    {
        public List<AdminViewModel> GetAllAdmin();
        public AdminViewModel GetAdminById(int adminId);
        public AdminViewModel AuthenticateAdmin(LoginViewModel loginViewModel);
        public bool CheckExistUserName(string username);
        public AdminViewModel CreateAdmin(AdminViewModel adminViewModel);
        public AdminViewModel UpdateAdmin(int adminId, AdminViewModel adminViewModel);
        public AdminViewModel DisabledAdmin(int adminId);
        public AdminPagingViewModel GetAllAdminPaging(string? filterByRole, DateTime? filterByDate, string? searchString, string? fieldName, string? sortType, int page, int limit);
        public Activity SaveActivity(int? adminId, string action, string objectType, string objectName);
        public List<Activity> GetActivitiesOfAdmin(int? adminId, string? objectType, DateTime? time);
    }
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AdminViewModel> GetAllAdmin()
        {
            var admins = _context.Admins
                .Where(m => m.Status)
                .Include(m => m.Role)
                .ToList();

            var adminDTO = _mapper.Map<List<AdminViewModel>>(admins);
            return adminDTO;
        }
        public AdminPagingViewModel GetAllAdminPaging(string? filterByRole, DateTime? filterByDate, string? searchString, string? fieldName, string? sortType, int page, int limit)
        {
            // Query data
            var query = _context.Admins
                        .Include(m => m.Role)
                        .Where(m => m.Status);

            // search
            if (!string.IsNullOrEmpty(searchString))
            {
                // username, lastname
                query = query.Where(m =>
                    (m.UserName.Replace(" ", "").ToUpper().Contains(searchString.Replace(" ", "").ToUpper())) ||
                    (m.LastName.Replace(" ", "").ToUpper().Contains(searchString.Replace(" ", "").ToUpper()))
                );
            }

            // Filter by role
            if (!string.IsNullOrEmpty(filterByRole))
            {
                if (filterByRole != "All")
                {
                    switch (filterByRole)
                    {
                        case "Admin":
                            query = query.Where(m => m.RoleId == 1);
                            break;
                        case "Employee":
                            query = query.Where(m => m.RoleId == 2);
                            break;
                    }

                }
            }

            // Filter by date
            if (filterByDate != null)
            {
                query = query.Where(m => m.RegisteredDate.Date == filterByDate.Value.Date);
            }

            // Sort
            if (!string.IsNullOrEmpty(fieldName))
            {
                if (sortType == "asc")
                {
                    switch (fieldName)
                    {
                        case "id":
                            query = query.OrderBy(m => m.AdminId);
                            break;
                        case "userName":
                            query = query.OrderBy(m => m.UserName);
                            break;
                        case "lastName":
                            query = query.OrderBy(m => m.LastName);
                            break;
                        case "registerDate":
                            query = query.OrderBy(m => m.RegisteredDate);
                            break;
                        case "role":
                            query = query.OrderBy(m => m.Role.RoleName);
                            break;
                    }
                }
                else if (sortType == "desc")
                {
                    switch (fieldName)
                    {
                        case "id":
                            query = query.OrderByDescending(m => m.AdminId);
                            break;
                        case "userName":
                            query = query.OrderByDescending(m => m.UserName);
                            break;
                        case "lastName":
                            query = query.OrderByDescending(m => m.LastName);
                            break;
                        case "registerDate":
                            query = query.OrderByDescending(m => m.RegisteredDate);
                            break;
                        case "role":
                            query = query.OrderByDescending(m => m.Role.RoleName);
                            break;
                    }
                }
            }

            int total = query.Count();

            query = query.Skip((page - 1) * limit).Take(limit);

            var adminDTO = _mapper.Map<List<AdminViewModel>>(query.ToList());

            return new AdminPagingViewModel
            {
                Admins = adminDTO,
                TotalItem = total,
                Page = page,
                LastPage = (int)Math.Ceiling(Decimal.Divide(total, limit))
            };
        }

       
        public List<Activity> GetActivitiesOfAdmin(int? adminId, string? objectType, DateTime? time)
        {
            var activities = _context.Activities.Where(m => m.AdminId != null);

            // Filter
            if (adminId != null)
            {
                activities = activities.Where(m => m.AdminId == adminId);
            }            
            if (!string.IsNullOrEmpty(objectType))
            {
                string[] listObjectType = objectType.Trim().Split(',');
                activities = activities.Where(m => listObjectType.Contains(m.ObjectType));
            }

            // Filter by date
            if (time != null)
            {
                activities = activities.Where(m => m.Time.Date == time.Value.Date);
            }

            return activities.OrderByDescending(m => m.Time).ToList();
        }

        public AdminViewModel GetAdminById(int adminId)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            var admin = _context.Admins.Find(adminId);
            if (admin != null)
            {
                adminViewModel.AdminId = admin.AdminId;
                adminViewModel.UserName = admin.UserName;
                adminViewModel.Avatar = admin.Avatar;
                adminViewModel.FirstName = admin.FirstName;
                adminViewModel.LastName = admin.LastName;
                adminViewModel.Email = admin.Email;
                adminViewModel.Phone = admin.Phone;
                adminViewModel.Birthday = admin.Birthday;
                adminViewModel.Gender = admin.Gender == Gender.Men ? "Men" : "Women";
                adminViewModel.RegisteredDate = admin.RegisteredDate;
                adminViewModel.RoleId = admin.RoleId;
                return adminViewModel;
            }
            return null;
        }

        public AdminViewModel AuthenticateAdmin(LoginViewModel loginViewModel)
        {
            var admin = new AdminViewModel();
            admin = _context.Admins
                .Where(m => m.UserName == loginViewModel.UserName && m.Password == loginViewModel.Password)
                .Include(m => m.Role)
                .Select(m => new AdminViewModel
                {
                    AdminId = m.AdminId,
                    UserName = m.UserName,
                    Avatar = m.Avatar,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Email = m.Email,
                    Phone = m.Phone,
                    Birthday = m.Birthday,
                    Gender = m.Gender == Gender.Men ? "Men" : "Women",
                    RoleName = m.Role.RoleName
                }).FirstOrDefault();
            return admin;
        }

        public bool CheckExistUserName(string username)
        {
            var admin = new Admin();
            admin = _context.Admins.FirstOrDefault(m => m.UserName == username);
            return admin != null;
        }


        public AdminViewModel CreateAdmin(AdminViewModel adminViewModel)
        {
            var adminDTO = new AdminViewModel();
            if (CheckExistUserName(adminViewModel.UserName))
            {
                adminDTO.IsExistedUsername = true;
                return adminDTO;
            }

            Admin admin = new Admin()
            {
                UserName = adminViewModel.UserName,
                Password = adminViewModel.Password,
                FirstName = adminViewModel.FirstName,
                LastName = adminViewModel.LastName,
                Email = adminViewModel.Email,
                Phone = adminViewModel.Phone,
                Birthday = adminViewModel.Birthday,
                RegisteredDate = adminViewModel.RegisteredDate,
                Gender = adminViewModel.Gender == "Men" ? Gender.Men : Gender.Women,
                Avatar = "avatar.jpg",
                Status = true,
                RoleId = adminViewModel.RoleName == "Admin" ? 1 : 2
            };
            _context.Admins.Add(admin);
            _context.SaveChanges();
            adminDTO = _mapper.Map<AdminViewModel>(admin);
            adminDTO.RoleName = admin.RoleId == 1 ? "Admin" : "Employee";

            return adminDTO;
        }

        public AdminViewModel UpdateAdmin(int adminId, AdminViewModel adminViewModel)
        {
            var admin = _context.Admins.Find(adminId);
            if (admin != null)
            {
                admin.FirstName = adminViewModel.FirstName;
                admin.LastName = adminViewModel.LastName;
                admin.Email = adminViewModel.Email;
                admin.Phone = adminViewModel.Phone;
                admin.Birthday = adminViewModel.Birthday;
                admin.RegisteredDate = adminViewModel.RegisteredDate;
                admin.Gender = adminViewModel.Gender == "Men" ? Gender.Men : Gender.Women;
                admin.RoleId = adminViewModel.RoleName == "Admin" ? 1 : 2;

                _context.Admins.Update(admin);
                _context.SaveChanges();

                var adminDTO = _mapper.Map<AdminViewModel>(admin);
                adminDTO.RoleName = admin.RoleId == 1 ? "Admin" : "Employee";
                return adminDTO;
            }
            return null;
        }

        public AdminViewModel DisabledAdmin(int adminId)
        {
            var admin = _context.Admins.Find(adminId);
            if (admin != null)
            {
                admin.Status = false;

                _context.Admins.Update(admin);
                _context.SaveChanges();

                var adminDTO = _mapper.Map<AdminViewModel>(admin);
                return adminDTO;
            }
            return null;
        }


        public Activity SaveActivity(int? adminId, string action, string objectType, string objectName)
        {
            Activity activity = new Activity();
            activity.AdminId = adminId;
            activity.ActivityType = action;
            activity.ObjectType = objectType;
            activity.ObjectName = objectName;
            activity.Time = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            _context.Activities.Add(activity);

            _context.SaveChanges();
            return activity;
        }
    }
}
