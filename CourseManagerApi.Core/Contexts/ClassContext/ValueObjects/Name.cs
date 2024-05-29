using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.ClassContext.ValueObjects;

public class Name : ValueObject
{
    public Name() { }
    public Name(string name) 
    {
        if (string.IsNullOrEmpty(name))
            throw new Exception("Nome inválido");

        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Nome inválido");

        Value = name;
    }
    public string Value { get; private set; } = string.Empty;

    public static implicit operator Name(string name) => new(name);
    
    public static implicit operator String(Name name) => name.ToString();

    public override string ToString() => Value;
}