using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Mappings;

namespace WebApplication1.Models
{
    public class HRMSContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Product> Products { get; set; }

        public HRMSContext(DbContextOptions<HRMSContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne<Department>(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId);
                
            modelBuilder.ApplyConfiguration(new EmployeeMap());
        }
    }
}
