using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using Flunt.Notifications;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Get;

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
    string FullName, 
    string BadgeName, 
    string Email,
    DateTime BirthDate,
    EDocumentType DocumentType,
    string Document,
    string? OccupationId,
    EGenderType GenderType,
    string GenderDetails,
    bool IsSmoker, 
    bool IsActive,
    string Observation,
    string CreatorId,
    string CaptivatorId,
    string IndicatorId,
    bool IndicatorIsCaptivator
);
