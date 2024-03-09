using Flunt.Notifications;

namespace CourseManagerApi.Shared.ValueObjects;

public abstract class ValueObject : Notifiable<Notification> 
{
    public abstract bool Validate();
    protected abstract bool VerifyNullValuesToNotifications();
    protected abstract void VerifyNotifications();
}