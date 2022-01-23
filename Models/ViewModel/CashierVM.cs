using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ViewModel
{
    public class CashierVM
    {

        public string Id { get; set; }
        [Display(Name="Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Transactions { get; set; }


    }
}
