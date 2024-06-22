using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit;

public record Request(
    string ClientId,
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
    string IndicatorId = "",
    string BadgeName = "",
    string Observation = "",
    string GenderDetail = ""
) : IRequest<Response>;