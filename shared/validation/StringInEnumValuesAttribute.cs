namespace Api.Shared;

using System.ComponentModel.DataAnnotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class StringInEnumValuesAttribute : ValidationAttribute
{
    public Type Type { get; set; }

    public StringInEnumValuesAttribute(Type type)
    {
        Type = type;
    }

    public override string FormatErrorMessage(string name)
    {
        var enumValuesString = String.Join("|", (new List<string>(Enum.GetNames(Type))).ConvertAll(s => s.ToLowerInvariant()));
        return name + " must be one of the following values " + enumValuesString;
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return false;
        }

        switch (value)
        {
            case string str:
                return Enum.TryParse(Type!, str, true, out object? result);
            default:
                return false;
        }
    }
}