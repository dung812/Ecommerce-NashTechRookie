using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesShop.Domain.Enum;

namespace ShoesShop.Domain
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
        public string Avatar { get; set; } = null!;
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
