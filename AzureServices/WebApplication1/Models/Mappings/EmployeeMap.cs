using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Version).IsConcurrencyToken();
            builder.Property<int>("TenantId");
            builder.HasQueryFilter(x => EF.Property<int>(x, "TenantId") == 1);
        }
    }
}
