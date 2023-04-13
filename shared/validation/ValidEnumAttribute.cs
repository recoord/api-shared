using System.ComponentModel.DataAnnotations;

public class ValidEnumAttribute : ValidationAttribute
{
    public bool AllowZeroValues { get; set; } = true;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        var type = value.GetType();

        if (!(type.IsEnum && Enum.IsDefined(type, value)) || !AllowZeroValues && Convert.ToInt32(value) == 0)
        {
            return new ValidationResult(ErrorMessage ?? $"{value} is not a valid value for type {type.Name}");
        }

        return ValidationResult.Success;
    }
}