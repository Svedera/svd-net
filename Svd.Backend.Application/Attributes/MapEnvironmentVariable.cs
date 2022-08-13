namespace Svd.Backend.Application.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class MapEnvironmentVariable : Attribute
{
    private string Name { get; }

    public MapEnvironmentVariable(string name)
    {
        Name = name;
    }

    public string? GetVarValue() =>
        Environment.GetEnvironmentVariable(Name);
}
