using System.ComponentModel.DataAnnotations;

namespace Sample.Commons.Contracts;

public class EnumValueChecker : RequiredAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null) return true;
        var type = value.GetType();

        return type.IsEnum && Enum.IsDefined(type, value);
    }
}
