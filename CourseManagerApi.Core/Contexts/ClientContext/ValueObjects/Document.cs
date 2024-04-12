using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using CourseManagerApi.Core.Extensions;
using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;

public class Document : ValueObject
{
    protected Document() { }
    public Document(string number, EDocumentType type)
    {
        if (!type.Validate(number))
            throw new Exception("Número do documento inválido");

        Number = number;
        HashedNumber = Number.ToHashedString();
        Type = type;
    }

    public string Number { get; private set; } = string.Empty;
    public string HashedNumber { get; private set; } = string.Empty;
    public EDocumentType Type { get; private set; } = EDocumentType.CPF;
}