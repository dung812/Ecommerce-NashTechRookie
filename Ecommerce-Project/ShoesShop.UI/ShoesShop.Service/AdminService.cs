using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.DTO.Admin;

namespace ShoesShop.Service
{
    public interface IAdminService
    {
        public List<AdminViewModel> GetAllAdmin();
        public AdminViewModel GetAdminById(int adminId);
        public AdminViewModel AuthenticateAdmin(LoginViewModel loginViewModel);
        public bool CheckExistUserName(string username);
        public bool CreateAdmin(AdminViewModel adminViewModel);
        public bool UpdateAdmin(int adminId, AdminViewModel adminViewModel);
        public bool DeleteAdmin(int adminId);
        public AdminPagingViewModel GetAllAdminPaging(string? filterByRole, DateTime? filterByDate, string? fieldName, string? searchString, string? sortType, int page, int limit);
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

        public AdminPagingViewModel GetAllAdminPaging(string? filterByRole, DateTime? filterByDate, string? fieldName, string? searchString, string? sortType, int page, int limit)
        {
            // Query data
            var query = _context.Admins
                        .Include(m => m.Role)
                        .Where(m => m.Status && m.Status);

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
            if (filterByDate!= null)
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

        //string[] listRole = filterByRole.Trim().Split(' ');

        //foreach (var role in listRole)
        //{
        //    query = query.Where(m => listRole.Contains(m.Role.RoleName));
        //}

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
            }
            return adminViewModel;
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


        public bool CreateAdmin(AdminViewModel adminViewModel)
        {
            if (CheckExistUserName(adminViewModel.UserName))
                return false;

            try
            {
                Admin admin = new Admin()
                {
                    UserName = adminViewModel.UserName,
                    Password = adminViewModel.Password,
                    FirstName = adminViewModel.FirstName,
                    LastName = adminViewModel.LastName,
                    Email = adminViewModel.Email,
                    Phone = adminViewModel.Phone,
                    Birthday = adminViewModel.Birthday,
                    Gender = adminViewModel.Gender == "Men" ? Gender.Men : Gender.Women,
                    Avatar = adminViewModel.Avatar,
                    RegisteredDate = DateTime.Now,
                    Status = true,
                    RoleId = adminViewModel.RoleId
                };
                _context.Admins.Add(admin);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAdmin(int adminId, AdminViewModel adminViewModel)
        {
            try
            {
                bool result;
                var admin = _context.Admins.Find(adminId);
                if (admin != null)
                {
                    admin.FirstName = adminViewModel.FirstName;
                    admin.LastName = adminViewModel.LastName;
                    admin.Email = adminViewModel.Email;
                    admin.Phone = adminViewModel.Phone;
                    admin.Birthday = adminViewModel.Birthday;
                    admin.Gender = adminViewModel.Gender == "Men" ? Gender.Men : Gender.Women;
                    admin.Avatar = adminViewModel.Avatar;
                    admin.RoleId = adminViewModel.RoleId;

                    _context.Admins.Update(admin);
                    _context.SaveChanges();

                    result = true;
                }
                else
                    result = false;
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAdmin(int adminId)
        {
            try
            {
                bool result;
                var admin = _context.Admins.Find(adminId);
                if (admin != null)
                {
                    admin.Status = false;

                    _context.Admins.Update(admin);
                    _context.SaveChanges();

                    result = true;
                }
                else
                    result = false;
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
