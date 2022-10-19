using Microsoft.EntityFrameworkCore;
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
    public interface ICustomerAddressService
    {
        public List<CustomerAddressViewModel> GetAddressListOfCustomerById(int customerId);
        public CustomerAddress GetCustomerAddressById(int customerAddressId);
        public CustomerAddress GetDefaultAddressOfCustomer(int customerId);
        public int CountAddressOfCustomer(int customerId);
        public void CreateAddressOfCustomer(CustomerAddressViewModel customerAddressViewModel, bool isDefault);
        public void UpdateAddressOfCustomer(int customerAddressId, CustomerAddressViewModel customerAddressViewModel, bool isDefault);
        public void DeleteAddressOfCustomer(int customerAddressId, int customerId);
    }
    public class CustomerAddressService : ICustomerAddressService
    {
        public List<CustomerAddressViewModel> GetAddressListOfCustomerById(int customerId)
        {
            List<CustomerAddressViewModel> addresses = new List<CustomerAddressViewModel>();
            using (var context = new ApplicationDbContext())
            {
                addresses = context.CustomerAddresses.Where(m => m.CustomerId == customerId && m.Status == true).Select(m => new CustomerAddressViewModel
                {
                    customerAddressId = m.CustomerAddressId,
                    CustomerId = m.CustomerId,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Address = m.Address,
                    Phone = m.Phone,   
                    IsDefault = m.IsDefault ?? false,
                }).ToList(); 
            }
            return addresses;
        }


        public CustomerAddress GetCustomerAddressById(int customerAddressId)
        {
            var customerAddress = new CustomerAddress();
            using (var context = new ApplicationDbContext())
            {
                customerAddress = context.CustomerAddresses.FirstOrDefault(m => m.CustomerAddressId == customerAddressId);
            }
            return customerAddress ?? new CustomerAddress();
        }
        public CustomerAddress GetDefaultAddressOfCustomer(int customerId)
        {
            var addressDefault = new CustomerAddress();
            using (var context = new ApplicationDbContext())
            {
                addressDefault = context.CustomerAddresses.Where(m => m.IsDefault == true && m.CustomerId == customerId).FirstOrDefault();
            }
            return addressDefault;
        }
        public int CountAddressOfCustomer(int customerId)
        {
            int count = 0;
            using (var context = new ApplicationDbContext())
            {
                count = context.CustomerAddresses.Where(m => m.CustomerId == customerId).Count();
            }
            return count;
        }
        public void CreateAddressOfCustomer(CustomerAddressViewModel customerAddressViewModel, bool isDefault)
        {
            using (var context = new ApplicationDbContext())
            {
                CustomerAddress customerAddress = new CustomerAddress();
                customerAddress.CustomerId = customerAddressViewModel.CustomerId;
                customerAddress.FirstName = customerAddressViewModel.FirstName;
                customerAddress.LastName = customerAddressViewModel.LastName;
                customerAddress.Address = customerAddressViewModel.Address;
                customerAddress.Phone = customerAddressViewModel.Phone;
                customerAddress.Status = true;

                if (isDefault)
                {
                    // Set all default address is false
                    context.Database.ExecuteSqlRaw("UPDATE CustomerAddresses SET IsDefault = 0 WHERE IsDefault = '1' AND CustomerId = '" + customerAddressViewModel.CustomerId + "'");

                    customerAddress.IsDefault = true;
                }
                else
                {
                    int countAddress = CountAddressOfCustomer(customerAddressViewModel.CustomerId);

                    if (countAddress == 0)
                    {
                        // If new address is first
                        customerAddress.IsDefault = true;
                    }
                    else
                    {
                        customerAddress.IsDefault = false;
                    }

                }

                context.CustomerAddresses.Add(customerAddress);
                context.SaveChanges();
            }
        }
        public void UpdateAddressOfCustomer(int customerAddressId, CustomerAddressViewModel customerAddressViewModel, bool isDefault)
        {
            using (var context = new ApplicationDbContext())
            {

                var customerAddress = context.CustomerAddresses.Find(customerAddressId);

                if (customerAddress != null)
                {
                    customerAddress.FirstName = customerAddressViewModel.FirstName;
                    customerAddress.LastName = customerAddressViewModel.LastName;
                    customerAddress.Address = customerAddressViewModel.Address;
                    customerAddress.Phone = customerAddressViewModel.Phone;

                    if (isDefault)
                    {
                        // Set all default address is false
                        context.Database.ExecuteSqlRaw("UPDATE CustomerAddresses SET IsDefault = 0 WHERE IsDefault = '1' AND CustomerId = '" + customerAddressViewModel.CustomerId + "'");

                        customerAddress.IsDefault = true;
                    }
                    context.CustomerAddresses.Update(customerAddress);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteAddressOfCustomer(int customerAddressId, int customerId)
        {
            using (var context = new ApplicationDbContext())
            {
                var customerAddress = GetCustomerAddressById(customerAddressId);

                if (customerAddress.IsDefault == true)
                {
                    // Case address was deleted is default, set first address in list is default
                    context.Database.ExecuteSqlRaw("UPDATE TOP (1) CustomerAddresses SET IsDefault = 1 WHERE IsDefault = '0' AND CustomerId = '" + customerId + "'");
                }
                context.CustomerAddresses.Remove(customerAddress);
                context.SaveChanges();
            }
        }

    }
}
