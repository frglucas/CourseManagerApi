using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit;

public record Request(
    string LeadId,
    string Email, 
    string FullName,
    string AreaCode,
    string PhoneNumber,  
    string Observation = ""
) : IRequest<Response>;