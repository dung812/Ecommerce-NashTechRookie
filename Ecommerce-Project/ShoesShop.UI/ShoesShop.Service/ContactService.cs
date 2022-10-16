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
    public interface IContactService
    {
        public void Create(ContactViewModel contactViewModel);
    }
    public class ContactService : IContactService
    {
        public void Create(ContactViewModel contactViewModel)
        {
            Contact contact = new Contact();
            contact.Name = contactViewModel.Name;
            contact.Subject = contactViewModel.Subject;
            contact.Message = contactViewModel.Message;
            contact.Email = contactViewModel.Email;
            contact.DateContact = DateTime.Now;

            using (var context = new ApplicationDbContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }
    }
}
