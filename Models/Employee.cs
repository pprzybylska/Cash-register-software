using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;



namespace WebApplication2.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DisplayName("Hasło")]
        public string Password { get; set; }
        public int Transactions { get; set; }
    }
}
