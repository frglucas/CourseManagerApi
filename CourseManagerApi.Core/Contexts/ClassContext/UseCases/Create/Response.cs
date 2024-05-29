using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Create;

public class Response : Shared.Contexts.SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(string message, ResponseData data)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data;
    }

    public ResponseData? Data { get; set; }
}


public record ResponseData(Guid Id);