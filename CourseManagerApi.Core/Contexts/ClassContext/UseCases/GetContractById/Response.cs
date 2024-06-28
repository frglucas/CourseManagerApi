using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById;

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
    string ClientId,
    string ClientName,
    string ClientEmail,
    PaymentResponseData? payment
);

public record PaymentResponseData(
    string Id,
    List<InstallmentResponseData> installments
);

public record InstallmentResponseData(
    string Id,
    decimal Money,
    int PaymentStatus,
    int PaymentMethod,
    DateTime DueDate
);