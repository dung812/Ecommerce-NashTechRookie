using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class CommentProductViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
