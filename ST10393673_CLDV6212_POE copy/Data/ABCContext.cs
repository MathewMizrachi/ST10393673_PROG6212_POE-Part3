using Microsoft.EntityFrameworkCore;
using ST10393673_CLDV6212_POE.Controllers;
using ST10393673_CLDV6212_POE.Models;
using System.Collections.Generic;

namespace ST10393673_CLDV6212_POE.Data
{
    public class ABCContext : DbContext
    {
        public ABCContext(DbContextOptions<ABCContext> options) : base(options) { }

        // DbSet for each entity that you want to include in your database
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
