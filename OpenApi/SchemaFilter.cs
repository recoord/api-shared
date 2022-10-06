using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if(context.MemberInfo != null)
        {
            var schemaAttribute = context.MemberInfo.GetCustomAttributes<SwaggerSchemaExampleAttribute>()
            .FirstOrDefault();
            if (schemaAttribute != null)
                ApplySchemaAttribute(schema, schemaAttribute);
        }
    }

        private void ApplySchemaAttribute(OpenApiSchema schema, SwaggerSchemaExampleAttribute schemaAttribute)
        {
            if (schemaAttribute.Example != null)
            {
                switch( schemaAttribute.Example.GetType() )
                {
                    case Type stringType when stringType == typeof(string):
                        schema.Example = new Microsoft.OpenApi.Any.OpenApiString(schemaAttribute.Example.ToString());
                    break;
                    case Type stringArrayType when stringArrayType == typeof(string[]):
                        var array = new Microsoft.OpenApi.Any.OpenApiArray();
                        var elements = schemaAttribute.Example as string[] ?? new string[] {""};
                        foreach (string element in elements)
                        {
                            array.Add(new Microsoft.OpenApi.Any.OpenApiString(element));
                        }
                        schema.Enum = array;
                    break;
                }
            }
        }
    }