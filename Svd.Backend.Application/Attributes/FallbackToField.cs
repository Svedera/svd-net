namespace Svd.Backend.Application.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class FallbackToField : Attribute
{
    public string Name { get; }

    public FallbackToField(string name)
    {
        Name = name;
    }
}
