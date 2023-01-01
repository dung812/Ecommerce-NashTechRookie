using ShoesShop.Data;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.Domain;
using Microsoft.EntityFrameworkCore;


namespace ShoesShop.Service
{
    public interface ICommentProductService
    {
        public List<CommentProductViewModel> GetListCommentOfProductById(int productId);
        public bool CheckCustomerCommentYet(int productId, int customerId);
        public bool AddCommentOfProduct(int productId, int customerId, int star, string content);
    }
    public class CommentProductService : ICommentProductService
    {
        private readonly ApplicationDbContext _context;
        public CommentProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<CommentProductViewModel> GetListCommentOfProductById(int productId)
        {
            List<CommentProductViewModel> comments = new List<CommentProductViewModel>();
            comments = _context.CommentProducts
                    .Where(p => p.ProductId == productId)
                    .Include(m => m.Customer)
                    .Select(m => new CommentProductViewModel
                    {
                        FirstName = m.Customer.FirstName,
                        LastName = m.Customer.LastName,
                        Avatar = m.Customer.Avatar,
                        Star = m.Star,
                        Content = m.Content,
                        Date = m.Date
                    }).ToList();
            return comments;
        }

        public bool CheckCustomerCommentYet(int productId, int customerId)
        {
            var result = false;
            var countCommentOfCustomer = _context.CommentProducts.Where(m => m.ProductId == productId && m.CustomerId == customerId).Count();
            if (countCommentOfCustomer >= 1)
                result = true;
            return result;
        }

        public bool AddCommentOfProduct(int productId, int customerId, int star, string content)
        {
            CommentProduct commentProduct = new CommentProduct();
            commentProduct.ProductId = productId;
            commentProduct.CustomerId = customerId;
            commentProduct.Star = star;
            commentProduct.Content = content;
            commentProduct.Date = DateTime.Now;
            commentProduct.Status = true;

            _context.CommentProducts.Add(commentProduct);
            _context.SaveChanges();

            return true;
        }
    }
}
