using ShoesShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO.Admin
{
    public class AdminViewModel
    {
        public int? AdminId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime RegisteredDate { get; set; }
        public string? Avatar { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool? IsExistedUsername { get; set; }
        public bool? Status { get; set; }
    }
}
