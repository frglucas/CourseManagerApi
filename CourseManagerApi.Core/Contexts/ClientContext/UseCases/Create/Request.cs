using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create;

public record Request(
    string Email, 
    string FullName,
    string Document, 
    EDocumentType DocumentType,
    DateTime BirthDate,
    string OccupationId,
    bool IsSmoker,
    EGenderType GenderType,
    string CaptivatorId,
    bool IndicatorIsCaptivator,
    List<PhoneNumberRequest> phoneNumbers,
    string IndicatorId = "",
    string LeadId = "",
    string BadgeName = "", 
    string Observation = "",
    string GenderDetail = ""
) : IRequest<Response>;

public record PhoneNumberRequest(string AreaCode, string PhoneNumber);