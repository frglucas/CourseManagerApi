using Flunt.Notifications;

namespace CourseManagerApi.Shared.Entities;

public abstract class Entity : Notifiable<Notification>
{
    protected Entity() => Id = Guid.NewGuid();

    public Guid Id { get; private set; }

    protected abstract void VerifyNotifications();
}
