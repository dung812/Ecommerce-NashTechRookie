using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class StatisticNumberViewModel
    {
        public string Title { get; set; } = null!;
        public int Number { get; set; }
        public bool IsUp { get; set; }
        public string Percent { get; set; }
        public string Prefix { get; set; } = null!;
        public string Suffix { get; set; } = null!;

    }
}
