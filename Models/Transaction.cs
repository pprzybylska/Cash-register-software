using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public int Discount { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
