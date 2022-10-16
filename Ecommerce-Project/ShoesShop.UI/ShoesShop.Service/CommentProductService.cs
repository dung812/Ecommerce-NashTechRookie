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
    public interface ICommentProductService
    {
        //List<ProductViewModel> GetAllProduct();
        //ProductViewModel GetSingleProduct(int productId);
        public List<CommentProduct> GetListCommentOfProductById(int productId);
    }
    public class CommentProductService : ICommentProductService
    {
        public List<CommentProduct> GetListCommentOfProductById(int productId)
        {
            List<CommentProduct> list = new List<CommentProduct>();
            using (var context = new ApplicationDbContext())
            {
                var comments = context.CommentProducts.Where(p => p.ProductId == productId).ToList();
                list = comments;
            }
            return list;
        }
    }
}
