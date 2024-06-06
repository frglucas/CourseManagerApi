using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create;

public record Request(
    string Email, 
    string FullName,
    string AreaCode,
    string PhoneNumber,  
    string Observation = ""
) : IRequest<Response>;