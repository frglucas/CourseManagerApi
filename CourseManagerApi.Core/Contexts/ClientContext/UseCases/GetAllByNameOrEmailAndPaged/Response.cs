using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.GetAllByNameOrEmailAndPaged;

public class Response : Shared.Contexts.SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public Response(string message, IEnumerable<ResponseData> data, int pageCount, int page)
    {
        Message = message;
        Status = 201;
        Notifications = null;
        Data = data;
        PageCount = pageCount;
        Page = page;
    }

    public IEnumerable<ResponseData> Data { get; set; } = [];

    public int PageCount { get; set; } = (int)decimal.Zero;

    public int Page { get; set; } = (int)decimal.Zero;
}


public record ResponseData(Guid Id, string Name, string Email, bool isActive);