using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using CourseManagerApi.Core.Utils;

namespace CourseManagerApi.Core.Extensions;

public static class EnumExtension
{
    public static bool Validate(this EDocumentType documentType, string document) 
    {
        switch (documentType)
        {
            case EDocumentType.CPF:
                return StringUtils.ValidateCPF(document);
            case EDocumentType.CNPJ:
                return StringUtils.ValidateCNPJ(document);
            default:
                return false;
        }
    }

    public static bool IsOther(this EGenderType type) => type.Equals(EGenderType.Other);
}