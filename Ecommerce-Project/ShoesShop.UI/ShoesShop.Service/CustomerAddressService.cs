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
        public bool CreateAddressOfCustomer(CustomerAddressViewModel customerAddressViewModel, bool isDefault);
        public bool UpdateAddressOfCustomer(int customerAddressId, CustomerAddressViewModel customerAddressViewModel, bool isDefault);
        public bool DeleteAddressOfCustomer(int customerAddressId, int customerId);
    }
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ApplicationDbContext _context;
        public CustomerAddressService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<CustomerAddressViewModel> GetAddressListOfCustomerById(int customerId)
        {
            List<CustomerAddressViewModel> addresses = new List<CustomerAddressViewModel>();
            addresses = _context.CustomerAddresses
                .Where(m => m.CustomerId == customerId && m.Status == true)
                .Select(m => new CustomerAddressViewModel
                {
                    customerAddressId = m.CustomerAddressId,
                    CustomerId = m.CustomerId,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Address = m.Address,
                    Phone = m.Phone,
                    IsDefault = m.IsDefault ?? false,
                }).ToList();
            return addresses;
        }
        public CustomerAddress GetCustomerAddressById(int customerAddressId)
        {
            var customerAddress = new CustomerAddress();
            customerAddress = _context.CustomerAddresses.FirstOrDefault(m => m.CustomerAddressId == customerAddressId);
            return customerAddress ?? new CustomerAddress();
        }
        public CustomerAddress GetDefaultAddressOfCustomer(int customerId)
        {
            var addressDefault = new CustomerAddress();
            addressDefault = _context.CustomerAddresses.Where(m => m.IsDefault == true && m.CustomerId == customerId).FirstOrDefault();
            return addressDefault ?? new CustomerAddress();
        }
        public int CountAddressOfCustomer(int customerId)
        {
            int count = 0;
            count = _context.CustomerAddresses.Where(m => m.CustomerId == customerId).Count();
            return count;
        }
        public bool CreateAddressOfCustomer(CustomerAddressViewModel customerAddressViewModel, bool isDefault)
        {
            try
            {
                CustomerAddress customerAddress = new CustomerAddress
                {
                    CustomerId = customerAddressViewModel.CustomerId,
                    FirstName = customerAddressViewModel.FirstName,
                    LastName = customerAddressViewModel.LastName,
                    Address = customerAddressViewModel.Address,
                    Phone = customerAddressViewModel.Phone,
                    Status = true
                };

                if (isDefault)
                {
                    // Set all default address is false
                    _context.Database.ExecuteSqlRaw("UPDATE CustomerAddresses SET IsDefault = 0 WHERE IsDefault = '1' AND CustomerId = '" + customerAddressViewModel.CustomerId + "'");

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

                _context.CustomerAddresses.Add(customerAddress);
                _context.SaveChanges();
                return true;
            } catch(Exception)
            {
                return false;
            }
            
        }
        public bool UpdateAddressOfCustomer(int customerAddressId, CustomerAddressViewModel customerAddressViewModel, bool isDefault)
        {
            try
            {
                var customerAddress = _context.CustomerAddresses.Find(customerAddressId);

                if (customerAddress != null)
                {
                    customerAddress.FirstName = customerAddressViewModel.FirstName;
                    customerAddress.LastName = customerAddressViewModel.LastName;
                    customerAddress.Address = customerAddressViewModel.Address;
                    customerAddress.Phone = customerAddressViewModel.Phone;

                    if (isDefault)
                    {
                        // Set all default address is false
                        _context.Database.ExecuteSqlRaw("UPDATE CustomerAddresses SET IsDefault = 0 WHERE IsDefault = '1' AND CustomerId = '" + customerAddressViewModel.CustomerId + "'");

                        customerAddress.IsDefault = true;
                    }
                    _context.CustomerAddresses.Update(customerAddress);
                    _context.SaveChanges();
                }
                else
                    return false;
                return true;
            } catch(Exception)
            {
                return false;
            }
            
        }
        public bool DeleteAddressOfCustomer(int customerAddressId, int customerId)
        {
            try
            {
                var customerAddress = GetCustomerAddressById(customerAddressId);

                if (customerAddress.IsDefault == true)
                {
                    // Case address was deleted is default, set first address in list is default
                    _context.Database.ExecuteSqlRaw("UPDATE TOP (1) CustomerAddresses SET IsDefault = 1 WHERE IsDefault = '0' AND CustomerId = '" + customerId + "'");
                }
                _context.CustomerAddresses.Remove(customerAddress);
                _context.SaveChanges();
                return true;
            } catch(Exception)
            {
                return false;
            }
        }

    }
}
