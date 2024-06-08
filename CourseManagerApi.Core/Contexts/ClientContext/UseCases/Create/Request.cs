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
    string LeadId = "",
    string BadgeName = "", 
    string Observation = "",
    string GenderDetail = ""
) : IRequest<Response>;