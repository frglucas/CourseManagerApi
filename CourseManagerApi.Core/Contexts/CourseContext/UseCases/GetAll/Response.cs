using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.GetAll;

public class Response : Shared.Contexts.SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(string message, IEnumerable<ResponseData> data)
    {
        Message = message;
        Status = 200;
        Notifications = null;
        Data = data;
    }

    public IEnumerable<ResponseData> Data { get; set; } = [];
}


public record ResponseData(Guid Id, string Name);