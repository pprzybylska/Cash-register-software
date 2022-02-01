using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public bool IsBonUsed { get; set; }   
    }
}
