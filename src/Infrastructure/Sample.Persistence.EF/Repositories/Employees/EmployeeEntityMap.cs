using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Employees;

namespace Sample.Persistence.EF.Repositories.Employees;

public class EmployeeEntityMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.Property(_ => _.Name).IsRequired();
    }
}
