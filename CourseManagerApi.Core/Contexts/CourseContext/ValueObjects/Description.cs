using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.CourseContext.ValueObjects;

public class Description : ValueObject
{
    protected Description() { }
    public Description(string description)
    {
        if (string.IsNullOrEmpty(description))
            throw new Exception("Descrição inválida");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new Exception("Descrição inválida");

        Value  = description;
    }

    public string Value { get; private set; } = string.Empty;

    public static implicit operator Description(string value) => new(value);

    public static implicit operator string(Description description) => description.ToString();

    public override string ToString() => Value;
}