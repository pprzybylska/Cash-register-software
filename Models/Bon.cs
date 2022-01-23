using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Bon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Kwota musi byc wieksza od 0")]
        [Display(Name ="Wartość bonu")]
        public int Value { get; set; }

        [Display(Name ="Produkt")]
        public int  ProductID { get; set; }

        [ForeignKey("ProductID")]
        [Display(Name ="Produkt")]
        public virtual Product product { get; set; }
        public bool Used { get; set; }
    }
}
