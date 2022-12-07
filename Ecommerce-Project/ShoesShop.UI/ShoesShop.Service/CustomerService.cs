using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Domain.Enum;

namespace ShoesShop.Service
{
    public interface ICustomerService
    {
        public List<CustomerViewModel> GetAllCustomer();
        public bool CreateCustomer(CustomerViewModel customerViewModel);
        public bool CheckExistEmailOfCustomer(string email);
        public Customer GetValidCustomerByEmail(string email);
        public Customer GetValidCustomerById(int customerId);

        public Customer ValidateCustomerAccount(string email, string password);
        public void ChangePassword(int customerId, string newPassword);
        public void ResetPassword(int customerId, string newPassword);

        // Token
        public List<ForgotPassword> GetListTokenCustomerByEmail(string email);
        public void CreateTokenForgotPassword(int customerId, string email, string token);
        public ForgotPassword TokenValidate(string token);
        public void ActiveToken(string token);
        public int CountTokenInCurrentDayOfCustomer(int customerId);
        public Customer ChangeAvatarOfCustomer(int customerId, string newAvatarName);
        public bool CheckIsFirstLogin(int customerId);
        public Customer ChangeInformationFirstLogin(int customerId, FirstChangePasswordViewModel firstChangePasswordViewModel);

    }
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<CustomerViewModel> GetAllCustomer()
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            customers = _context.Customers
                                    .TagWith("Get list customer")
                                    .OrderByDescending(m => m.RegisterDate)
                                    .Select(m => new CustomerViewModel
                                    {
                                        CustomerId = m.CustomerId,
                                        FirstName = m.FirstName,
                                        LastName = m.LastName,
                                        Avatar = m.Avatar,
                                        Email = m.Email,
                                        RegisterDate = m.RegisterDate,
                                        TotalMoneyPuschased = 0,
                                        TotalOrderSuccess = 0,
                                        TotalOrderCancel = 0,
                                    }).ToList();


            // Handle money puschased

            // Handle total order success

            // Handle total order cancellation

            return customers;
        }

        public bool CreateCustomer(CustomerViewModel customerViewModel)
        {
            try
            {
                var checkExistEmail = CheckExistEmailOfCustomer(customerViewModel.Email);
                if (!checkExistEmail)
                {
                    Customer customer = new Customer
                    {
                        Email = customerViewModel.Email,
                        FirstName = customerViewModel.FirstName,
                        LastName = customerViewModel.LastName,
                        Password = customerViewModel.Password,
                        RegisterDate = DateTime.Now,
                        IsNewRegister = true,
                        Avatar = "avatar.jpg",
                        Status = true
                    };
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckExistEmailOfCustomer(string email)
        {
            var customer = new Customer();
            customer = _context.Customers.FirstOrDefault(m => m.Email == email);
            return customer != null ? true : false;
        }

        public Customer GetValidCustomerByEmail(string email) 
        {
            Customer customer = new Customer();
            customer = _context.Customers.Where(m => m.Status == true).FirstOrDefault(m => m.Email == email);
            return customer;
        }        
        public Customer GetValidCustomerById(int customerId) 
        {
            var customer = new Customer();
            customer = _context.Customers.FirstOrDefault(m => m.CustomerId == customerId && m.Status);
            return customer;
        }

        public Customer ValidateCustomerAccount(string email, string password)
        {
            var customer = new Customer();
            customer = _context.Customers.FirstOrDefault(m => m.Email == email && m.Password == password);
            return customer;
        }

        public void ChangePassword(int customerId, string newPassword)
        {
            var customer = _context.Customers.FirstOrDefault(m => m.CustomerId == customerId);
            if (customer != null)
            {
                customer.Password = newPassword;
                _context.SaveChanges();
            }
        }        
        public void ResetPassword(int customerId, string newPassword)
        {
            var customer = _context.Customers.FirstOrDefault(m => m.CustomerId == customerId);
            if (customer != null)
            {
                customer.Password = newPassword;
                customer.IsNewRegister = false;
                customer.IsLockedFirstLogin = false;
                _context.SaveChanges();
            }
        }


        // Forgot password
        public List<ForgotPassword> GetListTokenCustomerByEmail(string email)
        {
            List<ForgotPassword> list = new List<ForgotPassword>();
            list = _context.ForgotPasswords.Where(m => m.Email == email).ToList();
            return list;
        }
        public void CreateTokenForgotPassword(int customerId, string email, string token)
        {
            // Set all other token is unable
            _context.Database.ExecuteSqlRaw("UPDATE ForgotPasswords SET Status = 0 WHERE CustomerId = " + customerId + "");

            // Add new token
            ForgotPassword forgotPassword = new ForgotPassword
            {
                Email = email.Trim(),
                Token = token,
                CreateDate = DateTime.Now,
                Status = true,
                CustomerId = customerId
            };
            _context.ForgotPasswords.Add(forgotPassword);
            _context.SaveChanges();
        }

        public ForgotPassword TokenValidate(string token)
        {
            var forgotPassword = new ForgotPassword();
            forgotPassword = _context.ForgotPasswords.FirstOrDefault(m => m.Token == token && m.Status == true);
            return forgotPassword ?? new ForgotPassword();
        }

        public void ActiveToken(string token)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword = TokenValidate(token);
            if (forgotPassword != null)
            {
                forgotPassword.Status = false;
                _context.SaveChanges();
            }
        }

        public int CountTokenInCurrentDayOfCustomer(int customerId)
        {
            var count = 0;
            DateTime today = DateTime.Today;
            count = _context.ForgotPasswords.Where(m => m.CreateDate.Date == DateTime.Today && m.CustomerId == customerId).Count();
            return count;
        }

        public Customer ChangeAvatarOfCustomer(int customerId, string newAvatarName)
        {
            var customer = new Customer();
            customer = _context.Customers.Find(customerId);

            if (customer != null)
            {
                customer.Avatar = newAvatarName;

                _context.Customers.Update(customer);
                _context.SaveChanges();

                return customer;
            }

            return customer;
        }

        public bool CheckIsFirstLogin(int customerId)
        {
            var result = false;
            var customer = _context.Customers.Find(customerId);
            if (customer?.IsNewRegister == true)
            {
                customer.IsLockedFirstLogin = true;
                _context.SaveChanges();
                result = true;
            }
            else
                result = false;
            return result;
        }

        public Customer ChangeInformationFirstLogin(int customerId, FirstChangePasswordViewModel firstChangePasswordViewModel)
        {
            var customer = new Customer();
            customer = _context.Customers.Find(customerId);
            if (customer != null)
            {
                customer.Password = firstChangePasswordViewModel.NewPassword;
                customer.FirstName = firstChangePasswordViewModel.FirstName;
                customer.LastName = firstChangePasswordViewModel.LastName;
                customer.IsNewRegister = false;

                customer.IsLockedFirstLogin = false;
                _context.SaveChanges();
            }
            return customer;
        }
    }
}
