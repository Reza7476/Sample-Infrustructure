using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Employees;

namespace Sample.Persistence.EF.EntitiesConfig.Employees;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees").HasKey(_=>_.Id);

        builder.Property(_ => _.Id).IsRequired();

        builder.Property(_ => _.Name);

        builder.Property(_ => _.Age);
    }
}
