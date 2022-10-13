using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class Payment
    {

        [Key]
        public int PaymentId { get; set; }
        public string PaymentName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
