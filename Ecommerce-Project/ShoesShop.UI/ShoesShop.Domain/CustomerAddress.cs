using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class CustomerAddress
    {
        [Key]
        public int CustomerAddressId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool? IsDefault { get; set; }
        public bool Status { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
