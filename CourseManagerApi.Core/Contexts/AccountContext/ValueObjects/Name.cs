using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.AccountContext.ValueObjects;

public class Name : ValueObject
{
    protected Name() { }

    public Name(string value)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            throw new Exception("Nome inválido");
                
        Value = value;
    }

    public string Value { get; private set; } = string.Empty;

    public static implicit operator Name(string name) => new Name(name);
    
    public static implicit operator String(Name name) => name.ToString();

    public override string ToString() => Value;
}