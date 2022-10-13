using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public int? AdminId { get; set; }
        public string ActivityType { get; set; } = null!;
        public string ObjectType { get; set; } = null!;
        public string ObjectName { get; set; } = null!;
        public DateTime Time { get; set; } = DateTime.Now;

        public virtual Admin? Admin { get; set; }
    }
}
