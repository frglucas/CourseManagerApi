namespace CourseManagerApi.Shared.Commands;

public interface ICommand
{
    void Validate();

    bool VerifyNullValuesToNotifications();
}