using Sample.Core.Entities.Generals;

namespace Sample.Core.Entities.Employees;

public class Employee : BaseEntity
{
    public int Age { get; set; }
    
    public required string Name { get; set; }
}
