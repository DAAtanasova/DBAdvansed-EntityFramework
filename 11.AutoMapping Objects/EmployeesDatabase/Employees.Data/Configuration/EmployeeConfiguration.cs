using Employees.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(f => f.FirstName)
                .IsRequired();

            builder.Property(l => l.LastName)
                .IsRequired();

            builder.Property(s => s.Salary)
                .IsRequired();

            builder.Property(b => b.Birthday)
              .IsRequired(false);

            builder.Property(a => a.Address)
                .IsRequired(false);
        }
    }
}
