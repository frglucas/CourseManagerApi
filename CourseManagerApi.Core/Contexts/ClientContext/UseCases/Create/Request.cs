using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create;

public record Request(
    string Email, 
    string Name, 
    string Document, 
    EDocumentType DocumentType,
    DateTime BirthDate,
    string OccupationId,
    bool IsSmoker,
    EGenderType GenderType,
    string Observation = "",
    string GenderDetail = ""
) : IRequest<Response>;