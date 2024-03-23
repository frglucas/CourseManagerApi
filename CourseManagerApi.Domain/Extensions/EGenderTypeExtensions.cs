using CourseManagerApi.Domain.Enums;

namespace CourseManagerApi.Domain.Extensions;

public static class EGenderTypeExtensions
{
    public static bool IsOther(this EGenderType genderType) => genderType.Equals(EGenderType.Other);
}