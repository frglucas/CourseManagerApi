using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClientContext.Entities;

public class Occupation : Entity
{
    protected Occupation() { } 
    public Occupation(int code, string description)
    {
        Code = code;
        Description = description;
    }

    public int Code { get; private set; } = int.MinValue;
    public string Description { get; private set; } = string.Empty;
}