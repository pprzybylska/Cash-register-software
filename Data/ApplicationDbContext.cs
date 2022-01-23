using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<CartItem> Cart{ get; set; }
        public DbSet<Bon> Bony{ get; set; }
    }
}
