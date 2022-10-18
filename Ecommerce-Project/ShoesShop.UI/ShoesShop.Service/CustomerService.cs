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

namespace ShoesShop.Service
{
    public interface ICustomerService
    {
        public bool CreateCustomer(CustomerViewModel customerViewModel);
        public bool CheckExistEmailOfCustomer(string email);
        public Customer GetValidCustomerByEmail(string email);

        public Customer ValidateCustomerAccount(string email, string password);
        public void ChangePassword(int customerId, string newPassword);

        // Token
        public List<ForgotPassword> GetListTokenCustomerByEmail(string email);
        public void CreateTokenForgotPassword(int customerId, string email, string token);
        public ForgotPassword TokenValidate(string token);
        public void ActiveToken(string token);
        public int CountTokenInCurrentDayOfCustomer(int customerId);

    }
    public class CustomerService : ICustomerService
    {
        public bool CreateCustomer(CustomerViewModel customerViewModel)
        {
            try
            {
                Customer customer = new Customer();
                customer.FirstName = customerViewModel.FirstName;
                customer.LastName = customerViewModel.LastName;
                customer.Email = customerViewModel.Email;
                customer.Password = customerViewModel.Password;
                customer.RegisterDate = DateTime.Now;
                customer.Avatar = "avatar.jpg";
                customer.Status = true;
                using (var context = new ApplicationDbContext())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckExistEmailOfCustomer(string email)
        {
            var isExist = new Customer();
            using (var context = new ApplicationDbContext())
            {
                isExist = context.Customers.FirstOrDefault(m => m.Email == email);
            }
            return isExist == null ? true : false;
        }

        public Customer GetValidCustomerByEmail(string email) 
        {
            Customer customer = new Customer();
            using (var context = new ApplicationDbContext())
            {
                customer = context.Customers.Where(m => m.Status == true).FirstOrDefault(m => m.Email == email);
            }
            return customer;
        }

        public Customer ValidateCustomerAccount(string email, string password)
        {
            var customer = new Customer();
            using (var context = new ApplicationDbContext())
            {
                customer = context.Customers.FirstOrDefault(m => m.Email == email && m.Password == password);
            }
            return customer;
        }

        public void ChangePassword(int customerId, string newPassword)
        {
            using (var context = new ApplicationDbContext())
            {
                var customer = context.Customers.FirstOrDefault(m => m.CustomerId == customerId);
                if (customer != null)
                {
                    customer.Password = newPassword;
                    context.SaveChanges();
                }
            }
                
        }


        // Forgot password
        public List<ForgotPassword> GetListTokenCustomerByEmail(string email)
        {
            List<ForgotPassword> list = new List<ForgotPassword>();
            using (var context = new ApplicationDbContext())
            {
                list = context.ForgotPasswords.Where(m => m.Email == email).ToList();
            }
            return list;
        }
        public void CreateTokenForgotPassword(int customerId, string email, string token)
        {
            using (var context = new ApplicationDbContext())
            {
                // Set all other token is unable
                context.Database.ExecuteSqlRaw("UPDATE ForgotPasswords SET Status = 0 WHERE CustomerId = " + customerId + "");

                // Add new token
                ForgotPassword forgotPassword = new ForgotPassword();
                forgotPassword.Email = email.Trim();
                forgotPassword.Token = token;
                forgotPassword.CreateDate = DateTime.Now;
                forgotPassword.Status = true;
                forgotPassword.CustomerId = customerId;
                context.ForgotPasswords.Add(forgotPassword);
                context.SaveChanges();
            }
        }

        public ForgotPassword TokenValidate(string token)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            using (var context = new ApplicationDbContext())
            {
                forgotPassword = context.ForgotPasswords.FirstOrDefault(m => m.Token == token && m.Status == true);
            }
            return forgotPassword;
        }

        public void ActiveToken(string token)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            using (var context = new ApplicationDbContext())
            {
                forgotPassword = TokenValidate(token);
                if (forgotPassword != null)
                {
                    forgotPassword.Status = false;
                    context.SaveChanges();
                }
            }
        }

        public int CountTokenInCurrentDayOfCustomer(int customerId)
        {
            var count = 0;
            DateTime today = DateTime.Today;
            using (var context = new ApplicationDbContext())
            {
                count = context.ForgotPasswords.Where(m => m.CreateDate.Date == DateTime.Today && m.CustomerId == customerId).Count();
            }
            return count;
        }

    }
}
