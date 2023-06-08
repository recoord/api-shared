namespace Api.Shared;

[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Struct |
    AttributeTargets.Parameter |
    AttributeTargets.Property |
    AttributeTargets.Enum,
    AllowMultiple = false)]
public class SwaggerSchemaExampleAttribute : System.Attribute
{
    public Object Example { get; set; }
    public bool EnumType { get; set; }
    public SwaggerSchemaExampleAttribute(Object example, bool enumType = false)
    {
        Example = example;
        enumType = EnumType;
    }
}