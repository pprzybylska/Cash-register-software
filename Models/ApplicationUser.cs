using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Transactions { get; set; }
    }
}
