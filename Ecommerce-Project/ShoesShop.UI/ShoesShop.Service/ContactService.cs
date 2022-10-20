using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;

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
            Contact contact = new Contact()
            {
                Name = contactViewModel.Name,
                Subject = contactViewModel.Subject,
                Message = contactViewModel.Message,
                Email = contactViewModel.Email,
                DateContact = DateTime.Now,
            };

            using (var context = new ApplicationDbContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }
    }
}
