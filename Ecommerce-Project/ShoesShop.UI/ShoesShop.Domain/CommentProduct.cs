using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class CommentProduct
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Star { get; set; }
        public string Content { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.Now;
        public bool Status { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
