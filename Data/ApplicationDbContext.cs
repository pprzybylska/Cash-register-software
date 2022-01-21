using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Bon> Bony { get; set; }

    }
}
