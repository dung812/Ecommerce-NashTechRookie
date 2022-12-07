using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
    }
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<AdminViewModel> GetAllAdmin()
        {
            var Admins = new List<AdminViewModel>();
            Admins = _context.Admins
                .Where(m => m.Status)
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
                    RegisteredDate = m.RegisteredDate,
                    RoleName = m.Role.RoleName
                }).ToList();
            return Admins;
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
