using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using CourseManagerApi.Core.Extensions;
using CourseManagerApi.Shared.Contexts.SharedContext.ValueObjects;

namespace CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;

public class Gender : ValueObject
{
    protected Gender() { }
    public Gender(EGenderType type, string detail = null!)
    {
        Type = type;
        Detail = detail;

        if (!type.IsOther())
            Detail = string.Empty;
    }

    public EGenderType Type { get; private set; } = EGenderType.Uninformed;
    public string Detail { get; private set; } = string.Empty;
}