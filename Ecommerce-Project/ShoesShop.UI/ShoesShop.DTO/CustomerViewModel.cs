using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public int TotalOrderSuccess { get; set; }
        public int TotalOrderCancel { get; set; }
        public int TotalMoneyPuschased { get; set; }
    }
}
