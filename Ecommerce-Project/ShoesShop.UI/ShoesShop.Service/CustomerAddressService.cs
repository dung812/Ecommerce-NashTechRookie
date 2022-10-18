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
        public void CreateAddressOfCustomer(int customerId);
        public void UpdateAddressOfCustomer(int customerId);
        public void DeleteAddressOfCustomer(int customerId);
        public CustomerAddress GetDefaultAddressOfCustomer(int customerId);
        public int CountAddressOfCustomer(int customerId);
    }
    public class CustomerAddressService : ICustomerAddressService
    {
        public void CreateAddressOfCustomer(int customerId)
        {
            using (var context = new ApplicationDbContext())
            {

            }
        }        
        public void UpdateAddressOfCustomer(int customerId)
        {
            using (var context = new ApplicationDbContext())
            {

            }
        }

        public void DeleteAddressOfCustomer(int customerId)
        {
            using (var context = new ApplicationDbContext())
            {

            }
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
    }
}
