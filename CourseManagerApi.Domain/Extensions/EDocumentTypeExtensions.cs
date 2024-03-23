using System.Reflection.Metadata;
using CourseManagerApi.Domain.Enums;
using CourseManagerApi.Domain.Utils;

namespace CourseManagerApi.Domain.Extensions;

public static class EDocumentTypeExtensions
{
    public static bool Validate(this EDocumentType documentType, string document) 
    {
        switch (documentType)
        {
            case EDocumentType.CPF:
                return DocumentUtils.ValidateCPF(document);
            case EDocumentType.CNPJ:
                return DocumentUtils.ValidateCNPJ(document);
            default:
                return false;
        }
    }
}