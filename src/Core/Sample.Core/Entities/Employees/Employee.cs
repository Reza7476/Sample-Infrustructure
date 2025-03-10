using Sample.Core.Entities.Generals;

namespace Sample.Core.Entities.Employees;

public class Employee : BaseEntity
{
    public required string Name { get; set; }
    public required int Age { get; set; }
    
}
