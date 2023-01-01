using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.DTO.Admin;

namespace ShoesShop.Service
{
    public interface IContactService
    {
        public bool Create(ContactViewModel contactViewModel);
    }
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(ContactViewModel contactViewModel)
        {
            Contact contact = new Contact()
            {
                Name = contactViewModel.Name,
                Subject = contactViewModel.Subject,
                Message = contactViewModel.Message,
                Email = contactViewModel.Email,
                DateContact = DateTime.Now,
            };

            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return true;
        }
    }
}
