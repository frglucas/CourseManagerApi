using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllOccupations;

public class Response : Shared.Contexts.SharedContext.UseCases.Response
{
    protected Response() { }

    public Response(
        string message, 
        int status, 
        IEnumerable<Notification>? notifications = null)
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

public class ResponseData
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}