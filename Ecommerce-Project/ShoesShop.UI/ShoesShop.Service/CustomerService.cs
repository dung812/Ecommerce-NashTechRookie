using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Service
{
    public interface ICustomerService
    {
        public bool CreateCustomer(CustomerViewModel customerViewModel);
        public bool CheckExistEmailOfCustomer(string email);
        public Customer GetValidCustomerByEmail(string email);
        public Customer ValidateCustomerAccount(string email, string password);
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
            Customer customer = new Customer();
            using (var context = new ApplicationDbContext())
            {
                customer = context.Customers.FirstOrDefault(m => m.Email == email && m.Password == password);
            }
            return customer;
        }

        
    }
}
