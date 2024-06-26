namespace CourseManagerApi.Shared.Contexts.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    protected Entity() => Id = Guid.NewGuid();

    public Guid Id { get; }
    public Guid? TenantId { get; private set; } = null;

    public bool Equals(Guid id) => Id == id;

    public override int GetHashCode() => Id.GetHashCode();
}