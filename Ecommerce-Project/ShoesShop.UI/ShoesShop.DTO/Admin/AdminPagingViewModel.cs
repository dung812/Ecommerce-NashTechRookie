using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO.Admin
{
    public class AdminPagingViewModel
    {
        public List<AdminViewModel> Admins { get; set; }
        public int TotalItem { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
    }
}
