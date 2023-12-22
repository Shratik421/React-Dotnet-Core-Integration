using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace reactdonetwebapi.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
    
                public DbSet<Employee> Employees { get; set; }
          
        }
    
}
