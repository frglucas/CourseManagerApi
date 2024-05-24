using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;

public class Name : ValueObject
{
    protected Name() { }

    public Name(string fullName, string badgeName)
    {
        if (string.IsNullOrEmpty(fullName) || string.IsNullOrWhiteSpace(fullName))
            throw new Exception("Nome inválido");

        if (string.IsNullOrEmpty(badgeName) || string.IsNullOrWhiteSpace(badgeName))
            throw new Exception("Crachá inválido"); 

        FullName = fullName;
        BadgeName = badgeName;
    }

    public string FullName { get; private set; } = string.Empty;
    public string BadgeName { get; private set; } = string.Empty;

    public static implicit operator Name(string fullName) => new Name(fullName, fullName.Split("").First());
    
    public static implicit operator String(Name name) => name.ToString();

    public override string ToString() => FullName;
}