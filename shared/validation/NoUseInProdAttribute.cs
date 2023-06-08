namespace Api.Shared;

using System.ComponentModel.DataAnnotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class NoUseInProdAttribute : ValidationAttribute
{
    public const string DefaultErrorMessage = "The {0} field must not be used in production";
    public NoUseInProdAttribute() : base(DefaultErrorMessage) { }

    public override bool IsValid(object? value)
    {
        //NotEmpty doesn't necessarily mean required
        if (value is null)
        {
            return true;
        }

        return !EnvironmentHelper.IsProdEnvironment();
    }
}