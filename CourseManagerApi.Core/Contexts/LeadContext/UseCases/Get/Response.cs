using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Get;

public class Response : Shared.Contexts.SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(string message, ResponseData? data)
    {
        Message = message;
        Status = 200;
        Notifications = null;
        Data = data;
    }

    public ResponseData? Data { get; set; }
}

public record ResponseData(
    string Id, 
    string Name,
    string Email,
    string AreaCode,
    string PhoneNumber,
    string Observation,
    string CreatorName,
    bool IsAdhered
);