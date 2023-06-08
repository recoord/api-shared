namespace Api.Shared;

using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

[SwaggerSchema("Types of sport for a recording.")]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SportV1
{
    Unspecified = 0,
    Football = 1,
    IndoorFootball = 2,
    Basketball = 3,
    IceHockey = 4,
    OtherSport = 5,
    NotSport = 6,
    AmericanFootball = 7,
    Handball = 8,
    Lacrosse = 9,
    Rugby = 10,
    Volleyball = 11
}