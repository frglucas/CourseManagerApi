using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Delete;

public class Response : Shared.Contexts.SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(string message, object data = null!)
    {
        Message = message;
        Status = 200;
        Notifications = null;
        Data = data;
    }

    public object? Data { get; set; }
}
