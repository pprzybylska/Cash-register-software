using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ProductsCartVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<CartItem> Cart { get; set; }
        public IEnumerable<Bon> Bons { get; set; }

    }
}
