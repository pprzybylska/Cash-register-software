using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nazwa produktu:")]
        [Required]
        public string ProductName { get; set; }
        [DisplayName("Cena produktu:")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Cena musi być liczbą dodatnią")]
        public int ProductPrice { get; set; }


    }
}
